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
using System;

public class Login : MonoBehaviour
{
    public string email;
    public string password;
    public GameObject inputText;
    public TextMeshProUGUI showText;
    [DllImport("__Internal")] public static extern void FirebaseLogin(string email, string password, string objectName, string callback, string fallback);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void setEmail(string str)
    {
        email = str;
    }

    public void setPassword(string str)
    {
        password = str;
    }

    public void ConfirmLogin()
    {
        print(email+ " "+ password);
        FirebaseLogin(email, password, gameObject.name, "OnLoginSuccess", "OnLoginFailed");
    }

    public void goToRegister()
    {
        SceneManager.LoadScene(1);
    }

    public void OnLoginSuccess()
    {
        print("success");
        SceneManager.LoadScene(2);
    }

    public void OnLoginFailed()
    {
        print("failed");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
