using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeasuringTool : MonoBehaviour
{
    public Kepler k;
    private LineRenderer linerend;
    private Vector2 mousePos;
    private Vector2 startPos;
    public float distance;
    // Start is called before the first frame update
    void Start()
    {
        linerend = GetComponent<LineRenderer>();
        linerend.positionCount = 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (Camera.current.ToString().Contains("Topview") && k.paused)
        {
            if (Input.GetMouseButtonDown(0))
            {
                startPos = Camera.current.ScreenToWorldPoint(Input.mousePosition);
            }
            if (Input.GetMouseButton(0))
            {
                mousePos = Camera.current.ScreenToWorldPoint(Input.mousePosition);
                linerend.SetPosition(0, new Vector3(startPos.x, startPos.y, 0f));
                linerend.SetPosition(0, new Vector3(mousePos.x, mousePos.y, 0f));
                distance = (mousePos - startPos).magnitude;
                print("measured" + distance);
            }
        }

    }
}
