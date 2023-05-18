using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class entry_manager : MonoBehaviour
{
    public GameObject prefab;
    public GameObject entry_panel;
    //public Dictionary<GameObject, GameObject> objectMap = new Dictionary<GameObject, GameObject>();
    
    void Start()
    {
    }

    public void createEntry(GameObject start, GameObject end, string distance)
    {
        print(start.name+distance);
        GameObject instance = Instantiate(prefab, entry_panel.transform);
        instance.GetComponent<Entry>().Initialize(start, end, distance);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
