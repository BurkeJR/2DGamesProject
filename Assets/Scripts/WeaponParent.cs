using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public int _ammo = 7;

    private GameObject _sword;
    private GameObject _gun;
    private GameObject _muzzle;

    public Transform _origin;
    public float radius;

    private void Start()
    {
        _sword = this.transform.GetChild(2).gameObject;
        _gun = this.transform.GetChild(0).gameObject;
        _muzzle = this.transform.GetChild(1).gameObject;
        PlayerPrefs.SetInt("Ammo", _ammo);
    }

    private void Update()
    {
        PlayerPrefs.SetInt("Ammo", _ammo);
        if (Input.GetKey(KeyCode.Alpha1))
        {
            _swordOut = false;
        }
        else if (Input.GetKey(KeyCode.Alpha2))
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
        else
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
            _sanimator.SetTrigger("Attack");
            _attackBlocked = true;
            StartCoroutine(DelayAttack(_swordDelay));
        }
        else 
        {
            if(_ammo > 0) {
                _ganimator.SetTrigger("Attack");
                _attackBlocked = true;
                GameObject clone = Instantiate(_bulletPrefab, transform.GetChild(1).transform.position, transform.rotation);
                _ammo--;
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
        foreach(Collider2D collider in Physics2D.OverlapCircleAll(_origin.position, radius))
        {
            HealthScript health;
            if(health = collider.GetComponent<HealthScript>())
            {
                health.GetHit(1, transform.parent.gameObject);
            }
        }
    }

}
