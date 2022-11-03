using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements.Experimental;

public class EnemyScript : MonoBehaviour
{
    public PlantingScript _plantScript;
    public farmMGRScript farmMGRScript;

    GameObject _target;
    public float _speed;
    public GameObject _manager;

    Transform _transform;

    Animator _anim;
    bool _touchedPlant = false;
    List<GameObject> _plants;

    private float distance;

    bool _eating;
    float _eatTimer;

    // Start is called before the first frame update
    void Start()
    {
        _transform = transform;
        UpdateTarget();
        farmMGRScript = FindObjectOfType<farmMGRScript>();
        _plantScript = FindObjectOfType<PlantingScript>();
        _plants = new List<GameObject>();
        _anim = GetComponent<Animator>();
        _eating = false;
        _eatTimer = Time.time;
    }

    void UpdateTarget()
    {
        _plants = _plantScript._cropList;
        if (_plants.Count > 0)
        {
            float smallestDistance = float.MaxValue;
            int closestPlant = 0;
            for (int i = 0; i < _plants.Count; i++)
            {
                float dist = Vector2.Distance(_transform.position, _plants[i].transform.position);
                if (dist < smallestDistance)
                {
                    smallestDistance = dist;
                    closestPlant = i;
                }
            }
            _target = _plants[closestPlant];
        }
        else
        {
            // if we get here then that means there aren't any plants left, so game over
            _target = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_target != null)
        {
            Vector3 targetPos = _target.transform.position;

            float dist = Vector2.Distance(_transform.position, targetPos);
            Vector2 direction = targetPos - _transform.position;


            if (direction.x < 0)
            {
                this.gameObject.GetComponent<SpriteRenderer>().flipX = true;
            }
            else
            {
                this.gameObject.GetComponent<SpriteRenderer>().flipX = false;
            }
            _transform.position = Vector2.MoveTowards(_transform.position, targetPos, _speed * Time.deltaTime);

            _anim.SetFloat("Horizontal", direction.y);
            _anim.SetFloat("Vertical", direction.x);

            if (distance > Vector2.Distance(_transform.position, targetPos))
            {
                _anim.SetFloat("Speed", 1);
            }
            else
            {
                _anim.SetFloat("Speed", 0);
            }
        }
        else
        {
            // if we get here then game over (this is used for testing)
            UpdateTarget();
        }

        if (_eating && Time.time - _eatTimer > 2)
        {
            // play eating sound
            farmMGRScript.PlayEatingSound();
            _eatTimer = Time.time;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Crop" && _touchedPlant == false)
        {
            _touchedPlant = true;
            _eating = true;
            
            StartCoroutine(killPlant(collision.gameObject));
        }
    }

    IEnumerator killPlant(GameObject toDestroy)
    {
        if (!toDestroy.Equals(null))
        {
            
            yield return new WaitForSeconds(7f);
            if (_touchedPlant)
            {
                _touchedPlant = false;
                _plantScript._cropList.Remove(toDestroy);
                // get list of targetting animals and retarget them here
                Destroy(toDestroy);
            }
            _eating = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        GameObject.FindGameObjectWithTag("Targeted").gameObject.tag = "Crop";
        StopCoroutine(killPlant(other.gameObject));
    }
}
