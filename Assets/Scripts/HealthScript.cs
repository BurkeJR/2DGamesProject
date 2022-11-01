using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(ParticleSystem))]
public class HealthScript : MonoBehaviour
{
    [SerializeField]
    private int currentHealth, maxHealth;

    public UnityEvent<GameObject> OnHitWithReference, OnDeathWithReference;

    ParticleSystem _ps;

    //[SerializeField]
    //private bool isDead = false;

    private void Start()
    {
        _ps = GetComponent<ParticleSystem>();
    }

    public void InitializeHealth(int healthValue)
    {
        currentHealth = healthValue;
        maxHealth = healthValue;
        //isDead = false;
    }

    public void GetHit(int amount, GameObject sender)
    {
        //if (isDead)
        //    return;
        //if (sender.layer == gameObject.layer)
        //    return;

        currentHealth -= amount;
        _ps.Play();

        if (currentHealth <= 0)
        {
            DeadPrep();
        }

        //if (currentHealth > 0)
        //{
        //    OnHitWithReference?.Invoke(sender);
        //}
        //else
        //{
        //    OnDeathWithReference?.Invoke(sender);
        //    isDead = true;
        //    Destroy(gameObject);
        //}
    }

    //private void OnCollisionEnter2D(Collision2D collider)
    //{
    //    if (collider.gameObject.tag == "Bullet")
    //    {
    //        currentHealth -= 2;
    //        _ps.Play();
    //    }
    //    Debug.Log("Hit");
    //    if (currentHealth <= 0)
    //    {
    //        //isDead = true;
    //        //Destroy(gameObject);
    //        DeadPrep();
    //    }
    //}

    void DeadPrep()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
        Invoke("MakeDead", 1);
    }

    void MakeDead()
    {
        Destroy(gameObject);
    }
}
