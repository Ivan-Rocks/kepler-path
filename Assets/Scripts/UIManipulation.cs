using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIManipulation : MonoBehaviour
{
    public GameObject UIBarParent;
    public GameObject ShowButton;
    public GameObject ScaleSlider;
    public GameObject RotationSliderX;
    /*public GameObject RotationSliderY;
    public GameObject RotationSliderZ;*/
    public GameObject IncreaseRotationButton; 
    public GameObject DecreaseRotationButton; 
    public GameObject RotationButtonX;
    public float currentAngleX = 0.0f; 
    public float angleStep = 45.0f; // rotation step
    public float buttonAngleX = 0.0f; 

    private bool show = false;


    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.GetComponent<ControlsWithDialogue>().Hololens_Mode)
        {
            UIBarParent.SetActive(false);
        } else
        {
            ScaleSlider.SetActive(show);
            RotationSliderX.SetActive(show);
            /*RotationSliderY.SetActive(show);
            RotationSliderZ.SetActive(show);*/
            IncreaseRotationButton.SetActive(show); 
            DecreaseRotationButton.SetActive(show);
            RotationButtonX.SetActive(show);
        }
    }

    public void onShowButtonPressed()
    {
        show  = !show;
        ScaleSlider.SetActive(show);
        RotationSliderX.SetActive(show);
        /*RotationSliderY.SetActive(show);
        RotationSliderZ.SetActive(show);*/
        IncreaseRotationButton.SetActive(show); 
        DecreaseRotationButton.SetActive(show);
        RotationButtonX.SetActive(show);
    }

    public void onScaleSliderChanged()
    {
        float value = ScaleSlider.GetComponent<Slider>().value;
        Vector3 newScale = new Vector3(value, value, value);
        gameObject.transform.localScale = newScale;
        //print(value);
    }

    /*
     * Quaternion only takes [-180,180] degrees as argument
     * Out of caution and userbility, the slider bound values will be [-179,179] and the user starts at 0
     */
    public void onRotationSliderChanged()
    {
        float valueX = RotationSliderX.GetComponent<Slider>().value;
        /*float valueY = RotationSliderY.GetComponent<Slider>().value;
        float valueZ = RotationSliderZ.GetComponent<Slider>().value;*/
        transform.rotation = Quaternion.Euler(valueX, 0, 0);
    }


    // Increase x rotation angle 
    public void IncreaseRotation()
    {
        currentAngleX += angleStep;
        if (currentAngleX >= 180) currentAngleX = 179;
        RotateObjectToAngle(currentAngleX);
    }

    // Decrease x rotation angle 
    public void DecreaseRotation()
    {
        currentAngleX -= angleStep;
        if (currentAngleX < -180) currentAngleX = -180; 
        RotateObjectToAngle(currentAngleX);
    }


    private void RotateObjectToAngle(float angle)
    {
        transform.rotation = Quaternion.Euler(angle, 0, 0);
        print(currentAngleX);

    }

    // Rotate around X axis with one button 
    public void onRotationButtonChanged()
    {
        buttonAngleX += angleStep;
        if (buttonAngleX > 179) buttonAngleX -= 360;
        else if (buttonAngleX < -179) buttonAngleX += 360;
        RotateObjectToAngle(buttonAngleX);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
