using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILayout : MonoBehaviour
{
    public GameObject MainMenu;
    public GameObject Dialogue;
    public GameObject ObsPanel;
    public GameObject RadarPanel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetUITransform(GameObject obj, Vector3 offset)
    {
        Vector3 newPosition = Camera.main.transform.position + 
            Camera.main.transform.right * offset.x  + 
            Camera.main.transform.up * offset.y + 
            Camera.main.transform.forward * offset.z;
        obj.transform.position = newPosition;
        obj.transform.rotation = Camera.main.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (Camera.main != null)
        {
            if (MainMenu != null && MainMenu.activeSelf)
                SetUITransform(MainMenu, new Vector3(0, -0.5f, 1));
            if (Dialogue != null && Dialogue.activeSelf)
                SetUITransform(Dialogue, new Vector3(0,0,1));
            if (ObsPanel != null && ObsPanel.activeSelf)
                SetUITransform(ObsPanel, new Vector3(-0.7f, -0.3f, 1));
            if (RadarPanel != null && RadarPanel.activeSelf)
                SetUITransform(RadarPanel, new Vector3(-0.7f, 0.3f, 1));

        }
    }
}
