using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class DayNightScript : MonoBehaviour
{
    public Light2D _globalLight;
    public GameObject _clockHand;

    public bool daytime = true;

    // the length of the day/night cycle
    float _cycleLength = 30.0f;
    // the number of seconds between each time update 
    float _updateTime = 3;

    Color _dayColor = new Color(1.0f, 1.0f, 1.0f);
    Color _nightColor = new Color(0.5f, 0.5f, 0.8f);

    float _redIncrease;
    float _greenIncrease;
    float _blueIncrease;

    float _timePassed;

    Transform _clockHandTransform;
    float _currentHandRotation;
    float _handRotationIncrement;

    float _incrementScale;

    // Start is called before the first frame update
    void Start()
    {
        _incrementScale = _cycleLength / _updateTime;
        _timePassed = 0;
        _redIncrease = (_dayColor.r - _nightColor.r) / _incrementScale;
        _greenIncrease = (_dayColor.g - _nightColor.g) / _incrementScale;
        _blueIncrease = (_dayColor.b - _nightColor.b) / _incrementScale;
        
        _clockHandTransform = _clockHand.transform;
        _currentHandRotation = 0;
        _handRotationIncrement = 180 / _incrementScale;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _timePassed += Time.deltaTime;
        if (_timePassed > _updateTime)
        {
            if (daytime)
            {
                Color temp = new Color(_globalLight.color.r - _redIncrease,
                    _globalLight.color.g - _greenIncrease,
                    _globalLight.color.b - _blueIncrease);
                _globalLight.color = temp;
                if (_globalLight.color.r <= _nightColor.r)
                {
                    daytime = !daytime;
                }
            }
            else
            {
                Color temp = new Color(_globalLight.color.r + _redIncrease,
                    _globalLight.color.g + _greenIncrease,
                    _globalLight.color.b + _blueIncrease);
                _globalLight.color = temp;
                if (_globalLight.color.r >= _dayColor.r)
                {
                    daytime = !daytime;
                }
            }
            _timePassed = 0;
            if (_currentHandRotation >= 360)
            {
                _currentHandRotation = 0;
            }
            _currentHandRotation += _handRotationIncrement;
            _clockHandTransform.eulerAngles = new Vector3(0, 0, -_currentHandRotation);

        }
    }
}
