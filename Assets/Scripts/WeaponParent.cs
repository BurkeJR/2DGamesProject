using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WeaponParent : MonoBehaviour
{
    public DayNightScript _dnScript;

    public Vector2 PointerPosition { get; set; }
    public Animator _ganimator;
    public Animator _sanimator;
    public float _gunDelay = 0.5f;
    public float _swordDelay = 0.3f;

    private bool _attackBlocked = false;
    private bool _swordOut = false;
    private bool _hoeOut = false;

    public GameObject _bulletPrefab;
    public int _ammo;

    private GameObject _sword;
    private GameObject _gun;
    private GameObject _hoe;
    private GameObject _muzzle;

    public Transform _origin;
    Transform _guntrans;
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
        _guntrans = transform.GetChild(1).transform;
        _hoe = this.transform.GetChild(3).gameObject;

        ResetAmmo();

        _gunDamage = PlayerPrefs.GetInt(ConstLabels.pref_upgrade_gun);
        _swordDamage = PlayerPrefs.GetInt(ConstLabels.pref_player_melee_damage)
            + PlayerPrefs.GetInt(ConstLabels.pref_upgrade_melee);

        if (_gunDamage == 0)
        {
            _gunDamage = 4;
        }
        if (_swordDamage == 0)
        {
            _swordDamage = 1;
        }
    }

    private void Update()
    {
        if(!pauseMenuScript._GameIsPaused)
        {
            if (Input.GetKey(KeyCode.Alpha1) && SceneManager.GetActiveScene().name != "ShopScene" && !_dnScript._daytime)
            {
                _swordOut = false;
                _hoeOut = false;

            }
            else if (Input.GetKey(KeyCode.Alpha2) && SceneManager.GetActiveScene().name != "ShopScene" && !_dnScript._daytime)
            {
                _swordOut = true;
                _hoeOut = false;
            }
            else if ((Input.GetKey(KeyCode.Alpha3)) && SceneManager.GetActiveScene().name != "ShopScene")
            {
                _swordOut = false;
                _hoeOut = true;
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

            if (_swordOut && !_hoeOut && !_dnScript._daytime)
            {
                _sword.SetActive(true);
                _muzzle.SetActive(false);
                _gun.SetActive(false);
                _hoe.SetActive(false);
            }
            else if (!_swordOut && !_hoeOut && SceneManager.GetActiveScene().name != "ShopScene" && !_dnScript._daytime)
            {
                _sword.SetActive(false);
                _muzzle.SetActive(true);
                _gun.SetActive(true);
                _hoe.SetActive(false);
            }
            else if((_dnScript._daytime || _hoeOut) && SceneManager.GetActiveScene().name != "ShopScene")
            {
                _sword.SetActive(false);
                _muzzle.SetActive(false);
                _gun.SetActive(false);
                _hoe.SetActive(true);
            }
        } 
    }

    public void Attack()
    {
        if(_attackBlocked || Time.timeScale == 0 || _hoeOut)
        {
            return;
        }
        if (_swordOut && !_dnScript._daytime)
        {
            _as.PlayOneShot(_slash, .5f);
            _sanimator.SetTrigger("Attack");
            _attackBlocked = true;
            StartCoroutine(DelayAttack(_swordDelay));
        }
        else if(!_dnScript._daytime)
        {
            float maxAngle = 10f;
            if (_ammo > 0)
            {
                _as.PlayOneShot(_shotgun);
                _ganimator.SetTrigger("Attack");
                _attackBlocked = true;
                GameObject clone = Instantiate(_bulletPrefab, _guntrans.position, transform.rotation);
                clone.GetComponent<bulletScript>()._damage = _gunDamage;
                if(PlayerPrefs.GetInt(ConstLabels.pref_player_gun_spread) == 1)
                {
                    for (int i = 0; i < 2; i++)
                    {
                        // Generate a random angle between -maxAngle and maxAngle
                        float angle = Random.Range(-maxAngle, maxAngle);
                        Quaternion rotation = Quaternion.Euler(0, 0, angle + Mathf.Atan2(PointerPosition.y - transform.position.y, PointerPosition.x - 
                                                                                transform.position.x) * Mathf.Rad2Deg);
                        GameObject clones = Instantiate(_bulletPrefab, _guntrans.position, rotation);
                        clones.GetComponent<bulletScript>()._damage = _gunDamage;
                    }
                }

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
            if (collider.gameObject.tag == "Bear")
            {
                BearHealth health;
                if (health = collider.GetComponent<BearHealth>())
                {
                    health.GetHit(_swordDamage, transform.parent.gameObject);
                }
            }
            else
            {
                HealthScript health;
                if (health = collider.GetComponent<HealthScript>())
                {
                    health.GetHit(_swordDamage, transform.parent.gameObject);
                }
            }
                
        }
    }

    public void ResetAmmo()
    {
        _ammo = ConstLabels.start_ammo * PlayerPrefs.GetInt(ConstLabels.pref_upgrade_ammo);
        PlayerPrefs.SetInt(ConstLabels.pref_player_ammo, _ammo);
    }

}
