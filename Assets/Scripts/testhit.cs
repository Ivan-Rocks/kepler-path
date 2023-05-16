using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using Microsoft.MixedReality.Toolkit.UI;

public class testhit : MonoBehaviour
{

    public GameObject right;
    public void f()
    {
        print("hi");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnPointerClicked(MixedRealityPointerEventData eventData)
    {
        // Get the position and rotation of the pointer click
        Vector3 clickPosition = eventData.Pointer.Position;
        Quaternion clickRotation = eventData.Pointer.Rotation;

        // Perform actions based on the pointer click
        // ...

        print(clickPosition);
    }

    // Update is called once per frame
    void Update()
    {
        Transform x = right.GetComponent<MRTKRayInteractor>().rayOriginTransform;
        if (right.GetComponent<MRTKRayInteractor>().isSelectActive && x != null)
        {
            Vector3 rayDirection = x.forward;
            RaycastHit hit;
            if (Physics.Raycast(x.position, rayDirection, out hit, 50))
            {
                // An object is hit along the raycast path
                GameObject hitObject = hit.collider.gameObject;

                // Use the hit object for further calculations or actions
                // ...

                Debug.Log("Object Hit: " + hitObject.name);
            }
        }
    }
}
