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
    private ControlsWithDialogue controls;
    private string filePath;
    private string delimiter = ","; // Delimiter to separate values in the CSV file
    private StreamWriter action_writer;
    private String mode;
    // Start is called before the first frame update
    void Start()
    {
        controls = Simulation.GetComponent<ControlsWithDialogue>();
        print(controls == null);
        double unixTime = GetUnixTimestamp(DateTime.UtcNow);
        System.DateTime utcTime = System.DateTime.UtcNow;
        string formattedTime = utcTime.ToString("yyyy.MM.dd-HH_mm_ss");
        filePath = Path.Combine(Application.persistentDataPath, formattedTime + "_Actions.csv");
        print(filePath);
        action_writer = new StreamWriter(filePath, true);
        Console.WriteLine(action_writer); // Ivan -- this is what Luc
        action_writer.WriteLine("id,time,Scene,Action,Event,Description");
    }

    private double GetUnixTimestamp(DateTime dateTime)
    {
        TimeSpan timeSpan = dateTime - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        return timeSpan.TotalSeconds;
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
        //Unix Time
        double unixTime = GetUnixTimestamp(DateTime.UtcNow);
        message += "unix-" + unixTime.ToString() + delimiter;
        message += "UTC-" + formattedTime + delimiter + mode + delimiter;
        foreach (String temp in s)
            message += temp + delimiter;
        print(message);
        action_writer.WriteLine(message);
        action_writer.Flush();
    }

    // Update is called once per frame
    void Update()
    {
        if (controls.ObsPanel.activeSelf)
        {
            mode = "Observe";
        } else {
           if (controls.measuring)
            {
                mode = "Measure";
            }
           else
            {
                mode = "Play";
            }
        }
        print(mode);
    }
    
    public void recordPause()
    {
        if (controls.paused)
        {
            String[] elements = new String[] {"Button Press", "Play"};
            write_to_CSV(elements);
        } else
        {
            String[] elements = new String[] {"Button Press", "Pause"};
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
        String[] elements = new String[] { "Button Press", "Measure Mode" };
        write_to_CSV(elements);
    }

    public void recordRecord()
    {
        //String[] elements = new String[] { "Button Press", "Record" };
        //write_to_CSV(elements);
        MeasuringTool measuringTool = Simulation.GetComponent<MeasuringTool>();
        
        String[] elements = new String[] { "Distance Recorded", measuringTool.distance.ToString()+"AU", measuringTool.distance.ToString() };
        write_to_CSV(elements);
    }

    public void recordReset()
    {
        String[] elements = new String[] { "Line Reset" };
        write_to_CSV(elements);
    }

    public void recordReturn()
    {
        String[] elements = new String[] { "Return", "Return to Play Mode" };
        write_to_CSV(elements);
    }

    public void recordObserve()
    {
        if (controls.ObsPanel.activeSelf)
        {
            String[] elements = new String[] { "Button Press", "Open Observe Mode" };
            write_to_CSV(elements);
        }
        else
        {
            String[] elements = new String[] { "Button Press", "Close Observe Mode" };
            write_to_CSV(elements);
        }
    }

    public void recordToggles(String t)
    {
        String[] elements = t.Split(" ");
        print(elements);
        write_to_CSV(elements);
    }

    public  void recordHit(GameObject obj)
    {
        String[] elements = new String[] { "Object Selected " , obj.name};
        write_to_CSV(elements);
    }

    public void recordDataSelection(bool status, GameObject start, GameObject end, String distance, float t)
    {
        if (status)
        {
            String[] elements = new String[] { "Data Selected ", 
                "Show Entry:"+start.name+" to "+end.name+", distance:"+distance+" at radian"+t.ToString()};
            write_to_CSV(elements);
        } else
        {
            String[] elements = new String[] { "Data Selected ",
                "Hide Entry:"+start.name+" to "+end.name+",distance:"+distance+" at radian"+t.ToString()};
            write_to_CSV(elements);
        }
    }

    public void recordContinue()
    {
        String[] elements = new String[] { "Pressed Continue Button"};
        write_to_CSV(elements);
    }
}
