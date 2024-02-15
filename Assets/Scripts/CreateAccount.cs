using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreateAccount : MonoBehaviour
{
    public string email;
    public string password;
    public string confirmedpassword;

    [DllImport("__Internal")] public static extern void FirebaseRegister(string email, string password, string confirmedPassword, string objectName, string callback, string fallback);


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

    public void setConrimedPassword(string str)
    {
        confirmedpassword = str;
    }

    public void onRegisterAccount()
    {
        //print(email+ " " + password + " " + confirmedpassword);
        FirebaseRegister(email, password, confirmedpassword, gameObject.name, "OnRegisterSuccess", "OnRegisterFailed");
    }

    public void goToLogin()
    {
        SceneManager.LoadScene(0);
    }

    public void OnRegisterSuccess()
    {
        print("success");
        SceneManager.LoadScene(1);
        //showText.text = "new username, success";
    }

    public void OnRegisterFailed()
    {
        print("failed");
        //showText.text = "username taken, failed";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
