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
