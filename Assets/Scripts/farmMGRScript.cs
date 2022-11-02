using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class farmMGRScript : MonoBehaviour
{
    public Text ammo;
    public Text coins;

    public PlantingScript plScript;

    int currDay;

    // Start is called before the first frame update
    void Start()
    {
        currDay = PlayerPrefs.GetInt(ConstLabels.pref_currentDay);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene(ConstLabels.menu_scene);
        }

        ammo.text = PlayerPrefs.GetInt(ConstLabels.pref_player_ammo).ToString();
        coins.text = PlayerPrefs.GetInt(ConstLabels.pref_player_currency).ToString();

        if (currDay < PlayerPrefs.GetInt(ConstLabels.pref_currentDay))
        {
            PlayerPrefs.SetInt(ConstLabels.pref_player_currency, PlayerPrefs.GetInt(ConstLabels.pref_player_currency) + (50 * plScript._cropList.Count));
            plScript.HarvestCrops();
            currDay++;
        }
    }
}
