using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements.Experimental;

[RequireComponent(typeof(SpriteRenderer))]
public class EnemyScript : MonoBehaviour
{
    public PlantingScript _plantScript;
    public farmMGRScript farmMGRScript;
    public DayNightScript _dnScript;

    protected GameObject _target;
    public float _speed;
    public GameObject _manager;

    protected Transform _transform;
    protected SpriteRenderer _srender;

    Animator _anim;
    bool _touchedPlant = false;
    List<GameObject> _plants;


    bool _eating;
    float _eatTimer;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        _transform = transform;
        _srender = GetComponent<SpriteRenderer>();
        UpdateTarget();
        farmMGRScript = FindObjectOfType<farmMGRScript>();
        _plantScript = FindObjectOfType<PlantingScript>();
        _dnScript = FindObjectOfType<DayNightScript>();

        _plants = new List<GameObject>();
        _anim = GetComponent<Animator>();
        _eating = false;
        _eatTimer = Time.time;
    }

    protected virtual void UpdateTarget()
    {
        if(_plantScript._cropList != null)
        {
            _plants = _plantScript._cropList;
        }
        
        if (_plants.Count > 0)
        {
            float smallestDistance = float.MaxValue;
            int closestPlant = 0;
            float maxVar = .5f;
            for (int i = 0; i < _plants.Count; i++)
            {
                float variation = Random.Range(-maxVar, maxVar);
                Vector2 randTar = new Vector2(_plants[i].transform.position.x + variation, _plants[i].transform.position.y + variation);
                float dist = Vector2.Distance(_transform.position, randTar);
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
            DoMovement();
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

        if (_dnScript._daytime)
        {
            Destroy(gameObject);
        }
    }

    private void DoMovement()
    {
        Vector3 targetPos = GetTargetPos();

        float dist = Vector2.Distance(_transform.position, targetPos);
        Vector2 direction = targetPos - _transform.position;


        if (direction.x < 0)
        {
            _srender.flipX = true;
        }
        else
        {
            _srender.flipX = false;
        }


        _transform.position = Vector2.MoveTowards(_transform.position, targetPos, _speed * Time.deltaTime);

        _anim.SetFloat("Horizontal", direction.y);
        _anim.SetFloat("Vertical", direction.x);

        if (dist > Vector2.Distance(_transform.position, targetPos))
        {
            _anim.SetFloat("Speed", 1);
        }
        else
        {
            _anim.SetFloat("Speed", 0);
        }
    }

    protected virtual Vector2 GetTargetPos()
    {
        return _target.transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.tag == "Crop" || collision.gameObject.tag == "beanPlant") && _touchedPlant == false)
        {
            _touchedPlant = true;
            _eating = true;
            if(collision.gameObject.tag == "Crop")
            {
                StartCoroutine(killPlant(collision.gameObject));
            }
            else if(collision.gameObject.tag == "beanPlant")
            {
                StartCoroutine(wavePlant(collision.gameObject));
            }
            
        }
    }

    IEnumerator wavePlant(GameObject toWave)
    {
        if (!toWave.Equals(null))
        {
                yield return new WaitForSeconds(2f);
                toWave.GetComponent<Animator>().SetBool("touched", true);
                yield return new WaitForSeconds(1f);
                toWave.GetComponent<Animator>().SetBool("touched", false);
                toWave.gameObject.tag = "Crop";
            _eating = false;
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
