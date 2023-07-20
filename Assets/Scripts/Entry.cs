using System;
using Microsoft.MixedReality.Toolkit.UX;
using TMPro;
using UnityEngine;

public class Entry : MonoBehaviour
{
    [NonSerialized] public GameObject start;
    [NonSerialized] public GameObject end;
    [NonSerialized] public string distance;
    [NonSerialized] public float t;
    [NonSerialized] public bool status=false;//false for not shown on hologram
    [NonSerialized] public GameObject Data;
    public GameObject startText;
    public GameObject endText;
    public GameObject distText;
    public GameObject prefabGhost;
    private LineRenderer line;
    private GameObject instance;
    // Show Hide Delete Button
    public GameObject ButtonPrefab;
    public GameObject PromptButton;
    public GameObject Positive;
    public GameObject Negative;
    public GameObject Neutral;
    void Start()
    {
        //Assign Buttons
        PromptButton = Instantiate(ButtonPrefab, GameObject.Find("Data").transform);
        Positive = PromptButton.transform.Find("Canvas/Horizontal/Show-Pos").gameObject;
        print(Positive == null);
        Negative = PromptButton.transform.Find("Canvas/Horizontal/Hide-Neg").gameObject;
        Neutral = PromptButton.transform.Find("Canvas/Horizontal/Delete-Neutral").gameObject;
        PromptButton.SetActive(false);
        //Button Binding
        Positive.GetComponent<PressableButton>().enabled = true;
        Negative.GetComponent<PressableButton>().enabled = false;
        Neutral.GetComponent<PressableButton>().enabled = true;
        Positive.GetComponent<PressableButton>().OnClicked.AddListener(Show);
        Negative.GetComponent<PressableButton>().OnClicked.AddListener(Hide);
        Neutral.GetComponent<PressableButton>().OnClicked.AddListener(Delete);
        //other values
        gameObject.GetComponent<PressableButton>().OnClicked.AddListener(onPressed);
        Data = GameObject.Find("Data"); 
        gameObject.GetComponent<PressableButton>().OnClicked.AddListener(()=> Data.GetComponent<RecordActions>().recordDataSelection(status, start,end,distance,t));
    }

    public void Initialize(GameObject start, GameObject end, string distance, float t)
    {
        this.start = start;
        this.end = end;
        this.distance = distance;
        this.t = t;
        startText.GetComponent<TextMeshProUGUI>().text = start.name;
        endText.GetComponent<TextMeshProUGUI>().text = end.name;
        distText.GetComponent<TextMeshProUGUI>().text = distance;
        line = gameObject.GetComponent<LineRenderer>();
        line.positionCount = 0;
    }

    public void onPressed()
    {
        PromptButton.SetActive(true);
        print("pressed");
    }

    public void onStatusChanged()
    {
        status = !status;
        if (status==true)
        {
            Show();
        } else
        {
            Hide();
        }
    }

    public void Hide()
    {
        //on UI
        status = false;
        Positive.GetComponent<PressableButton>().enabled = true;
        Negative.GetComponent<PressableButton>().enabled = false;
        PromptButton.SetActive(false);
        //on Simulation
        line.positionCount = 0;
        if (instance != null)
        {
            instance.SetActive(false);
        }
    }

    public void Show()
    {
        //on UI
        Positive.GetComponent<PressableButton>().enabled = false;
        Negative.GetComponent<PressableButton>().enabled = true;
        PromptButton.SetActive(false);
        //on Simulation
        Vector3 startpos = start.transform.position;
        Vector3 endpos = end.transform.position;
        if (start.name == "Exoplanet")
        {
            print(t);
            startpos = GameObject.Find("Simulation").GetComponent<EllipticalOrbit>().getGhostPosition(t);
            instance = Instantiate(prefabGhost, GameObject.Find("Simulation").transform);
            instance.transform.position = startpos;
            instance.SetActive(true);
        }
        if (end.name == "Exoplanet")
        {
            endpos = GameObject.Find("Simulation").GetComponent<EllipticalOrbit>().getGhostPosition(t);
            instance = Instantiate(prefabGhost, GameObject.Find("Simulation").transform);
            instance.transform.position = endpos;
            instance.SetActive(true);
        }
        line.positionCount = 2;
        line.startWidth = GameObject.Find("Simulation").transform.localScale.x / 10;
        line.endWidth = GameObject.Find("Simulation").transform.localScale.x / 10;
        line.SetPosition(0, startpos);
        line.SetPosition(1, endpos);
    }

    public void Delete()
    {
        PromptButton.SetActive(false);
        if (gameObject!=null)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
