using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TestFirebase : MonoBehaviour
{
    public TextMeshProUGUI text;

    [DllImport("__Internal")] public static extern void GetJSON(string path, string objectName, string callback, string fallback);
    [DllImport("__Internal")] public static extern void SetJSON(string path, string objectName, string callback, string fallback);

    private void Start()
    {
        GetJSON("Users/Ivan", gameObject.name, "OnRequestSuccess", "OnRequestFailed");
        SetJSON("Users/Ivan4", gameObject.name, "OnRequestSuccess", "OnRequestFailed");
    }

    private void OnRequestSuccess(string data)
    {
        text.text = data;
        text.color = Color.green;
    }

    private void OnRequestFailed(string data)
    {
        text.text = data;
        text.color = Color.red;
    }
}
