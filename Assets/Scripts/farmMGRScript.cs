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
    public Text _earned;

    // Start is called before the first frame update
    void Start()
    {
        _as = GetComponent<AudioSource>();
        currDay = PlayerPrefs.GetInt(ConstLabels.pref_currentDay);
        _earned.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (currDay < PlayerPrefs.GetInt(ConstLabels.pref_currentDay))
        {
            StartCoroutine(earned());
            PlayerPrefs.SetInt(ConstLabels.pref_player_currency, PlayerPrefs.GetInt(ConstLabels.pref_player_currency) + (15 * plScript._cropList.Count));
            plScript.HarvestCrops();
            currDay++;
        }
    }

    public void PlayEatingSound()
    {
        _as.PlayOneShot(_eating, .75f);
    }

    public void PlayDeathSound()
    {
        _as.PlayOneShot(_death, .5f);
    }

    IEnumerator earned()
    {
        _earned.text = "+" + 15 * plScript._cropList.Count;
        _earned.enabled = true;
        _earned.GetComponent<Animator>().SetBool("money", true);
        yield return new WaitForSeconds(3);
        _earned.GetComponent<Animator>().SetBool("money", false);
        _earned.enabled = false;
    }

    
}
