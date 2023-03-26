using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DrawLine : MonoBehaviour
{

    private LineRenderer l;
    public GameObject planet;
    public GameObject sun;
    public float distance;
    public TextMeshProUGUI distanceText;
    // Start is called before the first frame update
    void Start()
    {
        l = GetComponent<LineRenderer>();
        l.positionCount = 2;
}

    // Update is called once per frame
    void Update()
    {
        l.SetPosition(0, planet.transform.position);
        l.SetPosition(1, sun.transform.position);
        distance = (planet.transform.position - sun.transform.position).magnitude;
        distanceText.text = distance.ToString("f3");
    }
}
