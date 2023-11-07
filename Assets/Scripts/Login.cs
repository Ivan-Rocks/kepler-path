using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Login : MonoBehaviour
{
    public string username;
    public GameObject text;

        // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ConfirmLogin()
    {
        username = text.GetComponent<TMP_InputField>().text;
        print(username);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
