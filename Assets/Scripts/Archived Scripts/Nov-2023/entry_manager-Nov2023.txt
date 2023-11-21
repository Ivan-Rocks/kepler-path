using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class entry_manager : MonoBehaviour
{
    public GameObject prefab;
    public GameObject entry_panel;
    public List<GameObject> entries;
    //public Dictionary<GameObject, GameObject> objectMap = new Dictionary<GameObject, GameObject>();
    
    void Start()
    {
    }

    public void createEntry(GameObject start, GameObject end, string distance, float t)
    {
        //print(start.name+distance);
        GameObject instance = Instantiate(prefab, entry_panel.transform);
        instance.GetComponent<Entry>().Initialize(start, end, distance, t);
        entries.Add(instance);
    }

    public void hide()
    {
        foreach (GameObject entry in entries)
        {
            entry.GetComponent<Entry>().Hide();
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
