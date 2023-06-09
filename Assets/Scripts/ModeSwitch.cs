using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ModeSwitch : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    public void switchMode()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        if( currentScene.name== "StartNew")
        {
            SceneManager.LoadScene("Simulation");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
