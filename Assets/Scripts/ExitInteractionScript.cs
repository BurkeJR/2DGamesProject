using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Experimental.Rendering.Universal;

public class ExitInteractionScript : MonoBehaviour
{
    public DayNightScript _dayNightScript;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        string sceneName = SceneManager.GetActiveScene().name;

        PlayerPrefs.SetFloat(ConstLabels.pref_timePassed, _dayNightScript._timePassed);
        PlayerPrefs.SetFloat(ConstLabels.pref_lastUpdate, _dayNightScript._lastUpdate);

        if (sceneName == ConstLabels.scene_farm)
        {
            SceneManager.LoadScene(ConstLabels.scene_shop);
        } 
        else if (sceneName == ConstLabels.scene_shop)
        {
            SceneManager.LoadScene(ConstLabels.scene_farm);
        }
    }
}
