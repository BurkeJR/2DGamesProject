using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class bulletScript : MonoBehaviour
{
    public float speed = 20f;
    public int _damage;
    Rigidbody2D _rbody;

    // Start is called before the first frame update
    void Start()
    {
        if(gameObject.tag != "beanPlant")
        {
            _rbody = GetComponent<Rigidbody2D>();
            _rbody.velocity = transform.right * speed;
        }
        
    }


    private void OnCollisionEnter2D(Collision2D collider)
    {
        if(collider.gameObject.tag == "Bear")
        {
            BearHealth health;
            if (health = collider.gameObject.GetComponent<BearHealth>())
            {
                health.GetHit(_damage, collider.gameObject);
            }
            else if ((health = collider.gameObject.GetComponent<BearHealth>()) && gameObject.tag == "beanPlant")
            {
                health.GetHit(_damage, collider.gameObject);
                return;
            }
        }
        else { 
            HealthScript health;
            if (health = collider.gameObject.GetComponent<HealthScript>())
            {
            health.GetHit(_damage, collider.gameObject);
            }
            else if ((health = collider.gameObject.GetComponent<HealthScript>()) && gameObject.tag == "beanPlant")
            {
            health.GetHit(_damage, collider.gameObject);
            return;
        }}
        
        
        Destroy(gameObject);
    }
}
