using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreenManagerScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("ToTitle", 5);
    }

    void ToTitle()
    {
        SceneManager.LoadScene(ConstLabels.menu_scene);
    }
}
