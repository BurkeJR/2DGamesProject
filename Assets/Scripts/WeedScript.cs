using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeedScript : MonoBehaviour
{

    public farmMGRScript _farmMGRScript;
    public GameObject _hoe;
    AudioSource _as;
    public AudioClip _weedSound;

    // Start is called before the first frame update
    void Start()
    {
        _as = GetComponent<AudioSource>();
        _hoe = GameObject.FindWithTag("Hoe");
        _farmMGRScript = FindObjectOfType<farmMGRScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        _hoe = GameObject.FindWithTag("Hoe");
        if (_hoe != null)
        {
            if (_hoe.activeInHierarchy)
            {
                _farmMGRScript._as.PlayOneShot(_weedSound);
                _farmMGRScript.WeedRemoved();
                Destroy(gameObject);
            }
            else
            {
                print("hoe not active");
            }
        }
    }
        
}
