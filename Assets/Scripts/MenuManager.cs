using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    private void Start()
    {
        LoadDefaultGlobals();
    }

    public void ChangeScenByName(string name)
    {
        SceneManager.LoadScene(name);
    }
   
    public void Quit()
    {
        Application.Quit();
    }

    void LoadDefaultGlobals()
    {
        // time
        PlayerPrefs.SetFloat(ConstLabels.pref_timePassed, 0);
        PlayerPrefs.SetFloat(ConstLabels.pref_lastUpdate, 0);
        PlayerPrefs.SetInt(ConstLabels.pref_currentDay, 1);

        // light
        Color _dayColor = new Color(1.0f, 1.0f, 1.0f);
        PlayerPrefs.SetFloat(ConstLabels.pref_light_r, _dayColor.r);
        PlayerPrefs.SetFloat(ConstLabels.pref_light_g, _dayColor.g);
        PlayerPrefs.SetFloat(ConstLabels.pref_light_b, _dayColor.b);

        // player
        PlayerPrefs.SetInt(ConstLabels.pref_player_ammo, 7);
        PlayerPrefs.SetInt(ConstLabels.pref_player_currency, 0);
        PlayerPrefs.SetInt(ConstLabels.pref_player_gun_damage, 2);
        PlayerPrefs.SetInt(ConstLabels.pref_player_melee_damage, 1);

        // upgrades
        PlayerPrefs.SetInt(ConstLabels.pref_upgrade_melee, 0);
        PlayerPrefs.SetInt(ConstLabels.pref_upgrade_gun, 0);
        PlayerPrefs.SetInt(ConstLabels.pref_upgrade_ammo, 1);
        PlayerPrefs.SetFloat(ConstLabels.pref_upgrade_speed, 0);

        // seeds
        PlayerPrefs.SetInt(ConstLabels.pref_corn_seeds, 0);
        PlayerPrefs.SetInt(ConstLabels.pref_bean_seeds, 0);
        PlayerPrefs.SetInt(ConstLabels.pref_carrot_seeds, 0);
        PlayerPrefs.SetInt(ConstLabels.pref_pepper_seeds, 0);
        PlayerPrefs.SetInt(ConstLabels.pref_eggplant_seeds, 0);
    }
}
