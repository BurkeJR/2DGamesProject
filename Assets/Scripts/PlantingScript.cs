using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantingScript : MonoBehaviour
{
    public GameObject _player;

    public GameObject _CornPrefab;
    public GameObject _EggplantPrefab;
    public GameObject _CarrotPrefab;
    public GameObject _PepperPrefab;
    public GameObject _BeanPrefab;



    int _currentSeed;
    float _lastPlanted;

    // list of crop objects
    List<GameObject> _seedList;

    // tuples of crop game objects and the float of the time they were created.
    List<Tuple<GameObject, float>> _cropList;

    List<Tuple<GameObject, float>> _animalList;


    // Start is called before the first frame update
    void Start()
    {
        _cropList = new List<Tuple<GameObject,float>>();
        _animalList = new List<Tuple<GameObject, float>>();
        _seedList = new List<GameObject>();

        // _cornObject = Instantiate(_CornPrefab);
        _seedList.Add(_CornPrefab);
        _seedList.Add(_EggplantPrefab);
        _seedList.Add(_CarrotPrefab);
        _seedList.Add(_PepperPrefab);
        _seedList.Add(_BeanPrefab);
        _currentSeed = 0;
        _lastPlanted = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.F) && _seedList.Count > 0) {
            if (OnSoil() && Time.time - _lastPlanted > 1)
            {
                var plant = Instantiate(_seedList[_currentSeed], _player.transform.position, Quaternion.identity);
                _cropList.Add(Tuple.Create(plant, Time.time));
                _seedList.RemoveAt(_currentSeed);
                print(_seedList.Count);
                _lastPlanted = Time.time;
            }
        }


    }

    bool OnSoil()
    {
        return true;
    }
}
