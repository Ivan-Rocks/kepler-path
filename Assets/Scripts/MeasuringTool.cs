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
    public GameObject planet;
    public GameObject hand;
    public TextMeshProUGUI distanceText;
    private EllipticalOrbit k;
    private LineRenderer lineRend;
    public float distance;
    public GameObject Record;
    public GameObject Reset;
    public GameObject Cancel;
    private int record_status = 0;
    private GameObject start;
    private GameObject end;

    // Start is called before the first frame update
    void Start()
    {
        //bind buttons
        Record.GetComponent<PressableButton>().OnClicked.AddListener(onRecord);
        Reset.GetComponent<PressableButton>().OnClicked.AddListener(onReset);
        Cancel.GetComponent<PressableButton>().OnClicked.AddListener(onCancel);
        //initialize settings
        Record.GetComponent<PressableButton>().enabled = false;
        k = planet.GetComponent<EllipticalOrbit>();
        lineRend = planet.GetComponent<LineRenderer>();
        lineRend.positionCount = 0;
    }

    public void onRecord()
    {
        print("record");

        gameObject.GetComponent<entry_manager>().createEntry(start, end, distance.ToString("F3")
            , (float)gameObject.GetComponent<EllipticalOrbit>().degree/360);
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
        Record.GetComponent<PressableButton>().enabled = false;
    }

    public void onCancel()
    {
        print("cancel");
        record_status = 0;
        lineRend.positionCount = 0;
        lineRend.positionCount = 2;
        distanceText.text = "";
        distance = 0;
        start = null;
        end = null;
        Record.GetComponent<PressableButton>().enabled = false;
        gameObject.GetComponent<entry_manager>().hide();
    }

    public bool isLegalPress(GameObject obj)
    {
        if (obj == null)
            return false;
        if (obj.name == "Sun" || obj.name == "A1" || obj.name == "B1" || 
            obj.name == "Planet" || obj.name == "Focus")
            return true;
        return false;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.GetComponent<Controls>().measuring)
        {
            Transform x = hand.GetComponent<MRTKRayInteractor>().rayOriginTransform;
            Reset.GetComponent<PressableButton>().enabled = true;
            //First Press
            if (hand.GetComponent<MRTKRayInteractor>().isSelectActive && x != null && record_status == 0)
            {
                Vector3 rayDirection = x.forward;
                RaycastHit hit;
                if (Physics.Raycast(x.position, rayDirection, out hit, 150) && isLegalPress(hit.collider.gameObject))
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
                if (Physics.Raycast(x.position, rayDirection, out hit, 150) &&
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
                lineRend.startWidth = gameObject.transform.localScale.x/10;
                lineRend.endWidth = gameObject.transform.localScale.x / 10;
                lineRend.SetPosition(0, start.transform.position);
                lineRend.SetPosition(1, end.transform.position);
                Record.GetComponent<PressableButton>().enabled = true;
            }
        }
    }
}
