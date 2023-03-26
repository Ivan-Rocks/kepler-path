using Microsoft.MixedReality.Toolkit.UX;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.UI;

public class MeasuringTool : MonoBehaviour
{
    public GameObject planet;
    public TextMeshProUGUI distanceText;
    private EllipticalOrbit k;
    private LineRenderer lineRend;
    public float distance;
    public GameObject btn;
    private int record_status = 0;
    private GameObject start;
    private GameObject end;
    // Start is called before the first frame update
    void Start()
    {
        k = planet.GetComponent<EllipticalOrbit>();
        lineRend = GetComponent<LineRenderer>();
        lineRend.positionCount = 0;
    }

    public void onReset()
    {
        print("reset");
        record_status = 0;
        lineRend.positionCount = 0;
        lineRend.positionCount = 2;
        start = null;
        end = null;
    }

    // Update is called once per frame
    void Update()
    {
        Camera cam = Camera.current.GetComponent<Camera>();
        if (cam.name == "Topview" && k.paused)
        {
            btn.GetComponent<PressableButton>().enabled = true;
            if (Input.GetMouseButton(0) && record_status == 0)
            {
                Ray ray = Camera.current.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 100))
                {
                    start = hit.transform.gameObject;
                    record_status++;
                    print("first" + start.name);
                }
            }
            if (Input.GetMouseButton(0) && record_status == 1)
            {
                Ray ray = Camera.current.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 100) && !hit.transform.gameObject.Equals(start))
                {
                    end = hit.transform.gameObject;
                    record_status++;
                    print("second" + end.name);
                    float distance = (start.transform.position - end.transform.position).magnitude;
                    distanceText.text = distance.ToString("f3");
                }
            }

            if (start != null && end != null && record_status ==2)
            {
                lineRend.positionCount = 2;
                lineRend.SetPosition(0, start.transform.position);
                lineRend.SetPosition(1, end.transform.position);
            }
        }
    }
}
