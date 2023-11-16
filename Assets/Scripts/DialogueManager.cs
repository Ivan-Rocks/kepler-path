using Microsoft.MixedReality.Toolkit.UX;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.IO;
using static UnityEngine.InputSystem.LowLevel.InputStateHistory;
using Unity.VisualScripting;
using Microsoft.MixedReality.Toolkit.SpatialManipulation;
using Microsoft.MixedReality.Toolkit.Utilities.Solvers;

public class DialogueManager : MonoBehaviour
{
    public GameObject MainMenu;
    public GameObject Simulation;
    public GameObject Dialogue;
    public GameObject Header;
    public GameObject MainText;
    public int phase = 0;
    public int total_phases=12;
    private ControlsWithDialogue.GameMode mode;
    //public string csvFilePath = "Assets/Scripts/Dialogues.csv";
    //Strings
    public List<string> headers = new List<string>();
    public List<string> texts = new List<string>();
    // Start is called before the first frame update
    void Start()
    {
        setDialogue();
        mode = Simulation.GetComponent<ControlsWithDialogue>().CurrentMode;
        if (mode != ControlsWithDialogue.GameMode.HoloLens)
        {
            Dialogue.GetComponent<Microsoft.MixedReality.Toolkit.Utilities.Solvers.RadialView>().enabled = false;
            Dialogue.GetComponent<Microsoft.MixedReality.Toolkit.Utilities.Solvers.SolverHandler>().enabled = false;
        }
    }

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
        if (phase == 3 && !Simulation.GetComponent<ControlsWithDialogue>().first_manipulation_detected && !MainMenu.activeSelf)
        {
            Simulation.GetComponent<ControlsWithDialogue>().enterInteractionMode();
            Dialogue.SetActive(false);
            MainMenu.SetActive(true);
            return;
        }

        if (phase == 5 && !Simulation.GetComponent<ControlsWithDialogue>().paused)
        {
            Simulation.GetComponent<ControlsWithDialogue>().enterPlayMode();
            Dialogue.SetActive(false);
            return;
        }

        if (phase == 7 && !Simulation.GetComponent<ControlsWithDialogue>().selectAllToggles())
        {
            Simulation.GetComponent<ControlsWithDialogue>().enterObserveMode();
            Dialogue.SetActive(false);
            return;
        }

        if (phase == 8 && !Simulation.GetComponent<ControlsWithDialogue>().measuring)
        {
            Simulation.GetComponent<ControlsWithDialogue>().enterMeasuringMode();
            Dialogue.SetActive(false);
            return;
        }
        if (phase== 9 && !Simulation.GetComponent<ControlsWithDialogue>().first_measurement_detected)
        {
            Dialogue.SetActive(false);
            return;
        }
        if(phase== 10 && !Simulation.GetComponent<ControlsWithDialogue>().first_record_detected)
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
        /* Fix the position of the Dialogue into a certain place.
         * I can also set the rotation of the dialogue instance to be the same as camera, but I think I will leave the design issue to Jina.
         */
        if (Camera.main != null)
        {
            Vector3 offset = new Vector3(0, 0, 1); // Adjust the offset as needed
            Vector3 newPosition = Camera.main.transform.position + Camera.main.transform.forward * offset.z;
            Dialogue.transform.position = newPosition;
        }
        //have to constantly check for updates on paused or other features to make sure we go to the next step
        //it automatically changes to onContinue with a status that current task is finished -> e.g. .paused
        //so the if statement on top can be bypassed
        if (phase == 3 && Simulation.GetComponent<ControlsWithDialogue>().first_manipulation_detected && MainMenu.activeSelf)
        {
            onContinue();
        }

        if (phase == 5 && Simulation.GetComponent<ControlsWithDialogue>().paused)
        {
            onContinue();
        }
        if (phase == 7 && Simulation.GetComponent<ControlsWithDialogue>().selectAllToggles())
        {
            onContinue();
        }
        if (phase == 8 && Simulation.GetComponent<ControlsWithDialogue>().measuring)
        {
            onContinue();
        }
        if (phase == 9 && Simulation.GetComponent<ControlsWithDialogue>().first_measurement_detected)
        {
            onContinue();
        }
    }
}
