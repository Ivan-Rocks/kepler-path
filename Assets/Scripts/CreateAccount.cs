using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateAccount : MonoBehaviour
{
    public string email;
    public string password;
    public string confirmedpassword;

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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
