using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class SelectionNotice : MonoBehaviour
{
    private ControlsWithDialogue controls;
    private GameObject selected;
    private Vector3 clickPosition;
    [SerializeField] private Color textColor;
    [SerializeField] private float positionOffsetSpeed;
    [SerializeField] private float colorDecaySpeed;
    private Vector3 position_offset = new Vector3(0, 0, 0);
    private Vector4 color_offset = new Vector4(0, 0, 0, 0);
    // Start is called before the first frame update
    void Start()
    {
        controls = GameObject.Find("Simulation").GetComponent<ControlsWithDialogue>();
    }

    public void setText(GameObject obj, Vector3 clickPosition)
    {
        selected = obj;
        this.clickPosition = clickPosition;
        gameObject.GetComponent<TextMeshProUGUI>().text = obj.name + " Selected";
        position_offset = new Vector3(0, 0, 0);
        color_offset = new Vector4(textColor.r, textColor.g, textColor.b, 1.0f);
        print("real" + selected.transform.position);
        print("mouse" + clickPosition);
    }

    // Update is called once per frame
    void Update()
    {
        if (selected != null)
        {
            position_offset.y += positionOffsetSpeed;
            color_offset.w -= colorDecaySpeed;
            gameObject.GetComponent<TextMeshProUGUI>().color = color_offset;
            if (controls.CurrentMode == ControlsWithDialogue.GameMode.HoloLens)
            {
                gameObject.transform.position = Camera.main.WorldToScreenPoint(selected.transform.position + new Vector3(0.3f, -1.0f, 0)) + position_offset;
            } else {
                gameObject.GetComponent<RectTransform>().anchoredPosition3D = clickPosition + new Vector3(0.3f, -1.0f, 0) + position_offset;
                print(gameObject.GetComponent<RectTransform>().anchoredPosition3D);
            }
        }
    }
}
