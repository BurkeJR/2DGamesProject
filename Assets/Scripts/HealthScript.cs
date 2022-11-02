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

    private void Start()
    {
        _ps = GetComponent<ParticleSystem>();
    }

    public void InitializeHealth(int healthValue)
    {
        currentHealth = healthValue;
        maxHealth = healthValue;
    }

    public void GetHit(int amount, GameObject sender)
    {
        currentHealth -= amount;
        _ps.Play();

        if (currentHealth <= 0)
        {
            DeadPrep();
        }
    }

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
