using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.Experimental.CameraSystem;
using Microsoft.MixedReality.Toolkit.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine;

public class RecordSimulation : MonoBehaviour
{
    public GameObject Simulation;
    private ControlsWithDialogue controls;
    //Recording related variable
    [SerializeField] private int lambda;
    private float recording_threshold;
    private float lastrecord;
    //Simulation recording
    private bool recoding_simulation = false;
    private string filePath; 
    private string delimiter = ","; // Delimiter to separate values in the CSV file
    private StreamWriter simulation_writer;

    [DllImport("__Internal")] public static extern void FirebaseLogSimulationData(string message);

    // Start is called before the first frame update
    void Start()
    {
        controls = Simulation.GetComponent<ControlsWithDialogue>();
        if (recording_threshold <= 0) 
        {
            recording_threshold = 5;
        }
        lastrecord = Time.time;
        recording_threshold = (float)1 / lambda;
        if (controls.CurrentMode == ControlsWithDialogue.GameMode.HoloLens)
        {
            double unixTime = GetUnixTimestamp(DateTime.UtcNow);
            System.DateTime utcTime = System.DateTime.UtcNow;
            string formattedTime = utcTime.ToString("yyyy.MM.dd-HH_mm_ss");
            filePath = Path.Combine(Application.persistentDataPath, formattedTime + "_Simulation.csv");
            simulation_writer = new StreamWriter(filePath, true);
            simulation_writer.WriteLine("id,time,position,rotation,scale");
        }
    }

    private double GetUnixTimestamp(DateTime dateTime)
    {
        TimeSpan timeSpan = dateTime - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        return timeSpan.TotalSeconds;
    }

    private void OnDestroy()
    {
        if (controls.CurrentMode == ControlsWithDialogue.GameMode.HoloLens)
        {
            simulation_writer.Close();
            simulation_writer.Dispose();
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
        if (controls.CurrentMode == ControlsWithDialogue.GameMode.WebGL)
            FirebaseLogSimulationData(message);
        if (controls.CurrentMode == ControlsWithDialogue.GameMode.HoloLens)
        {
            simulation_writer.WriteLine(message);
            simulation_writer.Flush();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //print(recoding_simulation);
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
