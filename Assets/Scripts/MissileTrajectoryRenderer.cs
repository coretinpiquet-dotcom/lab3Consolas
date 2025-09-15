using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(LineRenderer))]
public class MissileTrajectoryRenderer : MonoBehaviour
{
    [Header("Trajectory Settings")]
    public int numPoints = 50;
    public float timeStep = 0.1f;
    public float impulseForce = 20f;

    [Header("References")]
    public Transform firePoint;
    public LineRenderer lineRenderer;

    private void Awake()
    {
        if (lineRenderer == null)
            lineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        DrawTrajectory();
    }

    void DrawTrajectory()
    {
        Vector3[] points = new Vector3[numPoints];
        Vector3 currentPosition = firePoint.position;
        Vector3 currentVelocity = firePoint.forward * impulseForce;

        for (int i = 0; i < numPoints; i++)
        {
            points[i] = currentPosition;
            // Apply physics: position += velocity * dt; velocity += gravity * dt
            currentVelocity += Physics.gravity * timeStep;
            currentPosition += currentVelocity * timeStep;
        }

        lineRenderer.positionCount = numPoints;
        lineRenderer.SetPositions(points);
    }
}

