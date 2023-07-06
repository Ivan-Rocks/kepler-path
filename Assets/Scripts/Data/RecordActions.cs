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
    private string filePath;
    private string delimiter = ","; // Delimiter to separate values in the CSV file
    private StreamWriter action_writer;
    public GameObject[] buttons;
    // Start is called before the first frame update
    void Start()
    {
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
        foreach (String temp in s)
            message += temp + delimiter;
        print(message);
        action_writer.WriteLine(message);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void recordPause()
    {
        if(Simulation.GetComponent<Controls>().paused)
        {
            String[] elements = new String[] { Time.time.ToString(), "Button Press", "Pause"};
            write_to_CSV(elements);
        } else
        {
            String[] elements = new String[] { Time.time.ToString(), "Button Press", "Play" };
            write_to_CSV(elements);
        }
    }

    public  void recordHit(GameObject obj)
    {
        String[] elements = new String[] { Time.time.ToString(), "Made a measurement", "CLicked on "+obj.name};
        write_to_CSV(elements);
    }
}
