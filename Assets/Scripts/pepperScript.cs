using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pepperScript : MonoBehaviour
{

    public int _damage;

    private void OnCollisionEnter2D(Collision2D collider)
    {
        HealthScript health;
        if (health = collider.gameObject.GetComponent<HealthScript>())
        {
            health.GetHit(_damage, collider.gameObject);
        }
    }
}
