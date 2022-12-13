using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(ParticleSystem))]
public class BearHealth : MonoBehaviour
{
    [SerializeField]
    private int currentHealth, maxHealth;

    public UnityEvent<GameObject> OnHitWithReference, OnDeathWithReference;

    ParticleSystem _ps;
    private Transform bar;
    public farmMGRScript _mgrScript;

    private void Start()
    {
        Transform first = transform.Find("HealthBar");
        bar = first.transform.Find("Bar");
        
        _ps = GetComponent<ParticleSystem>();
        _mgrScript = FindObjectOfType<farmMGRScript>();
    }

    private void Update()
    {
        
    }

    public void InitializeHealth(int healthValue)
    {
        currentHealth = healthValue;
        maxHealth = healthValue;
    }

    public void GetHit(int amount, GameObject sender)
    {
        currentHealth -= amount;
        SetSize((float)((float)currentHealth / (float)maxHealth));
        _ps.Play();

        if (currentHealth <= 0)
        {
            DeadPrep();
        }
    }

    void DeadPrep()
    {
        // play death sound
        _mgrScript.PlayDeathSound();
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
        Invoke("MakeDead", 1);
    }

    void MakeDead()
    {
        Destroy(gameObject);
    }

    public void SetSize(float sizeNormalized)
    {
        bar.localScale = new Vector3(sizeNormalized, 1f);
    }
}
