using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShipConeRaycaster : MonoBehaviour
{
    [SerializeField] private List<Transform> planets;
    [SerializeField] private Transform sun; 
    [SerializeField] private TextMeshProUGUI infoDisplay;
    [SerializeField] private TextMeshProUGUI infoDisplay2;
    [SerializeField] private float speedUpTime = 1.0f; 
    private LineRenderer orbitLine;


    void Update()
    {
        Transform closestPlanet = FindClosestPlanet();

        if (closestPlanet != null)
        {
            DisplayPlanetInfo(closestPlanet);
            HighlightClosestPlanet(closestPlanet);
        }
    }


    private void HighlightClosestPlanet(Transform closestPlanet)
    {
        if (orbitLine == null)
        {
            GameObject lineObj = new GameObject("OrbitLine");
            orbitLine = lineObj.AddComponent<LineRenderer>();
            orbitLine.material = new Material(Shader.Find("Unlit/Color"));
            orbitLine.material.color = Color.cyan;
            orbitLine.widthMultiplier = 0.1f;
        }

        Vector3[] points = new Vector3[100];
        float radius = closestPlanet.localScale.x * 1.5f;
        for (int i = 0; i < points.Length; i++)
        {
            float angle = (i / (float)points.Length) * 2 * Mathf.PI;
            points[i] = closestPlanet.position + new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * radius;
        }

        orbitLine.positionCount = points.Length;
        orbitLine.SetPositions(points);
    }

    private Transform FindClosestPlanet()
    {
        Transform closestPlanet = null;
        float minDistance = Mathf.Infinity; 

        foreach (Transform planet in planets)
        {
            float distance = Vector3.Distance(transform.position, planet.position);

            if (distance < minDistance)
            {
                minDistance = distance;
                closestPlanet = planet;
            }
        }

        return closestPlanet; 
    }

    // Displays information about the planet
    private void DisplayPlanetInfo(Transform planet)
    {
        PlanetData planetData = planet.GetComponent<PlanetData>();
        if (planetData == null) return;

        string info1 = $"Planet: {planetData.planetName}\n" +
                      $"{planetData.description}\n" +
                      $"-Common Elements: {planetData.commonElements}\n" +
                      $"-Volume: {planetData.volume}\n" +
                      $"-Mass: {planetData.mass}\n";
        string info2 = $"Planet: {planetData.planetName}\n" +                 
                 $"-Avg Temp: {planetData.averageTemperature}°C\n" +
                 $"-Day Duration: {planetData.dayDuration} hrs\n" +
                 $"-Year Duration: {planetData.yearDuration} days\n" +
                 $"Distance: {Vector3.Distance(transform.position, planet.position)*10000:0.0} km"; 

        infoDisplay.text = info1;
        infoDisplay2.text = info2;
    }
}
