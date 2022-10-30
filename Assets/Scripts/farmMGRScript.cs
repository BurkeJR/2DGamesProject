using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class farmMGRScript : MonoBehaviour
{
    public Text ammo;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ammo.text = PlayerPrefs.GetInt("Ammo").ToString();
    }
}
