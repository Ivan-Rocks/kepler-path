using Microsoft.MixedReality.Toolkit.UX;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Controls : MonoBehaviour
{
    public bool paused = false;
    public bool measuring = false;
    //Buttons
    public GameObject ObservationPanel;
    public GameObject Log;
    public GameObject Reload;
    //public GameObject SwitchCam;
    public GameObject Pause;
    public GameObject Measure;
    public GameObject Record;
    public GameObject Reset;
    public GameObject Cancel;
    public GameObject Simulation;

    //Cameras
    //public Camera topview;
    public Camera sideview;
    private Camera activeCamera;
    // Start is called before the first frame update
    void Start()
    {
        sideview.enabled = true;
        //topview.enabled = false;
        activeCamera = sideview;
        //Link to onClick functions
        Log.GetComponent<PressableButton>().OnClicked.AddListener(onShowLog);
        Reload.GetComponent<PressableButton>().OnClicked.AddListener(onReload);
        Pause.GetComponent<PressableButton>().OnClicked.AddListener(onPause);
        Measure.GetComponent<PressableButton>().OnClicked.AddListener(onMeasure);
        Cancel.GetComponent<PressableButton>().OnClicked.AddListener(onCancel);
        //Set Measuring realted buttons
        Measure.GetComponent<PressableButton>().enabled= false;
        ObservationPanel.SetActive(false);
        Record.gameObject.SetActive(false);
        Reset.gameObject.SetActive(false);
        Cancel.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (activeCamera!= null && paused)
        {
            Measure.GetComponent<PressableButton>().enabled = true;
        } else
        {
            Measure.GetComponent<PressableButton>().enabled = false;
        }
    }

    public void onShowLog()
    {
        if (ObservationPanel.activeSelf)
        {
            ObservationPanel.SetActive(false);
        } else
        {
            ObservationPanel.SetActive(true);
        }
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
        //Cancel upper row
        //SwitchCam.gameObject.SetActive(false);
        Pause.gameObject.SetActive(false);
        Measure.gameObject.SetActive(false);
        //Activate lower row
        Record.gameObject.SetActive(true);
        Reset.gameObject.SetActive(true);
        Cancel.gameObject.SetActive(true);
        ObservationPanel.gameObject.SetActive(true);

        Record.GetComponent<PressableButton>().enabled=false;
        Reset.GetComponent<PressableButton>().enabled=true;
        Cancel.GetComponent<PressableButton>().enabled=true;
        measuring = true;
    }

    public void onCancel()
    {
        print("playing mode");
        //Cancel lower row
        Record.gameObject.SetActive(false);
        Reset.gameObject.SetActive(false);
        Cancel.gameObject.SetActive(false);
        ObservationPanel.gameObject.SetActive(false);
        measuring = false;
        //Activate Upper row
        Measure.GetComponent<PressableButton>().enabled = false;
        //SwitchCam.gameObject.SetActive(true);
        Pause.gameObject.SetActive(true);
        Measure.gameObject.SetActive(true);
        //SwitchCam.GetComponent<PressableButton>().enabled = true;
        Pause.GetComponent<PressableButton>().enabled = true;
        Measure.GetComponent<PressableButton>().enabled = false;
    }
}
