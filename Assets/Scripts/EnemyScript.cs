using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    GameObject _target;
    public float _speed;
    public GameObject _manager;
    int _currentCount = -1;
    Animator _anim;
    bool _touchedPlant = false;

    private float distance;

    // Start is called before the first frame update
    void Start()
    {
        _target = GameObject.FindGameObjectWithTag("Player");

        _anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_manager.GetComponent<PlantingScript>()._cropList.Count > _currentCount)
        {
            _currentCount = _manager.GetComponent<PlantingScript>()._cropList.Count;
            if(_currentCount == 1)
            {
                _target = _manager.GetComponent<PlantingScript>()._cropList[0];
            }
            else
            {
                foreach (GameObject crop in _manager.GetComponent<PlantingScript>()._cropList)
                {
                   if (Vector2.Distance(transform.position, _target.transform.position) > Vector2.Distance(transform.position, crop.transform.position))
                   {
                        _target = crop;
                   }
                }
            }
            
        }  
        distance = Vector2.Distance(transform.position, _target.transform.position);
        Vector2 direction = _target.transform.position - transform.position;


        if (direction.x < 0)
        {
            this.gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            this.gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }
        transform.position = Vector2.MoveTowards(this.transform.position, _target.transform.position, _speed * Time.deltaTime);
        _anim.SetFloat("Horizontal", direction.y);
        _anim.SetFloat("Vertical", direction.x);
        if (distance > Vector2.Distance(transform.position, _target.transform.position))
        {
            _anim.SetFloat("Speed", 1);
        }
        else
        {
            _anim.SetFloat("Speed", 0);
        }


    }
}
