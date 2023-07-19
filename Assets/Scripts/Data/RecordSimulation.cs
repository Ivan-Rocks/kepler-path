using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.Experimental.CameraSystem;
using Microsoft.MixedReality.Toolkit.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using UnityEngine;

public class RecordSimulation : MonoBehaviour
{
    public GameObject Simulation;
    //Recording related variable
    [SerializeField] private int lambda;
    private float recording_threshold;
    private float lastrecord;
    //Simulation recording
    private bool recoding_simulation = false;
    private string filePath; 
    private string delimiter = ","; // Delimiter to separate values in the CSV file
    private StreamWriter writer;
    // Start is called before the first frame update
    void Start()
    {
        if (recording_threshold <= 0) 
        {
            recording_threshold = 5;
        }
        lastrecord = Time.time;
        recording_threshold = (float)1 / lambda;

        double unixTime = GetUnixTimestamp(DateTime.UtcNow);
        System.DateTime utcTime = System.DateTime.UtcNow;
        string formattedTime = utcTime.ToString("yyyy.MM.dd-HH_mm_ss");
        filePath = Path.Combine(Application.persistentDataPath, formattedTime + "_Simulation.csv");
        writer = new StreamWriter(filePath, true);
        writer.WriteLine("id,time,position,rotation,scale");
    }

    private double GetUnixTimestamp(DateTime dateTime)
    {
        TimeSpan timeSpan = dateTime - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        return timeSpan.TotalSeconds;
    }

    private void OnDestroy()
    {
        writer.Close();
        writer.Dispose();
    }

    public void ClearCsvFile(string filePath)
    {
        // Open the file in write mode and overwrite the content with an empty string
        using (StreamWriter streamWriter = new StreamWriter(filePath))
        {
            streamWriter.Write(string.Empty);
        }
    }

    public void recordSimulation()
    {
        recoding_simulation = !recoding_simulation;
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
        message += "UTC-" + formattedTime + delimiter;
        foreach (String temp in s)
            message += temp + delimiter;
        print(message);
        writer.WriteLine(message);
        writer.Flush();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        print(recoding_simulation);
        if (Time.time - lastrecord < recording_threshold)
        {
            return;
        } else
        {
            lastrecord = Time.time;
        }
        //Simulation
        if (recoding_simulation)
        {
            String[] elements = new String[] { 
                Simulation.transform.position.ToString(), 
                Simulation.transform.localScale.ToString(), 
                Simulation.transform.rotation.ToString()};
            write_to_CSV(elements);
        }

    }
}
