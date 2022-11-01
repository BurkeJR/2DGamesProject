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
        PlayerPrefs.SetInt(ConstLabels.pref_player_bullets, 10);
        PlayerPrefs.SetInt(ConstLabels.pref_player_currency, 0);
        PlayerPrefs.SetInt(ConstLabels.pref_player_gun_damage, 2);
        PlayerPrefs.SetInt(ConstLabels.pref_player_melee_damage, 1);
    }
}
