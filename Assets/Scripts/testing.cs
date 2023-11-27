using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testing : MonoBehaviour
{
    public RectTransform button;
    public RectTransform text;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Button Local pos: " + button.position + "; Text Local" + text.position);
        //Debug.Log("Button Local scale : " + button.localScale + "; Text Local" + text.localScale);
        // Printing the position in world space
        //Debug.Log("World Position: " + button.position);
    }
}
