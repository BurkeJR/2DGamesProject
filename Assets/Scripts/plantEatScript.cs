using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plantEatScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    

    IEnumerator killPlant(GameObject toDestroy)
    {
        new WaitForSeconds(5);
        Destroy(toDestroy);
        return null;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            StartCoroutine(killPlant(gameObject));
        }
    }
}
