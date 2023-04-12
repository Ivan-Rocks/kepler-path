using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;
using UnityEngine.SceneManagement;

public class SimulationObjectManipulator : MonoBehaviour
{
    public GameObject Simulation;
    public bool measuring = false;
    // Start is called before the first frame update

    public void onMeasure()
    { if (measuring = true)
        {
            Simulation.GetComponent<ObjectManipulator>().enabled = false;
        }
    }
}
