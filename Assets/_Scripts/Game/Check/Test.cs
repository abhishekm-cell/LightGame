using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private EdgeCollider2D edgeCollider;

    [SerializeField] private List<Vector2> points = new List<Vector2>();

    public int pointCount = 0;

    [SerializeField] private float pointMinDistance = 0.1f;

    public void AddPoint(Vector2 newPoint)
    {
        if(pointCount>=1f && Vector2.Distance(newPoint, GetLastPoint()) < pointMinDistance)
        {
            return;
        }

        points.Add(newPoint);
        pointCount++;

        lineRenderer.positionCount = pointCount;
        lineRenderer.SetPosition(pointCount - 1, newPoint);

        //edgecollider

        if(pointCount >1)
        {
            edgeCollider.points = points.ToArray();
        }
    }

    public Vector2 GetLastPoint()
    {
        return (Vector2) lineRenderer.GetPosition(pointCount - 1);
    }

    // public void UsePhysics(bool physics)
    // {
            //rb.isKinematic = !usePhysics;
    // }

    public void SetLineColor(Gradient color)
    {
        lineRenderer.colorGradient = color;
    }

    public void SetPointMinDistance(float dist)
    {
        pointMinDistance = dist;
    }


    public void SetLineWidth(float width)
    {
        lineRenderer.startWidth = width;
        lineRenderer.endWidth = width;
        edgeCollider.edgeRadius = width/2f;  
    }

}

/*
[SerializeField] private LineRenderer _renderer;
    [SerializeField] private EdgeCollider2D _collider;

    private List<Vector2> points = new List<Vector2>();
    [SerializeField]private float smoothFactor = 10f;  // The higher = smoother

    void Awake()
    {
        _collider.transform.position -= transform.position;
    }

    public void SetPosition(Vector2 pos)
    {
        // First point (no smoothing)
        if (_renderer.positionCount == 0)
        {
            AddPoint(pos);
            return;
        }

        Vector2 lastPos = _renderer.GetPosition(_renderer.positionCount - 1);

        // Only add if far enough
        if (Vector2.Distance(lastPos, pos) < DrawManager.Resolution)
            return;

        // Smooth new point
        Vector2 smoothPos = Vector2.Lerp(lastPos, pos, Time.deltaTime * smoothFactor);

        AddPoint(smoothPos);
        
    }

    private void AddPoint(Vector2 point)
    {
        points.Add(point);

        _renderer.positionCount = points.Count;
        _renderer.SetPosition(points.Count - 1, point);

        _collider.points = points.ToArray();
    }
*/

