using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    [SerializeField] private LineRenderer _renderer;
    [SerializeField] private EdgeCollider2D _collider;
    public LayerMask obstacleMask;
    private List<Vector2> rawPoints = new List<Vector2>();      
    private List<Vector2> smoothPoints = new List<Vector2>();   

    [SerializeField] private int smoothingIterations = 2;   

    void Start()
    {
        _collider.transform.position -= transform.position;
    }

    // public void SetPosition(Vector2 pos)
    // {
        
    //     if (rawPoints.Count == 0)
    //     {
    //         rawPoints.Add(pos);
    //         UpdateLine();
    //         return;
    //     }

        
    //     if (Vector2.Distance(rawPoints[rawPoints.Count - 1], pos) < DrawManager.Resolution)
    //         return;

    //     rawPoints.Add(pos);
    //     UpdateLine();
    // }

    

    public void SetPosition(Vector2 pos)
    {
        if (rawPoints.Count > 0)
        {
            Vector2 lastPoint = rawPoints[rawPoints.Count - 1];

            // CHECK IF LINE WOULD CROSS AN OBSTACLE
            RaycastHit2D hit = Physics2D.Linecast(lastPoint, pos, obstacleMask);

            if (hit.collider != null)
            {
                // Block drawing on obstacles
                return;
            }
        }

        // ===== existing code below =====
        if (rawPoints.Count == 0)
        {
            rawPoints.Add(pos);
            UpdateLine();
            return;
        }

        if (Vector2.Distance(rawPoints[rawPoints.Count - 1], pos) < DrawManager.Resolution)
            return;

        rawPoints.Add(pos);
        UpdateLine();
    }


    void UpdateLine()
    {
        smoothPoints = ChaikinSmooth(rawPoints, smoothingIterations);

        
        _renderer.positionCount = smoothPoints.Count;
        for (int i = 0; i < smoothPoints.Count; i++)
        {
            _renderer.SetPosition(i, smoothPoints[i]);
        }
        
        _collider.points = smoothPoints.ToArray();
    }

    private List<Vector2> ChaikinSmooth(List<Vector2> points, int iterations)
    {
        if (points.Count < 3) return new List<Vector2>(points);

        List<Vector2> result = new List<Vector2>(points);

        for (int iter = 0; iter < iterations; iter++)
        {
            List<Vector2> newPoints = new List<Vector2>();
            newPoints.Add(result[0]); 

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

            newPoints.Add(result[result.Count - 1]); 
            result = newPoints;
        }

        return result;
    }
}



