using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class PlantingScript : MonoBehaviour
{
    public DayNightScript _dnScript;

    public GameObject _player;

    public GameObject _CornPrefab;
    public GameObject _EggplantPrefab;
    public GameObject _CarrotPrefab;
    public GameObject _PepperPrefab;
    public GameObject _BeanPrefab;

    public GameObject _weed1Prefab;
    public GameObject _weed2Prefab;
    public GameObject _weed3Prefab;

    public Tilemap _tMap;
    public Tile _dirtTile;

    public AudioClip _gainCoins;
    public AudioClip _plantCrop;

    public LayerMask _plantLayer;


    Dictionary<int, String> _seedDict = new Dictionary<int, String>();
    int _currentSeed;
    int _maxSeedInd;
    bool _radiiHidden;
    AudioSource _as;

    // list of crop objects
    List<GameObject> _seedList;

    // List of crop game objects
    public List<GameObject> _cropList;

    public List<GameObject> _weedList;

    // Start is called before the first frame update
    void Start()
    {
        _as = GetComponent<AudioSource>();

        _cropList = new List<GameObject>();
        _weedList = new List<GameObject>();

        if(PlayerPrefs.GetInt(ConstLabels.pref_corn_seeds) == 0)
        {
            PlayerPrefs.SetInt(ConstLabels.pref_corn_seeds, PlayerPrefs.GetInt(ConstLabels.pref_corn_seeds) + 1);
        }
        if ((PlayerPrefs.GetInt(ConstLabels.pref_pepper_seeds )) == 0)
        {
            PlayerPrefs.SetInt(ConstLabels.pref_pepper_seeds, PlayerPrefs.GetInt(ConstLabels.pref_pepper_seeds) + 1);
        }
        if (PlayerPrefs.GetInt(ConstLabels.pref_carrot_seeds) == 0)
        {
            PlayerPrefs.SetInt(ConstLabels.pref_carrot_seeds, PlayerPrefs.GetInt(ConstLabels.pref_carrot_seeds) + 1);
        }
        if (PlayerPrefs.GetInt(ConstLabels.pref_bean_seeds) == 0)
        {
            PlayerPrefs.SetInt(ConstLabels.pref_bean_seeds, PlayerPrefs.GetInt(ConstLabels.pref_bean_seeds) + 1);
        }
        if (PlayerPrefs.GetInt(ConstLabels.pref_eggplant_seeds) == 0)
        {
            PlayerPrefs.SetInt(ConstLabels.pref_eggplant_seeds, PlayerPrefs.GetInt(ConstLabels.pref_eggplant_seeds) + 1);
        }


        _seedDict.Add(0, ConstLabels.pref_corn_seeds);
        _seedDict.Add(1, ConstLabels.pref_bean_seeds);
        _seedDict.Add(2, ConstLabels.pref_carrot_seeds);
        _seedDict.Add(3, ConstLabels.pref_pepper_seeds);
        _seedDict.Add(4, ConstLabels.pref_eggplant_seeds);

        _currentSeed = 0;
        _maxSeedInd = 4;

        _radiiHidden = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && PlayerPrefs.GetInt(_seedDict[_currentSeed]) > 0)
        {
            if (OnSoil() && AwayFromPlants())
            {
                PlantSeed();
            }
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (_currentSeed < _maxSeedInd)
            {
                _currentSeed += 1;
            }
            else
            {
                _currentSeed = 0;
            }

        }
    }

    // Update is called once per frame
    void LateUpdate()
    {

        if (!_dnScript._daytime && _cropList.Count == 0)
        {
            HandleLoss();
        }

        // turn off plant radii if nightime
        if (!_dnScript._daytime && !_radiiHidden)
        {
            if (_cropList.Count == 0 && OnSoil() && AwayFromPlants())
            {
                PlantSeed();
            }
            foreach (GameObject pl in _cropList)
            {
                Transform rad = pl.transform.Find("Radius");
                rad.GetComponent<SpriteRenderer>().enabled = false;
            }
            _radiiHidden = true;
        }

        // plant weeds randomly
        if (Random.Range(0,1000) == 0 && !_dnScript._daytime && _weedList.Count < 5)
        {
            GrowWeed();
        }

        if (_dnScript._daytime && _weedList.Count > 0)
        {
            int i = 0;
            int originalCount = _weedList.Count;
            while (i < originalCount)
            {
                GameObject weed = _weedList[0];
                _weedList.RemoveAt(0);
                Destroy(weed);
                i++;
            }
        }
    }

    

    void HandleLoss()
    {
        SceneManager.LoadScene(ConstLabels.scene_end);
    }

    bool OnSoil()
    {
        return true;
        /*
        var tile = _tMap.GetTile(new Vector3Int((int)Math.Round(_player.transform.position.x, 0), (int)Math.Round(_player.transform.position.y, 0), (int)Math.Round(_player.transform.position.z, 0)));

        if (tile == _dirtTile)
        {
            return true;

        }
        else
        {
            return false;
            print("not in dirt");
        }
        */
    }

    bool AwayFromPlants()
    {
        RaycastHit2D hit = Physics2D.Raycast(_player.transform.position, new Vector3(0, 0, -1), 20, _plantLayer);
        // RaycastHit2D hitLeft = Physics2D.Raycast(_player.transform.position - new Vector3(_player.transform.position.x / 2, 0, 0), new Vector3(0, 0, 1));
        if (hit.collider == null)
        {
            return true;
        } else
        {
            return false;
        }
    }

    void PlantSeed()
    {
        if (_currentSeed == 0 && PlayerPrefs.GetInt(ConstLabels.pref_corn_seeds) > 0)
        {
            PlayerPrefs.SetInt(ConstLabels.pref_corn_seeds, PlayerPrefs.GetInt(ConstLabels.pref_corn_seeds) - 1);
            var plant = Instantiate(_CornPrefab, _player.transform.position, Quaternion.identity);
            _cropList.Add(plant);
        }
        else if (_currentSeed == 1 && PlayerPrefs.GetInt(ConstLabels.pref_bean_seeds) > 0)
        {
            PlayerPrefs.SetInt(ConstLabels.pref_bean_seeds, PlayerPrefs.GetInt(ConstLabels.pref_bean_seeds) - 1);
            var plant = Instantiate(_BeanPrefab, _player.transform.position, Quaternion.identity);
            _cropList.Add(plant);
        }
        else if (_currentSeed == 2 && PlayerPrefs.GetInt(ConstLabels.pref_carrot_seeds) > 0)
        {
            PlayerPrefs.SetInt(ConstLabels.pref_carrot_seeds, PlayerPrefs.GetInt(ConstLabels.pref_carrot_seeds) - 1);
            var plant = Instantiate(_CarrotPrefab, _player.transform.position, Quaternion.identity);
            _cropList.Add(plant);
        }
        else if (_currentSeed == 3 && PlayerPrefs.GetInt(ConstLabels.pref_pepper_seeds) > 0)
        {
            PlayerPrefs.SetInt(ConstLabels.pref_pepper_seeds, PlayerPrefs.GetInt(ConstLabels.pref_pepper_seeds) - 1);
            var plant = Instantiate(_PepperPrefab, _player.transform.position, Quaternion.identity);
            _cropList.Add(plant);
        }
        else if (_currentSeed == 4 && PlayerPrefs.GetInt(ConstLabels.pref_eggplant_seeds) > 0)
        {
            PlayerPrefs.SetInt(ConstLabels.pref_eggplant_seeds, PlayerPrefs.GetInt(ConstLabels.pref_eggplant_seeds) - 1);
            var plant = Instantiate(_EggplantPrefab, _player.transform.position, Quaternion.identity);
            _cropList.Add(plant);
        }
        _as.PlayOneShot(_plantCrop);
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
        _as.PlayOneShot(_gainCoins, .7f);
    }

    public void GrowWeed()
    {
        var position = new Vector3(Random.Range(-10.5f, 19f), Random.Range(-21f, -2f), 0);
        int weedNum = Random.Range(0, 3);
        if (weedNum == 0)
        {
            var weed = Instantiate(_weed1Prefab, position, Quaternion.identity);
            _weedList.Add(weed);
        } 
        else if (weedNum == 1)
        {
            var weed = Instantiate(_weed2Prefab, position, Quaternion.identity);
            _weedList.Add(weed);
        }
        else
        {
            var weed = Instantiate(_weed3Prefab, position, Quaternion.identity);
            _weedList.Add(weed);
        }

    }
}
