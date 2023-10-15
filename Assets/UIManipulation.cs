using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIManipulation : MonoBehaviour
{
    public GameObject ShowButton;
    public GameObject ScaleSlider;
    public GameObject RotationSliderX;
    public GameObject RotationSliderY;
    public GameObject RotationSliderZ;

    private bool show = false;

    // Start is called before the first frame update
    void Start()
    {
        ScaleSlider.SetActive(show);
        RotationSliderX.SetActive(show);
        RotationSliderY.SetActive(show);
        RotationSliderZ.SetActive(show);
    }

    public void onShowButtonPressed()
    {
        show  = !show;
        ScaleSlider.SetActive(show);
        RotationSliderX.SetActive(show);
        RotationSliderY.SetActive(show);
        RotationSliderZ.SetActive(show);
    }

    public void onScaleSliderChanged()
    {
        float value = ScaleSlider.GetComponent<Slider>().value;
        Vector3 newScale = new Vector3(value, value, value);
        gameObject.transform.localScale = newScale;
        print(value);
    }

    /*
     * Quaternion only takes [-180,180] degrees as argument
     * Out of caution and userbility, the slider bound values will be [-179,179] and the user starts at 0
     */
    public void onSRotationSliderChanged()
    {
        float valueX = RotationSliderX.GetComponent<Slider>().value;
        float valueY = RotationSliderY.GetComponent<Slider>().value;
        float valueZ = RotationSliderZ.GetComponent<Slider>().value;
        transform.rotation = Quaternion.Euler(valueX, valueY, valueZ);
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
