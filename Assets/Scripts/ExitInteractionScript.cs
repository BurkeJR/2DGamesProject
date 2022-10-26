using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitInteractionScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        string sceneName = SceneManager.GetActiveScene().name;

        // TODO: carry over day/night cycle info

        if (sceneName == ConstLabels.scene_farm)
        {
            SceneManager.LoadScene(ConstLabels.scene_shop);
        } 
        else if (sceneName == ConstLabels.scene_shop)
        {
            SceneManager.LoadScene(ConstLabels.scene_farm);
        }
    }
}
