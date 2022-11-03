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

        Debug.Log("night num:" + _nightNum);
    }

    // Update is called once per frame
    void Update()
    {
        if ((!_dnScript._daytime) && (_spawnedTonight < (_nightNum * 2) + 15) && (_random.Next(0, 320) == 0)) 
        {
            int r = _random.Next(0, 100);
            if (r < 5)
            {
                runSpawn(_bearPrefab);
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
    }

    void runSpawn(GameObject toSpawn)
    {
        float wait = _random.Next(1, 5);
        float x = _random.Next(-9, 19);
        float y = _random.Next(-21, -3);
        spawnEnemy(toSpawn, x, y);
    }
}
