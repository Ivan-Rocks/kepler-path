using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class testing : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameObject[] gameObjects = FindObjectsOfType<GameObject>();
        int gameObjectCount = gameObjects.Length;
        gameObject.GetComponent<TextMeshProUGUI>().text = gameObjectCount.ToString();
    }
}
