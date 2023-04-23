using Microsoft.MixedReality.Toolkit.UX;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class MeasuringTool : MonoBehaviour
{
    public GameObject controls;
    public GameObject planet;
    public TextMeshProUGUI distanceText;
    private EllipticalOrbit k;
    private LineRenderer lineRend;
    public float distance;
    public GameObject record_btn;
    public GameObject reset_btn;
    public GameObject cancel_btn;
    private int record_status = 0;
    private GameObject start;
    private GameObject end;
    public GameObject prefab;
    public Transform panel;

    // Start is called before the first frame update
    void Start()
    {
        record_btn.GetComponent<PressableButton>().enabled = false;
        k = planet.GetComponent<EllipticalOrbit>();
        lineRend = GetComponent<LineRenderer>();
        lineRend.positionCount = 0;
    }

    public void onRecord()
    {
        print("record");
        GameObject prefabInstance = Instantiate(prefab, panel);
        entry_manager entry = prefabInstance.GetComponent<entry_manager>();
        entry.startObj = start;
        entry.endObj = end;
        entry.distance = distance;
        entry.degree = k.degree;
        //print(start.name);
        //print(end.name);
        //print(distance.ToString());
    }
    public void onReset()
    {
        print("reset");
        record_status = 0;
        lineRend.positionCount = 0;
        lineRend.positionCount = 2;
        distanceText.text = "";
        start = null;
        end = null;
        record_btn.GetComponent<PressableButton>().enabled = false;
    }

    public void onCancel()
    {
        print("cancel");
        record_status = 0;
        lineRend.positionCount = 0;
        lineRend.positionCount = 2;
        distanceText.text = "";
        start = null;
        end = null;
        record_btn.GetComponent<PressableButton>().enabled = false;
    }

    public bool isLegalPress(GameObject obj)
    {
        if (obj == null)
            return false;
        if (obj.name == "Sun" || obj.name == "A1" || obj.name == "B1" || 
            obj.name == "Planet" || obj.name == "Fictional Focus")
            return true;
        return false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Camera.current != null && controls.GetComponent<Controls>().measuring)
        {
            Camera cam = Camera.current.GetComponent<Camera>();
            reset_btn.GetComponent<PressableButton>().enabled = true;
            //First Press
            if (Input.GetMouseButton(0) && record_status == 0)
            {
                Ray ray = Camera.current.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 100) && 
                    isLegalPress(hit.transform.gameObject))
                {
                    start = hit.transform.gameObject;
                    record_status++;
                    print("first" + start.name);
                }
            }
            //Second Press
            if (Input.GetMouseButton(0) && record_status == 1)
            {
                Ray ray = Camera.current.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 100) && 
                    !hit.transform.gameObject.Equals(start) && 
                    isLegalPress(hit.transform.gameObject))
                {
                    end = hit.transform.gameObject;
                    if (isLegalPress(end))
                    record_status++;
                    print("second" + end.name);
                    distance = (start.transform.position - end.transform.position).magnitude;
                    distance /= GameObject.Find("Simulation").transform.localScale.x;
                    distanceText.text = distance.ToString("f3");
                }
            }
            //Can record
            if (start != null && end != null && record_status == 2)
            {
                lineRend.positionCount = 2;
                lineRend.SetPosition(0, start.transform.position);
                lineRend.SetPosition(1, end.transform.position);
                record_btn.GetComponent<PressableButton>().enabled = true;
            }
        }
    }
}
