using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    private void Start()
    {
        PlayerPrefs.SetFloat(ConstLabels.pref_timePassed, 0);
        PlayerPrefs.SetFloat(ConstLabels.pref_lastUpdate, 0);

        Color _dayColor = new Color(1.0f, 1.0f, 1.0f);
        PlayerPrefs.SetFloat(ConstLabels.pref_light_r, _dayColor.r);
        PlayerPrefs.SetFloat(ConstLabels.pref_light_g, _dayColor.g);
        PlayerPrefs.SetFloat(ConstLabels.pref_light_b, _dayColor.b);
    }

    public void ChangeScenByName(string name)
    {
        SceneManager.LoadScene(name);
    }
   
    public void Quit()
    {
        Application.Quit();
    }
}
