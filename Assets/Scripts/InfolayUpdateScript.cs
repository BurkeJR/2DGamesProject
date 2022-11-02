using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class InfolayUpdateScript : MonoBehaviour
{
    public Text dayNumber;
    public Text ammo;
    public Text coins;

    public Text cornSeeds;
    public Text beanSeeds;
    public Text pepperSeeds;
    public Text carrotSeeds;
    public Text eggplantSeeds;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        ammo.text = PlayerPrefs.GetInt(ConstLabels.pref_player_ammo).ToString();
        coins.text = PlayerPrefs.GetInt(ConstLabels.pref_player_currency).ToString();
        dayNumber.text = "Day " + PlayerPrefs.GetInt(ConstLabels.pref_currentDay);

        cornSeeds.text = PlayerPrefs.GetInt(ConstLabels.pref_corn_seeds).ToString();
        beanSeeds.text = PlayerPrefs.GetInt(ConstLabels.pref_bean_seeds).ToString();
        pepperSeeds.text = PlayerPrefs.GetInt(ConstLabels.pref_pepper_seeds).ToString();
        carrotSeeds.text = PlayerPrefs.GetInt(ConstLabels.pref_carrot_seeds).ToString();
        eggplantSeeds.text = PlayerPrefs.GetInt(ConstLabels.pref_eggplant_seeds).ToString();
    }
}
