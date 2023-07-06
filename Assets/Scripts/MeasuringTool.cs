using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.UX;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class MeasuringTool : MonoBehaviour
{
    public GameObject planet;
    public GameObject hand;
    private EllipticalOrbit k;
    private LineRenderer lineRend;
    public float distance;
    public GameObject Record;
    public GameObject Reset;
    public GameObject Cancel;
    public int record_status = 0;
    private GameObject start;
    private GameObject end;
    public GameObject Data;

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
        bool measuring;
        if (SceneManager.GetActiveScene().name.Contains("Dialogue"))
            measuring = gameObject.GetComponent<ControlsWithDialogue>().measuring;
        else
            measuring = gameObject.GetComponent<Controls>().measuring;
        if (measuring)
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
                    GameObject notice = GameObject.Find("Selection Notice");
                    notice.GetComponent<SelectionNotice>().setText(start);
                    print("first" + start.name);
                    Data.GetComponent<RecordActions>().recordHit(start);
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
                    {
                        record_status++;
                        print("second" + end.name);
                        distance = (start.transform.position - end.transform.position).magnitude;
                        distance /= GameObject.Find("Simulation").transform.localScale.x;
                        GameObject notice = GameObject.Find("Selection Notice");
                        notice.GetComponent<SelectionNotice>().setText(end);
                        Data.GetComponent<RecordActions>().recordHit(end);
                    }
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
