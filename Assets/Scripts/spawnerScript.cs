using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnerScript : MonoBehaviour
{
    public PlantingScript _plantScript;
    public DayNightScript _dnScript;

    public GameObject _pigeonPrefab;
    public GameObject _wolfPrefab;
    public GameObject _snakePrefab;
    public GameObject _sheepPrefab;
    public GameObject _bearPrefab;
    bool _bearSpawned = true;

    System.Random _random;

    int _nightNum;
    int _spawnedTonight;
    bool _hasClearedBoard;

    // Start is called before the first frame update
    void Start()
    {
        _nightNum = PlayerPrefs.GetInt(ConstLabels.pref_currentDay);
        _spawnedTonight = 0;
        _random = new System.Random();
    }

    // Update is called once per frame
    void Update()
    {
        if (_nightNum == 14 || _nightNum == 21)
        {
            _bearSpawned = true;
        }
        if ((!_dnScript._daytime) && (_spawnedTonight < (_nightNum * 2) + 15) && (_random.Next(0, 320) == 0)) 
        {
            int r = _random.Next(0, 100);
            
            if ((_nightNum % 7 == 0) && _bearSpawned)
            {
                _bearSpawned = false;
                bearSpawn(_bearPrefab);
                
            }
            else if (r < 25)
            {
                runSpawn(_sheepPrefab);
            }
            else if (r < 45)
            {
                runSpawn(_wolfPrefab);
            }
            else if (r < 75)
            {
                runSpawn(_pigeonPrefab);
            }
            else
            {
                runSpawn(_snakePrefab);
            }
            _spawnedTonight++;
        }
        if (_dnScript._daytime && !_hasClearedBoard)
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag(ConstLabels.tag_enemy);
            for (int i = 0; i < enemies.Length; i++)
            {
                Destroy(enemies[i]);
            }
            _hasClearedBoard = true;
        }
    }

    void spawnEnemy(GameObject toSpawn, float x, float y)
    {
        Vector2 pos = new Vector2(x, y);
        GameObject clone = Instantiate(toSpawn, pos, Quaternion.identity);
        clone.GetComponent<EnemyScript>()._manager = gameObject;
        clone.GetComponent<EnemyScript>()._plantScript = _plantScript;
        if (clone.GetComponent<SnakeScript>() != null)
        {
            clone.GetComponent<SnakeScript>()._manager = gameObject;
            clone.GetComponent<SnakeScript>()._plantScript = _plantScript;
        }
    }

    void runSpawn(GameObject toSpawn)
    {
        float spot = _random.Next(1, 4);
        if(spot == 1)
        {
            float x = _random.Next(-9, -3);
            float y = _random.Next(-21, -3);
            spawnEnemy(toSpawn, x, y);
        }
        else if(spot == 2)
        {
            float x = _random.Next(12, 19);
            float y = _random.Next(-21, -3);
            spawnEnemy(toSpawn, x, y);
        }
        else if (spot == 3)
        {
            float x = _random.Next(-9, 19);
            float y = _random.Next(-21, -15);
            spawnEnemy(toSpawn, x, y);
        }
        else if (spot == 4)
        {
            float x = _random.Next(-9, 19);
            float y = _random.Next(-9, -3);
            spawnEnemy(toSpawn, x, y);
        }
    }

    void bearSpawn(GameObject toSpawn)
    {
        float spot = _random.Next(1, 4);
        if (spot == 1)
        {
            float y = _random.Next(-21, -3);
            spawnEnemy(toSpawn, -9, y);
        }
        else if (spot == 2)
        {
            float y = _random.Next(-21, -3);
            spawnEnemy(toSpawn, 19, y);
        }
        else if (spot == 3)
        {
            float x = _random.Next(-9, 19);
            spawnEnemy(toSpawn, x, -21);
        }
        else if (spot == 4)
        {
            float x = _random.Next(-9, 19);
            spawnEnemy(toSpawn, x, -5);
        }
    }
}
