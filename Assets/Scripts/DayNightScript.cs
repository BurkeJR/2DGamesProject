using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Experimental.Rendering.Universal;
using System.Drawing;

public class DayNightScript : MonoBehaviour
{
    public Light2D _globalLight;
    public GameObject _clockHand;

    //public WeaponParent _weapon;
    //public bool _ammoReset;
    public Text dayNumber;
    public Text ammo;
    public Text coins;

    public bool _daytime;

    // the length of the day/night cycle
    float _cycleLength = 100.0f;
    public float _timePassed;
    // the number of seconds between each time update 
    float _updateInterval = 3;
    public float _lastUpdate;

    UnityEngine.Color _dayColor = new UnityEngine.Color(1.0f, 1.0f, 1.0f);
    UnityEngine.Color _nightColor = new UnityEngine.Color(0.5f, 0.5f, 0.8f);

    float _redIncrease;
    float _greenIncrease;
    float _blueIncrease;



    Transform _clockHandTransform;
    float _currentHandRotation;
    float _handRotationIncrement;

    float _incrementScale;

    // Start is called before the first frame update
    void Start()
    {
        _timePassed = PlayerPrefs.GetFloat(ConstLabels.pref_timePassed);
        _lastUpdate = PlayerPrefs.GetFloat(ConstLabels.pref_lastUpdate);
        _globalLight.color = new UnityEngine.Color(PlayerPrefs.GetFloat(ConstLabels.pref_light_r), 
            PlayerPrefs.GetFloat(ConstLabels.pref_light_g),
            PlayerPrefs.GetFloat(ConstLabels.pref_light_b));

        _daytime = _timePassed / _cycleLength <= 0.5f;

        _incrementScale = _cycleLength / _updateInterval / 2;
        _redIncrease = (_dayColor.r - _nightColor.r) / _incrementScale;
        _greenIncrease = (_dayColor.g - _nightColor.g) / _incrementScale;
        _blueIncrease = (_dayColor.b - _nightColor.b) / _incrementScale;

        _clockHandTransform = _clockHand.transform;
        _handRotationIncrement = 180 / _incrementScale;
        _currentHandRotation = 360 * (_timePassed/_cycleLength);

        _clockHandTransform.eulerAngles = new Vector3(0, 0, -_currentHandRotation);
    }

    private void Update()
    {
        ammo.text = PlayerPrefs.GetInt(ConstLabels.pref_player_ammo).ToString();
        coins.text = PlayerPrefs.GetInt(ConstLabels.pref_player_currency).ToString();
        dayNumber.text = "Day " + PlayerPrefs.GetInt(ConstLabels.pref_currentDay);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _lastUpdate += Time.deltaTime;
        _timePassed += Time.deltaTime;
        if (_timePassed > _cycleLength)
        {
            _timePassed -= _cycleLength;
            PlayerPrefs.SetInt(ConstLabels.pref_currentDay, PlayerPrefs.GetInt(ConstLabels.pref_currentDay) + 1);
        }

        //if (PlayerPrefs.GetInt(ConstLabels.pref_currentDay) < ((int)(_timePassed / _cycleLength)))
        //{

        //}

        _daytime = _timePassed / _cycleLength <= 0.5f;
        if (_lastUpdate > _updateInterval)
        {

            if (_daytime)
            {
                _globalLight.color -= new UnityEngine.Color(_redIncrease, _greenIncrease, _blueIncrease);
            }
            else
            {
                _globalLight.color += new UnityEngine.Color(_redIncrease, _greenIncrease, _blueIncrease);
            }

            

            _lastUpdate = 0;

            if (_currentHandRotation >= 360)
            {
                _currentHandRotation = 0;
            }
            _currentHandRotation += _handRotationIncrement;
            _clockHandTransform.eulerAngles = new Vector3(0, 0, -_currentHandRotation);
        }
    }

}
