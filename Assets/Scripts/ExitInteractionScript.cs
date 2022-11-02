using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Experimental.Rendering.Universal;

public class ExitInteractionScript : MonoBehaviour
{
    public DayNightScript _dayNightScript;
    public Light2D _globalLight;

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
        if (_dayNightScript._daytime && collision.tag.Equals(ConstLabels.tag_player))
        {
            SwitchScenes();
        }
    }

    public void SwitchScenes()
    {
        string sceneName = SceneManager.GetActiveScene().name;

        PlayerPrefs.SetFloat(ConstLabels.pref_timePassed, _dayNightScript._timePassed);
        PlayerPrefs.SetFloat(ConstLabels.pref_lastUpdate, _dayNightScript._lastUpdate);

        PlayerPrefs.SetFloat(ConstLabels.pref_light_r, _globalLight.color.r);
        PlayerPrefs.SetFloat(ConstLabels.pref_light_g, _globalLight.color.g);
        PlayerPrefs.SetFloat(ConstLabels.pref_light_b, _globalLight.color.b);

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
