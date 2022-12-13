using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeedScript : MonoBehaviour
{

    public farmMGRScript _farmMGRScript;
    public GameObject _hoe;

    // Start is called before the first frame update
    void Start()
    {
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
