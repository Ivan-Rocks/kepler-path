using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class entry_manager : MonoBehaviour
{
    //Self Attributes
    public GameObject startObj;
    public GameObject endObj;
    private string start;
    private string end;
    public float distance;
    //Related 
    public int degree;
    private bool visible = false;
    public GameObject prefabInstance;
    public GameObject ghost_prefab;
    private GameObject Attractor;
    private EllipticalOrbit k;
    private GameObject panel;
    //UI Elements
    public TextMeshProUGUI startText;
    public TextMeshProUGUI endText;
    public TextMeshProUGUI distanceText;
    //Dotted Line
    private LineRenderer lineRend;
    void Start()
    {
        start = startObj.name.ToString();
        end = endObj.name.ToString();
        k = GameObject.Find("Planet").GetComponent<EllipticalOrbit>();
        Attractor = GameObject.Find("Sun");
        prefabInstance = Instantiate(ghost_prefab, Attractor.transform);
        prefabInstance.SetActive(false);
        panel = GameObject.Find("Observation Panel");
        //setup
        //three texts
        Transform startTransform = gameObject.transform.Find("Point A");
        startText = startTransform.gameObject.GetComponent<TextMeshProUGUI>();
        startText.text = start;
        Transform endTransform = gameObject.transform.Find("Point B");
        endText = endTransform.gameObject.GetComponent<TextMeshProUGUI>();
        endText.text = end;
        Transform distanceTransform = gameObject.transform.Find("Distance");
        distanceText = distanceTransform.gameObject.GetComponent<TextMeshProUGUI>();
        distanceText.text = distance.ToString();
        //two buttons
        Transform showTransform = gameObject.transform.Find("Show");
        Button showButton = showTransform.gameObject.GetComponent<Button>();
        showButton.onClick.AddListener(onShow);
        Transform deleteTransform = gameObject.transform.Find("Delete");
        Button deleteButton = deleteTransform.gameObject.GetComponent<Button>();
        deleteButton.onClick.AddListener(OnDelete);
        //dotted line
        lineRend = GetComponent<LineRenderer>();
        lineRend.material.mainTextureScale = new Vector2((int)distance,
            1);
        lineRend.positionCount = 0;
    }

    public void onShow()
    {
        print("show");
        if (!visible)
        {
            if (start == "Planet" || end == "Planet")
            {
                Vector3 pos = EllipticalOrbit.ComputePointOnOrbit(k.apoapsis, k.periapsis,
                k.argumentOfPeriapsis, k.inclination, (float)degree / 360);
                Quaternion system_rotation = GameObject.Find("Simulation").transform.rotation;
                pos = system_rotation * pos + Attractor.transform.position;
                prefabInstance.transform.position = pos;
                prefabInstance.SetActive(true);
                //print(pos);
                if (start == "Planet")
                {
                    lineRend.positionCount = 2;
                    lineRend.SetPosition(0, pos);
                    lineRend.SetPosition(1, endObj.transform.position);
                } else
                {
                    lineRend.positionCount = 2;
                    lineRend.SetPosition(0, startObj.transform.position);
                    lineRend.SetPosition(1, pos);
                }
            } else
            {
                lineRend.positionCount = 2;
                lineRend.SetPosition(0, startObj.transform.position);
                lineRend.SetPosition(1, endObj.transform.position);
            }
        } else
        {
            lineRend.positionCount = 0;
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
    }
}
