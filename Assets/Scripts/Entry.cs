using System;
using Microsoft.MixedReality.Toolkit.UX;
using TMPro;
using UnityEngine;

public class Entry : MonoBehaviour
{
    [NonSerialized] public GameObject start;
    [NonSerialized] public GameObject end;
    [NonSerialized] public string distance;
    [NonSerialized] public bool status=false;//false for not shown on holograph
    public GameObject startText;
    public GameObject endText;
    public GameObject distText;
    void Start()
    {
        gameObject.GetComponent<PressableButton>().OnClicked.AddListener(Show);
    }

    public void Initialize(GameObject start, GameObject end, string distance)
    {
        this.start = start;
        this.end = end;
        this.distance = distance;
        startText.GetComponent<TextMeshProUGUI>().text = start.name;
        endText.GetComponent<TextMeshProUGUI>().text = end.name;
        distText.GetComponent<TextMeshProUGUI>().text = distance;
    }

    public void Show()
    {
        //No need to worry about the positions of the 
        if (status==true)
        {
            print("show");
        }
        else
        {
            print("hide");
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
