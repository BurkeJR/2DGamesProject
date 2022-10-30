using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class bulletScript : MonoBehaviour
{
    public float speed = 20f;
    Rigidbody2D _rbody;
    // Start is called before the first frame update
    void Start()
    {
        _rbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        _rbody.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Enemy")
        {
            Destroy(other.gameObject);
        }
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collider)
    {
        HealthScript health;
        if (health = collider.gameObject.GetComponent<HealthScript>())
        {
            health.GetHit(2, collider.gameObject);
        }
        Destroy(gameObject);
    }
}
