using Microsoft.MixedReality.Toolkit.UX;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.InputSystem.LowLevel.InputStateHistory;

public class DialogueManager : MonoBehaviour
{
    public GameObject Dialogue;
    public GameObject ContinueButton;
    public TextMeshProUGUI header;
    public TextMeshProUGUI text;

    // Start is called before the first frame update
    void Start()
    {
        Dialogue.GetComponent<PressableButton>().OnClicked.AddListener(onContinue);
        setDialogue();
    }

    public void setDialogue()
    {
        Dialogue.SetActive(true);
        header.text = "head";
        text.text = "aaa";
    }

    public void onContinue()
    {
        Dialogue.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
