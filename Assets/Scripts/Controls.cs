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
    public GameObject ObservationPanel;
    public GameObject Reload;
    public GameObject Log;
    public GameObject Pause;
    public GameObject Measure;
    public GameObject Record;
    public GameObject Reset;
    public GameObject Cancel;
    public GameObject[] transparent_objects = { };

    // Start is called before the first frame update
    void Start()
    {
        //Link to onClick functions
        Log.GetComponent<PressableButton>().OnClicked.AddListener(onShowLog);
        Reload.GetComponent<PressableButton>().OnClicked.AddListener(onReload);
        Pause.GetComponent<PressableButton>().OnClicked.AddListener(onPause);
        Measure.GetComponent<PressableButton>().OnClicked.AddListener(onMeasure);
        Cancel.GetComponent<PressableButton>().OnClicked.AddListener(onCancel);
        //Set Measuring realted buttons
        Measure.GetComponent<PressableButton>().enabled= false;
        Log.SetActive(false);
        ObservationPanel.SetActive(false);
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
        ObjectManipulator objectManipulator = gameObject.GetComponent<ObjectManipulator>();
        objectManipulator.enabled = !measuring;
        Measure.GetComponent<PressableButton>().enabled = paused;
    }

    public void onShowLog()
    {
        ObservationPanel.SetActive(!ObservationPanel.activeSelf);
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
        ObservationPanel.gameObject.SetActive(true);
        //Cancel lower row
        Record.GetComponent<PressableButton>().enabled=false;
        Reset.GetComponent<PressableButton>().enabled=true;
        Cancel.GetComponent<PressableButton>().enabled=true;
        measuring = true;
        //Activate transparent objects
        for (int i = 0; i < transparent_objects.Length; i++)
            transparent_objects[i].SetActive(true);
    }

    public void onCancel()
    {
        print("playing mode");
        //Cancel lower row
        Log.gameObject.SetActive(false);
        Record.gameObject.SetActive(false);
        Reset.gameObject.SetActive(false);
        Cancel.gameObject.SetActive(false);
        ObservationPanel.SetActive(false);
        measuring = false;
        //Activate Upper row
        Measure.GetComponent<PressableButton>().enabled = false;
        //SwitchCam.gameObject.SetActive(true);
        Pause.gameObject.SetActive(true);
        Measure.gameObject.SetActive(true);
        //SwitchCam.GetComponent<PressableButton>().enabled = true;
        Pause.GetComponent<PressableButton>().enabled = true;
        Measure.GetComponent<PressableButton>().enabled = false;
        //Deactivate transparent objects
        for (int i = 0; i < transparent_objects.Length; i++)
            transparent_objects[i].SetActive(false);
    }
}
