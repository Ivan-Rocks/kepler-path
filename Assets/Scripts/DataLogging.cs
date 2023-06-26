using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.Experimental.CameraSystem;
using Microsoft.MixedReality.Toolkit.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using UnityEngine;

public class DataLogging : MonoBehaviour
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
        filePath = Application.dataPath + "/Generated Data/Simulation.csv";
        ClearCsvFile(filePath);
        writer = new StreamWriter(filePath, true);
        writer.WriteLine("id,time,position,rotation,scale");
    }

    private void OnDestroy()
    {
        writer.Close(); // Close the StreamWriter when the application is closed
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
        foreach (String temp in s) 
            message += temp + delimiter;
        print(message);
        writer.WriteLine(message);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
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
            String[] elements = new String[] { Time.time.ToString(), 
                Simulation.transform.position.ToString(), Simulation.transform.localScale.ToString(), Simulation.transform.rotation.ToString()};
            write_to_CSV(elements);
            //print(Simulation.transform.position +" "+ Simulation.transform.localScale +" "+Simulation.transform.rotation);
            //get it to str and write to csv
        }
        //Hands CSV
        
        /* Player.csv
        // Retrieve the player's position and rotation
        Vector3 playerPosition = CameraCache.Main.transform.position;
        Quaternion playerRotation = CameraCache.Main.transform.rotation;
        // Log the player's position and rotation or use them as needed
        Debug.Log("Player Position: " + playerPosition);
        Debug.Log("Player Rotation: " + playerRotation.eulerAngles);*/

    }
}
