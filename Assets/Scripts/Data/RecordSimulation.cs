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
        //filePath = Application.persistentDataPath + "/Generated Data/Simulation.csv";
        //String startTimeDate = utcTime.ToString("yyyy-MM-dd HH:mm:ss.fff"); // Ivan -- this is what Luc utcTime.ToString("yyyy-MM-dd HH:mm:ss.fff")
        //filePath = Path.Combine(Application.persistentDataPath, startTimeDate + "_Simulation.csv"); // Ivan -- this is what Luc
        System.DateTime utcTime = System.DateTime.UtcNow;
        string formattedTime = utcTime.ToString("yyyy-MM-dd HH:mm:ss.fff");
        filePath = Path.Combine(Application.persistentDataPath, formattedTime + "_Simulation.csv"); // Ivan -- this is what Luc
        //filePath = Path.Combine(Application.persistentDataPath, "Simulation.csv");
        //filePath = "Internal Storage/HoloOrbitsData/Simulation.csv";
        ClearCsvFile(filePath);
        writer = new StreamWriter(filePath, true);
        Console.WriteLine(writer); // Ivan -- this is what Luc
        writer.WriteLine("id,time,position,rotation,scale");
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
