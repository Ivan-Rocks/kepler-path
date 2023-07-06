using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.XR;

public class RecordRightHand : MonoBehaviour
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
        filePath = Application.dataPath + "/Generated Data/RightHand.csv";
        //filePath = "Internal Storage/HoloOrbitsData/Simulation.csv";
        ClearCsvFile(filePath);
        player_writer = new StreamWriter(filePath, true);
        player_writer.WriteLine("id,time,position,rotation,pointer");
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
        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.Palm, Handedness.Right, out MixedRealityPose palmPose))
        {
            print("hi");
            // Get hand position
            Vector3 handPosition = palmPose.Position;

            // Get hand rotation
            Quaternion handRotation = palmPose.Rotation;

            // Get hand forward vector
            Vector3 handForward = palmPose.Forward;

            print(handPosition);
        }
        //Left Hand
        /*
        if (leftHand != null)
        {
            Transform x = leftHand.GetComponent<MRTKRayInteractor>().rayOriginTransform;
            print(x.position);
            print(x.forward);
            String[] elements = new String[] { x.position.ToString(),
                x.rotation.ToString(), x.forward.ToString()};
            write_to_CSV(elements);
            print(elements);
        }*/
    }
}
