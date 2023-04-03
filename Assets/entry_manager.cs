using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class entry_manager : MonoBehaviour
{
    public string start;
    public string end;
    public string distance;
    public GameObject ghost;
    void Start()
    {
    }

    public void onShow()
    {
        print("sorry, not implemented yet");
        //Transform sun = GameObject.Find("Sun").transform;
        //GameObject prefabInstance = Instantiate(ghost);
    }

    public void OnDelete()
    {
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        //three texts
        Transform startTransform = gameObject.transform.Find("Point A");
        TextMeshProUGUI startText = startTransform.gameObject.GetComponent<TextMeshProUGUI>();
        startText.text = start;
        Transform endTransform = gameObject.transform.Find("Point B");
        TextMeshProUGUI endText = endTransform.gameObject.GetComponent<TextMeshProUGUI>();
        endText.text = end;
        Transform distanceTransform = gameObject.transform.Find("Distance");
        TextMeshProUGUI distanceText = distanceTransform.gameObject.GetComponent<TextMeshProUGUI>();
        distanceText.text = distance;
        //two buttons
        Transform showTransform = gameObject.transform.Find("Show");
        Button showButton = showTransform.gameObject.GetComponent<Button>();
        showButton.onClick.AddListener(onShow);
        Transform deleteTransform = gameObject.transform.Find("Delete");
        Button deleteButton = deleteTransform.gameObject.GetComponent<Button>();
        deleteButton.onClick.AddListener(OnDelete);
    }
}
