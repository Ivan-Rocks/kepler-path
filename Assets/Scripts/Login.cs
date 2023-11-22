using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Runtime.InteropServices;
using System.IO;
using UnityEditor;
using static System.Net.Mime.MediaTypeNames;
using UnityEngine.SceneManagement;

public class Login : MonoBehaviour
{
    public string username;
    public GameObject inputText;
    public TextMeshProUGUI showText;
    [DllImport("__Internal")] public static extern void FirebaseLogin(string path, string objectName, string callback, string fallback);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ConfirmLogin()
    {
        username = inputText.GetComponent<TMP_InputField>().text;
        if (username != "")
        {
            FirebaseLogin(username, gameObject.name, "OnLoginSuccess", "OnLoginFailed");
        }
    }

    public void OnLoginSuccess()
    {
        print("success");
        SceneManager.LoadScene(1);
        //showText.text = "new username, success";
    }

    public void OnLoginFailed()
    {
        print("failed");
        //showText.text = "username taken, failed";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
