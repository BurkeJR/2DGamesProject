using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyScript : MonoBehaviour
{
    public PlantingScript _plantScript;

    GameObject _target;
    public float _speed;
    public GameObject _manager;
    int _currentCount = -1;
    Animator _anim;
    bool _touchedPlant = false;
    List<GameObject> _plants;

    private float distance;

    // Start is called before the first frame update
    void Start()
    {
        _plants = new List<GameObject>();
        _target = GameObject.FindGameObjectWithTag("Crop");
        _anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        if (GameObject.FindGameObjectWithTag("Crop") != null) {
        distance = Vector2.Distance(transform.position, GameObject.FindGameObjectWithTag("Crop").transform.position);
                Vector2 direction = GameObject.FindGameObjectWithTag("Crop").transform.position - transform.position;


                if (direction.x < 0)
                {
                    this.gameObject.GetComponent<SpriteRenderer>().flipX = true;
                }
                else
                {
                    this.gameObject.GetComponent<SpriteRenderer>().flipX = false;
                }
                transform.position = Vector2.MoveTowards(this.transform.position, GameObject.FindGameObjectWithTag("Crop").transform.position, _speed * Time.deltaTime);
                
                _anim.SetFloat("Horizontal", direction.y);
                _anim.SetFloat("Vertical", direction.x);
                if (distance > Vector2.Distance(transform.position, GameObject.FindGameObjectWithTag("Crop").transform.position))
                {
                    _anim.SetFloat("Speed", 1);
                }
                else
                {
                    _anim.SetFloat("Speed", 0);
                }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Crop" && _touchedPlant == false)
        {
            GameObject.FindGameObjectWithTag("Crop").gameObject.tag = "Targeted";
            _touchedPlant = true;
            StartCoroutine(killPlant(collision.gameObject));
        }
    }


    System.Random rnd = new System.Random();
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

        }
    }

    private void OnTriggerExit(Collider other)
    {
        GameObject.FindGameObjectWithTag("Targeted").gameObject.tag = "Crop";
        StopCoroutine(killPlant(other.gameObject));
    }
}
