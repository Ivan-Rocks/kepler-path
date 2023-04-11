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
    //Related 
    public int degree;
    private bool visible = false;
    public GameObject prefabInstance;
    public GameObject ghost_prefab;
    private GameObject Attractor;
    private EllipticalOrbit k;
    void Start()
    {
        if (start.Contains("Planet")) {
            start = start + " at " + degree.ToString();
        }
        if (end.Contains("Planet"))
        {
            end = end + " at " + degree.ToString();
        }
        k = GameObject.Find("Planet").GetComponent<EllipticalOrbit>();
        Attractor = GameObject.Find("Sun");
        prefabInstance = Instantiate(ghost_prefab, Attractor.transform);
        prefabInstance.SetActive(false);
    }

    public void onShow()
    {
        print("show");
        if (!visible)
        {
            Vector3 pos = EllipticalOrbit.ComputePointOnOrbit(k.apoapsis,k.periapsis,
                k.argumentOfPeriapsis,k.inclination,(float)degree/360);
            prefabInstance.transform.position = pos;
            prefabInstance.SetActive(true);
            print(pos);
        } else
        {
            prefabInstance.SetActive(false);
        }
        visible = !visible;
    }

    public void OnDelete()
    {
        Destroy(prefabInstance);
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
        //visibility of objects

    }
}
