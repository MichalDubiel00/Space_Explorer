using System.Collections.Generic;
using UnityEngine;

public class CelestialBodiesManager : MonoBehaviour
{
    [SerializeField] private Transform sun; 
    [SerializeField] private List<Transform> planets; 

    [SerializeField] private float speedUpTime = 1; 

    /*Roatations around Own Axis:
      lets set Earth to 1.0f
      Mercury: 58d 16h, 10.83 km/h = 0.00688f
      Venus: 243d 26m, 6.52 km/h = 0.004142313f
      Earth: 23h 56m, 1574 km/h = 1.0f
      Mars: 24h 36m, 866 km/h = 0.5501906f
      Jupiter: 9h 55m, 45,583 km/h = 28.95997f
      Saturn: 10h 33m, 36,840 km/h = 23.40534f
      Uranus: 17h 14m, 14,794 km/h = 9.398984f
      Neptune: 16h, 9,719 km/h = 6.174714f
      
      Roatation Around The Sun
      Mercury: 88 days =  0.2712329
      Venus: 225 days = 0.6164383
      Earth: 365 days = 1f;
      Mars: 687 days = 1.882192
      Jupiter: 4,333 days = 11.87123
      Saturn: 10,759 days =  29.47671
      Uranus: 30,687 days = 84.07397
      Neptune: 60,190 days = 164.9041
    */
    private float[] rotationAroundOwnAxis = { 10.83f, 6.52f, 1574.0f, 866.0f, 45583.0f, 36840.0f, 14794.0f, 9719.0f };
    private float[] rotationAroundSunAxis = { 99, 225, 365, 687, 4333, 10759, 30687, 60190 };
    private float[] orbitRadii; 
    private List<LineRenderer> orbitRenderers = new List<LineRenderer>();

    private void Start()
    {
        orbitRadii = new float[planets.Count];

        
        for (int i = 0; i < rotationAroundOwnAxis.Length; i++)
        {
            rotationAroundOwnAxis[i] /= 1574.0f; 
            rotationAroundSunAxis[i] /= 365;    
            rotationAroundSunAxis[i] = 1 / rotationAroundSunAxis[i]; 

            
            orbitRadii[i] = Vector3.Distance(planets[i].position, sun.position);
        }
    }

    private void Update()
    {
        for (int i = 0; i < planets.Count; i++)
        {
            
            planets[i].Rotate(Vector3.up, rotationAroundOwnAxis[i] * Time.deltaTime * speedUpTime);
           
            planets[i].RotateAround(sun.position, Vector3.up, rotationAroundSunAxis[i] * Time.deltaTime * speedUpTime);
        }
    }
}
