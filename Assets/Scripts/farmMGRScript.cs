using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class farmMGRScript : MonoBehaviour
{
    public Text ammo;
    public Text coins;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ammo.text = PlayerPrefs.GetInt("Ammo").ToString();

        if (Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainMenu");
        }
        ammo.text = PlayerPrefs.GetInt(ConstLabels.pref_player_ammo).ToString();
        coins.text = PlayerPrefs.GetInt(ConstLabels.pref_player_currency).ToString();
    }
}
