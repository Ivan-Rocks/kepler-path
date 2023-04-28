using JetBrains.Annotations;
using Microsoft.MixedReality.Toolkit.UX;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Kepler : MonoBehaviour
{

    public GameObject Attractor;
    public bool paused;
    //public GameObject ImaginaryFocus;
    public float apoapsis;
    public float periapsis;
    public float argumentOfPeriapsis = 0;
    public float inclination = 0;
    public float t;
    private int cnt = 0;

    // Start is called before the first frame update
    void Start()
    {
        paused = false;
        apoapsis = Mathf.Abs(Attractor.transform.position.x - GameObject.Find("B1").transform.position.x);
        periapsis = Mathf.Abs(Attractor.transform.position.x - GameObject.Find("A1").transform.position.x);
    }

    public void onPause()
    {
        paused = !paused;
    }

    public void setPeriapsis()
    {
        UnityEngine.UI.Slider perislider = GameObject.Find("Periapsis").GetComponent<UnityEngine.UI.Slider>();
        periapsis = perislider.value;
    }

    public void setApoapsis()
    {
        UnityEngine.UI.Slider aposlider = GameObject.Find("Apoapsis").GetComponent<UnityEngine.UI.Slider>();
        apoapsis = aposlider.value;
    }    

    // Update is called once per frame
    void FixedUpdate()
    {
        GameObject.Find("A1").transform.position = new Vector3(periapsis, 0, 0);
        GameObject.Find("B1").transform.position = new Vector3(-apoapsis, 0, 0);
        GameObject.Find("Fictional Focus").transform.position = new Vector3(periapsis-apoapsis, 0, 0);
        //apoapssis = Mathf.Abs(Attractor.transform.position.x - GameObject.Find("B1").transform.position.x);
        //periapsis = Mathf.Abs(Attractor.transform.position.x - GameObject.Find("A1").transform.position.x);
        if (!paused)
        {
            cnt++;
        }
        if (cnt > 360)
            cnt -= 360;
        t = (float)cnt/360;
        transform.position = Attractor.transform.position + ComputePointOnOrbit(apoapsis, periapsis, argumentOfPeriapsis, inclination, t);
        //print(transform.position);
    }


    /// <summary> Computes a position on the orbit for a given value of t ranging between 0 and 1 </summary>
    public static Vector3 ComputePointOnOrbit(float apoapsis, float periapsis, float argumentOfPeriapsis, float inclination, float t)
    {
        float semiMajorAxis = (apoapsis + periapsis) / 2f;
        float semiMinorAxis = Mathf.Sqrt(apoapsis * periapsis);

        float meanAnomaly = t * Mathf.PI * 2f; // Mean anomaly ranges anywhere from 0 - 2��
        float linearEccentricity = semiMajorAxis - periapsis;
        float eccentricity = linearEccentricity / semiMajorAxis; // Eccentricity ranges from 0 - 1 with values tending to 1 being increasingly elliptical

        float eccentricAnomaly = SolveKepler(meanAnomaly, eccentricity);

        float x = semiMajorAxis * (Mathf.Cos(eccentricAnomaly) - eccentricity);
        float y = semiMinorAxis * Mathf.Sin(eccentricAnomaly);

        Quaternion inclinedPlane = Quaternion.AngleAxis(inclination, Vector3.forward);
        Quaternion parametricAngle = Quaternion.AngleAxis(argumentOfPeriapsis, Vector3.up);
        return parametricAngle * inclinedPlane * new Vector3(x, 0f, y);
    }

    /// <summary> Implementation of Kepler's equation: M = E - e * sin(E) where M is the mean anomaly, E is the eccentric anomaly and e is the eccentricity. </summary>
    public static float SolveKepler(float M, float e)
    {
        /// Keplers equation: M = E - e * sin(E)
        /// Solving for E given M has no closed-form solution, an iterative approach such as Newton's method must therefore be adopted
        /// Keplers equation must therefore be re-arranged to find the root of the function, f(E) = E - esin(E) - M(t)
        /// Setting the function equal to 0 now means we can solve iteratively

        // Iteration continues until f(E) < desired accuracy
        float accuracy = 0.000001f;
        int maxIterations = 100;

        // For most orbits an initial value of M(t) is sufficient, unless e > 0.8 in which case a value of �� should be used
        float E = e > 0.8f ? Mathf.PI : M;

        // Apply iteration with E = M + e * sin(E)

        for (int k = 1; k < maxIterations; k++)
        {
            // Kepler's fixed point iteration version, citation: https://adsabs.harvard.edu/full/2000JHA....31..339S
            // E = M + e * Mathf.Sin(E);

            // Application of Newton Rhapson iteration En+1 = En - (f(En) / f'(En))
            float nextValue = E - (KeplersEquation(M, E, e) / KeplersEquation_Differentiated(E, e));
            float difference = Mathf.Abs(E - nextValue);
            E = nextValue;

            if (difference < accuracy)
            {
                break;
            }
        }
        return E;
    }

    private static float KeplersEquation(float M, float E, float e)
    {
        return E - (e * Mathf.Sin(E)) - M;
    }

    private static float KeplersEquation_Differentiated(float E, float e)
    {
        return 1 - (e * Mathf.Cos(E));
    }
}
