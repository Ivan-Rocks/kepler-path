using Microsoft.MixedReality.Toolkit.Input;
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
    public GameObject hand;
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
    public GameObject entry_prefab;
    public Transform panel;

    //temporary place for texts
    public TextMeshProUGUI obja;
    public TextMeshProUGUI objb;
    public TextMeshProUGUI dist;

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
        obja.text = start.name;
        objb.text = end.name;
        dist.text = distance.ToString();
        /*GameObject prefabInstance = Instantiate(entry_prefab, panel);
        entry_manager entry = prefabInstance.GetComponent<entry_manager>();
        entry.startObj = start;
        entry.endObj = end;
        entry.distance = distance;
        entry.degree = k.degree;*/
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
        if (controls.GetComponent<Controls>().measuring)
        {
            Transform x = hand.GetComponent<MRTKRayInteractor>().rayOriginTransform;
            reset_btn.GetComponent<PressableButton>().enabled = true;
            //First Press
            if (hand.GetComponent<MRTKRayInteractor>().isSelectActive && x != null && record_status == 0)
            {
                Vector3 rayDirection = x.forward;
                RaycastHit hit;
                if (Physics.Raycast(x.position, rayDirection, out hit, 50) && isLegalPress(hit.collider.gameObject))
                {
                    start = hit.collider.gameObject;
                    record_status++;
                    print("first" + start.name);
                }
            }
            //Second Press
            if (hand.GetComponent<MRTKRayInteractor>().isSelectActive && x != null && record_status == 1)
            {
                Vector3 rayDirection = x.forward;
                RaycastHit hit;
                if (Physics.Raycast(x.position, rayDirection, out hit, 50) &&
                    !hit.collider.gameObject.Equals(start) && 
                    isLegalPress(hit.collider.gameObject))
                {
                    end = hit.collider.gameObject;
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
