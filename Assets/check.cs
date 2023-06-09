using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class check : MonoBehaviour
{
    public GameObject header;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int gameObjectCount = UnityEngine.Object.FindObjectsOfType<GameObject>().Length;
        header.GetComponent<TextMeshProUGUI>().text=gameObjectCount.ToString();
    }
}
