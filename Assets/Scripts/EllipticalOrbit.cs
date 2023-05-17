using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UIElements;

public class EllipticalOrbit : MonoBehaviour
{
    public GameObject controls;
    public GameObject Attractor;
    public GameObject Planet;
    public GameObject periObj;
    public GameObject apoObj;
    public GameObject fictObj;
    public float apoapsis;
    public float periapsis;
    public float argumentOfPeriapsis = 0;
    public float inclination = 0;
    public int interpolations; //how many interpolations per orbit
    private GameObject Simulation;
    private LineRenderer orbit;
    private Vector3[] positions;
    public int degree = 0;

    // Start is called before the first frame update
    void Start()
    {
        Simulation = GameObject.Find("Simulation");
        if (interpolations < 360)
            interpolations = 360;
        positions = new Vector3[interpolations+1];
        orbit= GetComponent<LineRenderer>();
        apoapsis = Mathf.Abs(Attractor.transform.position.x - apoObj.transform.position.x);
        periapsis = Mathf.Abs(Attractor.transform.position.x - periObj.transform.position.x);
        periObj.transform.position = new Vector3(periapsis, 0, 0);
        apoObj.transform.position = new Vector3(-apoapsis, 0, 0);
        fictObj.transform.position = new Vector3(periapsis - apoapsis, 0, 0);
        orbit.positionCount= interpolations+1;
        for (int i = 0; i <= interpolations; i++)
            positions[i] = ComputePointOnOrbit(apoapsis, periapsis, 
            argumentOfPeriapsis, inclination, (float)i / interpolations);
        //orbit.SetPositions(positions);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Adjust Orbit
        Vector3[] pos = new Vector3[interpolations+1];
        Quaternion system_rotation = gameobject.transform.rotation;
        Vector3 scale = gameobject.transform.localScale;
        Vector3 offset = gameobject.transform.position;
        for (int i=0; i<=interpolations; i++)
        {
            pos[i] = UpdatePosition(positions[i], system_rotation, scale, offset);
        }
        orbit.SetPositions(pos);
        //Calculate current planet position
        bool paused = controls.GetComponent<Controls>().paused; 
        if (!paused)
            degree++;
        if (degree > 360)
            degree -= 360;
        float t = (float)degree / 360;
        Vector3 planet_pos = ComputePointOnOrbit(apoapsis, periapsis, argumentOfPeriapsis, inclination, t);
        transform.position = UpdatePosition(planet_pos, system_rotation, scale, offset);
    }

    public Vector3 UpdatePosition(Vector3 x, Quaternion rotation, Vector3 scale, Vector3 offset)
    {
        Matrix4x4 scaleMatrix = Matrix4x4.Scale(scale);
        Vector3 updated = rotation * scaleMatrix.MultiplyPoint(x) + offset;
        return updated;
    }

    /// <summary> Computes a position on the orbit for a given value of t ranging between 0 and 1 </summary>
    public static Vector3 ComputePointOnOrbit(float apoapsis, float periapsis, float argumentOfPeriapsis, float inclination, float t)
    {
        float semiMajorAxis = (apoapsis + periapsis) / 2f;
        float semiMinorAxis = Mathf.Sqrt(apoapsis * periapsis);

        float meanAnomaly = t * Mathf.PI * 2f; // Mean anomaly ranges anywhere from 0 - 2¦Ð
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

        // For most orbits an initial value of M(t) is sufficient, unless e > 0.8 in which case a value of ¦Ð should be used
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
