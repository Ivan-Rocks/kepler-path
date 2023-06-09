using Microsoft.MixedReality.Toolkit.UX;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.IO;
using static UnityEngine.InputSystem.LowLevel.InputStateHistory;
using Unity.VisualScripting;
using Microsoft.MixedReality.Toolkit.SpatialManipulation;

public class DialogueManager : MonoBehaviour
{
    public GameObject Simulation;
    public GameObject Dialogue;
    public GameObject Header;
    public GameObject MainText;
    public int phase = 0;
    public int total_phases=12;
    //public string csvFilePath = "Assets/Scripts/Dialogues.csv";
    //Strings
    public List<string> headers = new List<string>();
    public List<string> texts = new List<string>();
    // Start is called before the first frame update
    void Start()
    {
        //ReadCSVFile();
        setDialogue();
    }

    /*
    public void ReadCSVFile()
    {
        StreamReader reader = new StreamReader(csvFilePath);

        while (!reader.EndOfStream)
        {
            total_phases++;
            string line = reader.ReadLine();
            string[] values = line.Split(';');
            //int x = int.Parse(values[0]);
            headers.Add(values[1]);
            texts.Add(values[2]);
        }
        print(total_phases);
        reader.Close();
        //print(headers);
    }*/

    public void setDialogue()
    {
        print(phase);
        Dialogue.SetActive(true);
        Header.GetComponent<TextMeshProUGUI>().text = headers[phase];
        MainText.GetComponent<TextMeshProUGUI>().text= texts[phase];
    }

    public void onContinue()
    {
        //if we reach a state and the action has not been finished
        //we make the action button clickable, if it has been done, it will call back and 
        if (phase ==3 && !Simulation.GetComponent<Controls>().first_manipulation_detected)
        {
            Simulation.GetComponent<Controls>().enterInteractionMode();
            Dialogue.SetActive(false);
            return;
        }
        if (phase == 5 && !Simulation.GetComponent<Controls>().paused)
        {
            Simulation.GetComponent<Controls>().enterPlayMode();
            Dialogue.SetActive(false);
            return;
        }
        if (phase ==8 && !Simulation.GetComponent<Controls>().selectAllToggles())
        {
            Simulation.GetComponent<Controls>().enterObserveMode();
            Dialogue.SetActive(false);
            return;
        }
        if (phase ==9 && !Simulation.GetComponent<Controls>().measuring)
        {
            Simulation.GetComponent<Controls>().enterMeasuringMode();
            Dialogue.SetActive(false);
            return;
        }
        if (phase==10 && !Simulation.GetComponent<Controls>().first_measurement_detected)
        {
            Dialogue.SetActive(false);
            return;
        }
        if(phase==11 && !Simulation.GetComponent<Controls>().first_record_detected)
        {
            Dialogue.SetActive(false);
            return;
        }
        //automatically goes to the next state
        if (phase < total_phases - 1)
            phase++;
        Dialogue.SetActive(false);
        setDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        //have to constantly check for updates on paused or other features to make sure we go to the next step
        //it automatically changes to onContinue with a status that current task is finished -> e.g. .paused
        //so the if statement on top can be bypassed
        if (phase == 3 && Simulation.GetComponent<Controls>().first_manipulation_detected)
        {
            onContinue();
        }
        if (phase==5 && Simulation.GetComponent<Controls>().paused)
        {
            onContinue();
        }
        if (phase==8 && Simulation.GetComponent<Controls>().selectAllToggles())
        {
            onContinue();
        }
        if (phase==9 && Simulation.GetComponent<Controls>().measuring)
        {
            onContinue();
        }
        if (phase==10 && Simulation.GetComponent<Controls>().first_measurement_detected)
        {
            onContinue();
        }
    }
}
