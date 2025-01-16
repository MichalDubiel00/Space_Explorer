using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class OrbitLinesGenerator : MonoBehaviour
{
    [SerializeField] private Transform sun; 
    [SerializeField] private List<Transform> planets; 
    [SerializeField] private Material orbitMaterial; 
    [SerializeField] private float lineWidth = 0.1f;
    [SerializeField] private int segments = 100;

    private List<LineRenderer> orbitRenderers = new List<LineRenderer>();

    private void Start()
    {
        GenerateOrbitLines();
    }

    private void LateUpdate()
    {
        UpdateOrbitLines();
    }

    private void GenerateOrbitLines()
    {
        foreach (var planet in planets)
        {        
            GameObject orbitObject = new GameObject($"Orbit_{planet.name}");
            orbitObject.transform.parent = transform; 
           
            LineRenderer lineRenderer = orbitObject.AddComponent<LineRenderer>();
            lineRenderer.material = orbitMaterial;
            lineRenderer.startWidth = lineWidth;
            lineRenderer.endWidth = lineWidth;
            lineRenderer.loop = true;
            lineRenderer.positionCount = segments;

            
            orbitRenderers.Add(lineRenderer);
        }
    }

    private void UpdateOrbitLines()
    {
        for (int i = 0; i < planets.Count; i++)
        {
            float orbitRadius = Vector3.Distance(planets[i].position, sun.position);
            Vector3[] points = CalculateOrbitPoints(orbitRadius);
            orbitRenderers[i].SetPositions(points);
        }
    }

    private Vector3[] CalculateOrbitPoints(float radius)
    {
        Vector3[] points = new Vector3[segments];
        float angleStep = 360f / segments;

        for (int i = 0; i < segments; i++)
        {
            float angle = i * angleStep * Mathf.Deg2Rad;
            points[i] = sun.position + new Vector3(Mathf.Cos(angle) * radius, 0, Mathf.Sin(angle) * radius);
        }

        return points;
    }
}
