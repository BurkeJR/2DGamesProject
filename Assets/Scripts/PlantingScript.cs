using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlantingScript : MonoBehaviour
{
    public DayNightScript _dnScript;

    public GameObject _player;
    public Text _gameOver;

    public GameObject _CornPrefab;
    public GameObject _EggplantPrefab;
    public GameObject _CarrotPrefab;
    public GameObject _PepperPrefab;
    public GameObject _BeanPrefab;

    public AudioClip _gainCoins;
    public AudioClip _plantCrop;

    int _currentSeed;
    float _lastPlanted;
    AudioSource _as;

    // list of crop objects
    List<GameObject> _seedList;

    // tuples of crop game objects and the float of the time they were created.
    public List<GameObject> _cropList;


    // Start is called before the first frame update
    void Start()
    {
        _as = GetComponent<AudioSource>();

        _cropList = new List<GameObject>();
        _seedList = new List<GameObject>();

        AddSeed(_CornPrefab);
        AddSeed(_EggplantPrefab);
        AddSeed(_CarrotPrefab);
        AddSeed(_PepperPrefab);
        AddSeed(_BeanPrefab);

        _currentSeed = 0;

        _lastPlanted = Time.time;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (Input.GetKey(KeyCode.F) && _seedList.Count > 0) {
            if (OnSoil() && Time.time - _lastPlanted > 1)
            {
                PlantSeed();
                
            }
        }
        if (!_dnScript._daytime && _cropList.Count == 0)
        {
            HandleLoss();
        }
    }

    

    void HandleLoss()
    {
        SceneManager.LoadScene(ConstLabels.scene_end);
    }

    bool OnSoil()
    {
        return true;
    }

    void AddSeed(GameObject plantPrefab)
    {
        if (plantPrefab == _CornPrefab)
        {
            _seedList.Add(plantPrefab);
            PlayerPrefs.SetInt(ConstLabels.pref_corn_seeds, 1);

        } else if (plantPrefab == _BeanPrefab)
        {
            _seedList.Add(plantPrefab);
            PlayerPrefs.SetInt(ConstLabels.pref_bean_seeds, 1);

        }
        else if (plantPrefab == _PepperPrefab)
        {
            _seedList.Add(plantPrefab);
            PlayerPrefs.SetInt(ConstLabels.pref_pepper_seeds, 1);

        }
        else if (plantPrefab == _EggplantPrefab)
        {
            _seedList.Add(plantPrefab);
            PlayerPrefs.SetInt(ConstLabels.pref_eggplant_seeds, 1);

        }
        else if (plantPrefab == _CarrotPrefab)
        {
            _seedList.Add(plantPrefab);
            PlayerPrefs.SetInt(ConstLabels.pref_carrot_seeds, 1);
            
        }
    }

    void PlantSeed()
    {
        if (_seedList[_currentSeed] == _CornPrefab)
        {
            PlayerPrefs.SetInt(ConstLabels.pref_corn_seeds, PlayerPrefs.GetInt(ConstLabels.pref_corn_seeds) - 1);
        }
        else if (_seedList[_currentSeed] == _CarrotPrefab)
        {
            PlayerPrefs.SetInt(ConstLabels.pref_carrot_seeds, PlayerPrefs.GetInt(ConstLabels.pref_carrot_seeds) - 1);
        }
        else if (_seedList[_currentSeed] == _BeanPrefab)
        {
            PlayerPrefs.SetInt(ConstLabels.pref_bean_seeds, PlayerPrefs.GetInt(ConstLabels.pref_bean_seeds) - 1);
        }
        else if (_seedList[_currentSeed] == _PepperPrefab)
        {
            PlayerPrefs.SetInt(ConstLabels.pref_pepper_seeds, PlayerPrefs.GetInt(ConstLabels.pref_pepper_seeds) - 1);
        }
        else if (_seedList[_currentSeed] == _EggplantPrefab)
        {
            PlayerPrefs.SetInt(ConstLabels.pref_eggplant_seeds, PlayerPrefs.GetInt(ConstLabels.pref_eggplant_seeds) - 1);
        }
        var plant = Instantiate(_seedList[_currentSeed], _player.transform.position, Quaternion.identity);
        _cropList.Add(plant);
        _seedList.RemoveAt(_currentSeed);
        _as.PlayOneShot(_plantCrop);
        _lastPlanted = Time.time;
    }

    public void HarvestCrops()
    {
        int i = 0;
        int originalCount = _cropList.Count;
        while (i < originalCount)
        {
            GameObject crop = _cropList[0];
            _cropList.RemoveAt(0);
            Destroy(crop);
            i++;
        }
        _as.PlayOneShot(_gainCoins);
    }
}
