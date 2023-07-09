using Microsoft.MixedReality.Toolkit.UX;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public class RecordActions : MonoBehaviour
{
    public GameObject Simulation;
    public Controls controls;
    private string filePath;
    private string delimiter = ","; // Delimiter to separate values in the CSV file
    private StreamWriter action_writer;
    public GameObject[] buttons;
    // Start is called before the first frame update
    void Start()
    {
        controls = Simulation.GetComponent<Controls>();
        filePath = Application.dataPath + "/Generated Data/Actions.csv";
        //filePath = "Internal Storage/HoloOrbitsData/Simulation.csv";
        ClearCsvFile(filePath);
        action_writer = new StreamWriter(filePath, true);
        action_writer.WriteLine("id,time,Action,Message");
    }
    private void OnDestroy()
    {
        action_writer.Close();
        action_writer.Dispose();
    }

    public void ClearCsvFile(string filePath)
    {
        // Open the file in write mode and overwrite the content with an empty string
        using (StreamWriter streamWriter = new StreamWriter(filePath))
        {
            streamWriter.Write(string.Empty);
        }
    }
    public void write_to_CSV(String[] s)
    {
        String message = "";
        int id = s.GetHashCode();
        uint unsignedHashCode = unchecked((uint)id);
        message = unsignedHashCode.ToString() + delimiter;
        //UTC Time
        System.DateTime utcTime = System.DateTime.UtcNow;
        string formattedTime = utcTime.ToString("yyyy-MM-dd HH:mm:ss.fff");
        message += "UTC-"+formattedTime + delimiter;
        foreach (String temp in s)
            message += temp + delimiter;
        //print(message);
        action_writer.WriteLine(message);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void recordPause()
    {
        if (controls.paused)
        {
            String[] elements = new String[] {"Button Press", "Play"};
            write_to_CSV(elements);
        } else
        {
            String[] elements = new String[] {"Button Press", "Pause" };
            write_to_CSV(elements);
        }
    }

    public void recordLog()
    {
        if (controls.RadarPanel.activeSelf)
        {
            String[] elements = new String[] { "Button Press", "Open Log" };
            write_to_CSV(elements);
        } else
        {
            String[] elements = new String[] { "Button Press", "Close Log" };
            write_to_CSV(elements);
        }
    }

    public void recordMeasure()
    {
        String[] elements = new String[] { "Button Press", "Measure" };
        write_to_CSV(elements);
    }

    public void recordRecord()
    {
        String[] elements = new String[] { "Button Press", "Record" };
        write_to_CSV(elements);
    }

    public void recordReset()
    {
        String[] elements = new String[] { "Button Press", "Reset" };
        write_to_CSV(elements);
    }

    public void recordReturn()
    {
        String[] elements = new String[] { "Button Press", "Return" };
        write_to_CSV(elements);
    }

    public void recordObserve()
    {
        if (controls.ObsPanel.activeSelf)
        {
            String[] elements = new String[] { "Button Press", "Open Observe" };
            write_to_CSV(elements);
        }
        else
        {
            String[] elements = new String[] { "Button Press", "Close Observe" };
            write_to_CSV(elements);
        }
    }

    public void recordToggles(String t)
    {
        String[] elements = new String[] { "Toggle Press", t };
        write_to_CSV(elements);
    }

    public  void recordHit(GameObject obj)
    {
        String[] elements = new String[] { "Made a measurement", "CLicked on "+obj.name};
        write_to_CSV(elements);
    }
}
