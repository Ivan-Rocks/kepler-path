using Microsoft.MixedReality.Toolkit.SpatialManipulation;
using Microsoft.MixedReality.Toolkit.UX;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Controls : MonoBehaviour
{
    [NonSerialized] public bool paused = false;
    [NonSerialized] public bool measuring = false;
    //Buttons
    public GameObject RadarPanel;
    public GameObject ObsPanel;
    public GameObject Reload;
    public GameObject Observe;
    public GameObject Log;
    public GameObject Pause;
    public GameObject Measure;
    public GameObject Record;
    public GameObject Reset;
    public GameObject Cancel;
    public GameObject Simulation;

    public GameObject[] transparent_objects = { };
    public GameObject[] transparent_toggles = { };

    // Start is called before the first frame update
    void Start()
    {
        //Link to onClick functions
        Log.GetComponent<PressableButton>().OnClicked.AddListener(onShowLog);
        Reload.GetComponent<PressableButton>().OnClicked.AddListener(onReload);
        Pause.GetComponent<PressableButton>().OnClicked.AddListener(onPause);
        Measure.GetComponent<PressableButton>().OnClicked.AddListener(onMeasure);
        Cancel.GetComponent<PressableButton>().OnClicked.AddListener(onCancel);
        Record.GetComponent<PressableButton>().OnClicked.AddListener(detectFirstRecord);
        //Initializing components
        Simulation.GetComponent<ObjectManipulator>().enabled = false;
        print(Simulation.GetComponent<ObjectManipulator>().enabled);
        print(Simulation.GetComponent<ObjectManipulator>().enabled);
        Measure.SetActive(false);
        Pause.GetComponent<PressableButton>().enabled= false;
        Observe.SetActive(false);
        Reload.GetComponent<PressableButton>().enabled= false;
        Log.SetActive(false);
        RadarPanel.SetActive(false);
        Record.gameObject.SetActive(false);
        Reset.gameObject.SetActive(false);
        Cancel.gameObject.SetActive(false);
        //Set some objects inactie
        for (int i = 0; i < transparent_objects.Length; i++)
            transparent_objects[i].SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("Dialogue Manager").GetComponent<DialogueManager>().phase >=9)
        {
            ObjectManipulator objectManipulator = gameObject.GetComponent<ObjectManipulator>();
            objectManipulator.enabled = !measuring;
            Measure.GetComponent<PressableButton>().enabled = paused;
        }
        if (GameObject.Find("Dialogue Manager").GetComponent<DialogueManager>().phase ==10 && 
            gameObject.GetComponent<MeasuringTool>().record_status==2)
        {
            detectFirstMeasurement();
        }
    }

    public void onShowLog()
    {
        RadarPanel.SetActive(!RadarPanel.activeSelf);
    }
    public void onReload()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void onPause()
    {
        paused = !paused;
    }

    public void onMeasure()
    {
        print("measuring mode");
        Pause.gameObject.SetActive(false);
        Measure.gameObject.SetActive(false);
        //Activate lower row
        Log.gameObject.SetActive(true);
        Record.gameObject.SetActive(true);
        Reset.gameObject.SetActive(true);
        Cancel.gameObject.SetActive(true);
        RadarPanel.gameObject.SetActive(true);
        //Cancel lower row
        Record.GetComponent<PressableButton>().enabled=false;
        Reset.GetComponent<PressableButton>().enabled=true;
        Cancel.GetComponent<PressableButton>().enabled = true;
        Observe.GetComponent<PressableButton>().enabled = false;
        //deactive obspanel
        ObsPanel.gameObject.SetActive(false);
        measuring = true;
        //Activate transparent objects
        for (int i = 0; i < transparent_objects.Length; i++)
            transparent_objects[i].SetActive(true);
        //Set box collider inactive when measuring
        Simulation.GetComponent<BoxCollider>().enabled=false;
    }

    public void onCancel()
    {
        print("playing mode");
        //De-toggle all the toggle buttons
        for (int i=0; i < transparent_toggles.Length; i++)
            if (transparent_toggles[i].GetComponent<PressableButton>().IsToggled)
                transparent_toggles[i].GetComponent<PressableButton>().ForceSetToggled(false, false);
        //Cancel lower row
        Log.gameObject.SetActive(false);
        Record.gameObject.SetActive(false);
        Reset.gameObject.SetActive(false);
        Cancel.gameObject.SetActive(false);
        RadarPanel.SetActive(false);
        measuring = false;
        //Activate Upper row
        Measure.GetComponent<PressableButton>().enabled = false;
        //SwitchCam.gameObject.SetActive(true);
        Pause.gameObject.SetActive(true);
        Measure.gameObject.SetActive(true);
        //SwitchCam.GetComponent<PressableButton>().enabled = true;
        Pause.GetComponent<PressableButton>().enabled = true;
        Measure.GetComponent<PressableButton>().enabled = false;
        Observe.GetComponent<PressableButton>().enabled = true;
        //Deactivate transparent objects
        for (int i = 0; i < transparent_objects.Length; i++)
            transparent_objects[i].SetActive(false);
        //Set box collider inactive when measuring
        Simulation.GetComponent<BoxCollider>().enabled = true;
    }


    public void enterPlayMode()
    {
        Pause.GetComponent <PressableButton>().enabled = true;
    }
    public void enterInteractionMode()
    {
        gameObject.GetComponent<ObjectManipulator>().enabled = true;
    }

    public bool first_manipulation_detected = false;
    public void detectFirstManipulation()
    {
        first_manipulation_detected=true;
    }

    public void enterObserveMode()
    {
        Observe.SetActive(true);
    }

    public void enterMeasuringMode()
    {
        Measure.SetActive(true);
    }

    public bool selectAllToggles()
    {
        for (int i = 0; i < transparent_toggles.Length; i++)
            if (!transparent_toggles[i].GetComponent<PressableButton>().IsToggled)
                return false;
        ObsPanel.SetActive(false);
        return true;
    }

    public bool first_measurement_detected = false;
    public void detectFirstMeasurement()
    {
        first_measurement_detected = true;
    }

    public bool first_record_detected = false;
    public void detectFirstRecord()
    {
        first_record_detected = true;
    }
}
