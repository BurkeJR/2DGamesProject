using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class farmMGRScript : MonoBehaviour
{

    public PlantingScript plScript;
    int currDay;

    AudioSource _as;
    public AudioClip _eating;
    public AudioClip _death;

    // Start is called before the first frame update
    void Start()
    {
        _as = GetComponent<AudioSource>();
        currDay = PlayerPrefs.GetInt(ConstLabels.pref_currentDay);
    }

    // Update is called once per frame
    void Update()
    {
        if (currDay < PlayerPrefs.GetInt(ConstLabels.pref_currentDay))
        {
            PlayerPrefs.SetInt(ConstLabels.pref_player_currency, PlayerPrefs.GetInt(ConstLabels.pref_player_currency) + (15 * plScript._cropList.Count));
            plScript.HarvestCrops();
            currDay++;
        }
    }

    public void PlayEatingSound()
    {
        _as.PlayOneShot(_eating);
    }

    public void PlayDeathSound()
    {
        _as.PlayOneShot(_death);
    }

    
}
