using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnerScript : MonoBehaviour
{
    public PlantingScript _plantScript;

    public GameObject _pigeonPrefab;
    public GameObject _wolfPrefab;
    public GameObject _snakePrefab;
    public GameObject _sheepPrefab;
    public GameObject _bearPrefab;
    int nightNum;
    int nightMult;
    // Start is called before the first frame update
    void Start()
    {
        nightNum = PlayerPrefs.GetInt(ConstLabels.pref_currentDay);
        nightMult = 0;
        Debug.Log(nightNum);
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = nightMult; i < nightNum; i++)
        {
            StartCoroutine(runSpawn(_snakePrefab));
            nightMult++;
        }
    }

    private int spawnEnemy(GameObject toSpawn, float x, float y)
    {
        System.Random rand = new System.Random();
        Vector2 pos = new Vector2(x, y);
        GameObject clone = Instantiate(toSpawn, pos, Quaternion.identity);
        clone.GetComponent<EnemyScript>()._manager = this.gameObject;
        clone.GetComponent<EnemyScript>()._plantScript = _plantScript;
        return nightNum;
    }

    IEnumerator runSpawn(GameObject toSpawn)
    {
       System.Random rand = new System.Random();
        float wait = rand.Next(1, 5);
        float x = rand.Next(-9, 19);
        float y = rand.Next(-21, -3);
        yield return new WaitForSeconds(wait);
        spawnEnemy(toSpawn, x, y);
    }
}
