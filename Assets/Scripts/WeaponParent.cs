using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WeaponParent : MonoBehaviour
{
    public Vector2 PointerPosition { get; set; }
    public Animator _ganimator;
    public Animator _sanimator;
    public float _gunDelay = 0.5f;
    public float _swordDelay = 0.3f;

    private bool _attackBlocked = false;
    private bool _swordOut = false;

    public GameObject _bulletPrefab;
    public int _ammo;

    private GameObject _sword;
    private GameObject _gun;
    private GameObject _muzzle;

    public Transform _origin;
    public float radius;

    public AudioClip _slash;
    public AudioClip _shotgun;
    AudioSource _as;

    int _gunDamage;
    int _swordDamage;

    private void Start()
    {
        _as = GetComponent<AudioSource>();

        _sword = this.transform.GetChild(2).gameObject;
        _gun = this.transform.GetChild(0).gameObject;
        _muzzle = this.transform.GetChild(1).gameObject;

        _gunDamage = PlayerPrefs.GetInt(ConstLabels.pref_player_gun_damage) 
            + PlayerPrefs.GetInt(ConstLabels.pref_upgrade_melee);
        _swordDamage = PlayerPrefs.GetInt(ConstLabels.pref_player_melee_damage)
            + PlayerPrefs.GetInt(ConstLabels.pref_upgrade_gun);

        if (_gunDamage == 0)
        {
            _gunDamage = 2;
        }
        if (_swordDamage == 0)
        {
            _swordDamage = 1;
        }
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Alpha1) && SceneManager.GetActiveScene().name != "ShopScene")
        {
            _swordOut = false;
        }
        else if (Input.GetKey(KeyCode.Alpha2) || SceneManager.GetActiveScene().name == "ShopScene")
        {
            _swordOut = true;
        }

        Vector2 direction = (PointerPosition - (Vector2)transform.position).normalized;
        transform.right = direction;

        Vector2 scale = transform.localScale;
        if (direction.x < 0)
        {
            scale.y = 1;
        }
        else if (direction.x > 0)
        {
            scale.y = -1;
        }
        transform.localScale = scale;

        if(_swordOut)
        {
            _sword.SetActive(true);
            _muzzle.SetActive(false);
            _gun.SetActive(false);
        }
        else if(!_swordOut && SceneManager.GetActiveScene().name != "ShopScene")
        {
            _sword.SetActive(false);
            _muzzle.SetActive(true);
            _gun.SetActive(true);
        }
    }

    public void Attack()
    {
        if(_attackBlocked || Time.timeScale == 0)
        {
            return;
        }
        if (_swordOut)
        {
            _as.PlayOneShot(_slash);
            _sanimator.SetTrigger("Attack");
            _attackBlocked = true;
            StartCoroutine(DelayAttack(_swordDelay));
        }
        else 
        {
            print(_ammo);
            if (_ammo > 0)
            {
                _as.PlayOneShot(_shotgun);
                _ganimator.SetTrigger("Attack");
                _attackBlocked = true;
                GameObject clone = Instantiate(_bulletPrefab, transform.GetChild(1).transform.position, transform.rotation);
                clone.GetComponent<bulletScript>()._damage = _gunDamage;
                _ammo--;
                PlayerPrefs.SetInt(ConstLabels.pref_player_ammo, _ammo);
                StartCoroutine(DelayAttack(_gunDelay));
            }

        }
        
    }

    private IEnumerator DelayAttack(float delay)
    {
        yield return new WaitForSeconds(delay);
        _attackBlocked = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Vector3 position = _origin == null ? Vector3.zero : _origin.position;
        Gizmos.DrawWireSphere(position, radius);
    }

    public void DetectColliders()
    {
        foreach (Collider2D collider in Physics2D.OverlapCircleAll(_origin.position, radius))
        {
            HealthScript health;
            if (health = collider.GetComponent<HealthScript>())
            {
                health.GetHit(_swordDamage, transform.parent.gameObject);
            }
        }
    }

    public void ResetAmmo()
    {
        _ammo = ConstLabels.start_ammo * PlayerPrefs.GetInt(ConstLabels.pref_upgrade_ammo);
        PlayerPrefs.SetInt(ConstLabels.pref_player_ammo, _ammo);
    }

}
