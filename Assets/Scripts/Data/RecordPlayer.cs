using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using static UnityEngine.InputSystem.HID.HID;

public class RecordPlayer : MonoBehaviour
{
    [SerializeField] private int lambda;
    private float recording_threshold;
    private float lastrecord;
    private string filePath;
    private string delimiter = ","; // Delimiter to separate values in the CSV file
    private StreamWriter player_writer;
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
        print("Current Unix time: " + unixTime);
        //filePath = Application.persistentDataPath + "/Generated Data/Simulation.csv";
        //String startTimeDate = utcTime.ToString("yyyy-MM-dd HH:mm:ss.fff"); // Ivan -- this is what Luc utcTime.ToString("yyyy-MM-dd HH:mm:ss.fff")
        //filePath = Path.Combine(Application.persistentDataPath, startTimeDate + "_Simulation.csv"); // Ivan -- this is what Luc
        System.DateTime utcTime = System.DateTime.UtcNow;
        string formattedTime = utcTime.ToString("yyyy.MM.dd-HH_mm_ss");
        print("Current Unix time: " + formattedTime);
        //filePath = Path.Combine(Application.persistentDataPath, formattedTime + "_Simulation.csv"); // Ivan -- this is what Luc
        filePath = Path.Combine(Application.persistentDataPath, formattedTime + "_Player.csv");
        //filePath = "Internal Storage/HoloOrbitsData/Simulation.csv";
        ClearCsvFile(filePath);
        player_writer = new StreamWriter(filePath, true);
        player_writer.WriteLine("id,time,position,rotation,pointer");
    }

    private double GetUnixTimestamp(DateTime dateTime)
    {
        TimeSpan timeSpan = dateTime - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        return timeSpan.TotalSeconds;
    }

    private void OnDestroy()
    {
        player_writer.Close();
        player_writer.Dispose();
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
        message += "UTC-" + formattedTime + delimiter;
        foreach (String temp in s)
            message += temp + delimiter;
        //print(message);
        player_writer.WriteLine(message);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - lastrecord < recording_threshold)
        {
            return;
        }
        else
        {
            lastrecord = Time.time;
        }
        //Player
        if (Camera.current!= null)
        {
            String[] elements = new String[] { Camera.current.transform.position.ToString(),
            Camera.current.transform.rotation.ToString(), Camera.current.transform.forward.ToString()};
            write_to_CSV(elements);
        }
    }
}
