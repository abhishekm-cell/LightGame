using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    [SerializeField] private LineRenderer _renderer;
    [SerializeField] private EdgeCollider2D _collider;

    private List<Vector2> rawPoints = new List<Vector2>();      // Raw input points
    private List<Vector2> smoothPoints = new List<Vector2>();   // Smoothed points

    [SerializeField] private int smoothingIterations = 2;   // You can increase to 3 or 4 for extra smoothness

    void Start()
    {
        _collider.transform.position -= transform.position;
    }

    public void SetPosition(Vector2 pos)
    {
        // First point
        if (rawPoints.Count == 0)
        {
            rawPoints.Add(pos);
            UpdateLine();
            return;
        }

        // Add only if far enough
        if (Vector2.Distance(rawPoints[rawPoints.Count - 1], pos) < DrawManager.Resolution)
            return;

        rawPoints.Add(pos);
        UpdateLine();
    }

    void UpdateLine()
    {
        smoothPoints = ChaikinSmooth(rawPoints, smoothingIterations);

        // Update line renderer
        _renderer.positionCount = smoothPoints.Count;
        for (int i = 0; i < smoothPoints.Count; i++)
        {
            _renderer.SetPosition(i, smoothPoints[i]);
        }

        Debug.Log("smooth points: " + smoothPoints.Count);
        // Update edge collider
        _collider.points = smoothPoints.ToArray();
    }

    // ------------------------
    //     CHAIKIN SMOOTHING
    // ------------------------
    private List<Vector2> ChaikinSmooth(List<Vector2> points, int iterations)
    {
        if (points.Count < 3) return new List<Vector2>(points);

        List<Vector2> result = new List<Vector2>(points);

        for (int iter = 0; iter < iterations; iter++)
        {
            List<Vector2> newPoints = new List<Vector2>();
            newPoints.Add(result[0]); // Keep first point

            for (int i = 0; i < result.Count - 1; i++)
            {
                Vector2 p0 = result[i];
                Vector2 p1 = result[i + 1];

                // Q = 75% of p0, 25% of p1
                Vector2 Q = p0 * 0.75f + p1 * 0.25f;

                // R = 25% of p0, 75% of p1
                Vector2 R = p0 * 0.25f + p1 * 0.75f;

                newPoints.Add(Q);
                newPoints.Add(R);
            }

            newPoints.Add(result[result.Count - 1]); // Keep last point
            result = newPoints;
        }

        return result;
    }
}



