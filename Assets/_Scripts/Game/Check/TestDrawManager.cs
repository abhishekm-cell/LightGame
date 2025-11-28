using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestDrawManager : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private RectTransform canvasRect;
    [SerializeField] private GameObject linePrefab; // Not used anymore, but keep for reference
    [SerializeField] private float lineWidth = 5f;
    [SerializeField] private Color lineColor = Color.white;
    [SerializeField] private PhysicsMaterial2D lineMaterial; // Optional physics material
    
    private RectTransform currentLine;
    private List<GameObject> drawnLines = new List<GameObject>();
    private List<Vector2> currentPoints = new List<Vector2>();
    private const float minDistance = 10f;

    void Start()
    {
        if (canvasRect == null)
        {
            canvasRect = canvas.GetComponent<RectTransform>();
        }
    }

    void Update()
    {
        HandleInput();
    }

    void HandleInput()
    {
        Vector2 inputPos = Vector2.zero;
        bool isDrawing = false;
        bool startedDrawing = false;

        if (Input.GetMouseButtonDown(0))
        {
            startedDrawing = true;
            inputPos = Input.mousePosition;
        }
        else if (Input.GetMouseButton(0))
        {
            isDrawing = true;
            inputPos = Input.mousePosition;
        }

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            inputPos = touch.position;

            if (touch.phase == TouchPhase.Began)
            {
                startedDrawing = true;
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                isDrawing = true;
            }
        }

        if (startedDrawing)
        {
            StartNewLine(inputPos);
        }
        else if (isDrawing)
        {
            AddPoint(inputPos);
        }
    }

    void StartNewLine(Vector2 screenPos)
    {
        GameObject lineObj = new GameObject("DrawnLine");
        lineObj.transform.SetParent(canvas.transform, false);
        lineObj.layer = LayerMask.NameToLayer("Default"); // Set appropriate layer
        
        currentLine = lineObj.AddComponent<RectTransform>();
        currentPoints.Clear();
        
        drawnLines.Add(lineObj);
        
        AddPoint(screenPos);
    }

    void AddPoint(Vector2 screenPos)
    {
        if (currentLine == null) return;

        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvasRect,
            screenPos,
            canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : canvas.worldCamera,
            out localPoint
        );

        if (currentPoints.Count > 0)
        {
            float dist = Vector2.Distance(localPoint, currentPoints[currentPoints.Count - 1]);
            if (dist < minDistance) return;
        }

        currentPoints.Add(localPoint);

        if (currentPoints.Count >= 2)
        {
            DrawLineSegment(
                currentPoints[currentPoints.Count - 2], 
                currentPoints[currentPoints.Count - 1]
            );
        }
    }

    void DrawLineSegment(Vector2 start, Vector2 end)
    {
        GameObject segment = new GameObject("LineSegment");
        segment.transform.SetParent(currentLine, false);

        // Add Image component
        Image img = segment.AddComponent<Image>();
        img.color = lineColor;

        // Setup RectTransform
        RectTransform rect = segment.GetComponent<RectTransform>();
        
        Vector2 midpoint = (start + end) / 2f;
        rect.anchoredPosition = midpoint;

        float length = Vector2.Distance(start, end);
        float angle = Mathf.Atan2(end.y - start.y, end.x - start.x) * Mathf.Rad2Deg;

        rect.sizeDelta = new Vector2(length, lineWidth);
        rect.rotation = Quaternion.Euler(0, 0, angle);
        rect.pivot = new Vector2(0.5f, 0.5f);
        rect.anchorMin = new Vector2(0.5f, 0.5f);
        rect.anchorMax = new Vector2(0.5f, 0.5f);

        // Add BoxCollider2D to the segment
        BoxCollider2D collider = segment.AddComponent<BoxCollider2D>();
        
        // Convert UI size to world size for collider
        // The collider size should match the visual size
        Vector2 colliderSize = rect.sizeDelta;
        
        // Scale based on canvas scale
        float canvasScale = canvas.scaleFactor;
        colliderSize /= canvasScale;
        
        collider.size = colliderSize;
        collider.offset = Vector2.zero;
        
        // Optional: Add physics material
        if (lineMaterial != null)
        {
            collider.sharedMaterial = lineMaterial;
        }
        
        // Optional: Make it a trigger or static collider
        // collider.isTrigger = true; // Uncomment if you want trigger collisions
        
        Debug.Log($"Created line segment with collider. Size: {colliderSize}, Position: {midpoint}");
    }

    public void ClearAllLines()
    {
        foreach (GameObject line in drawnLines)
        {
            if (line != null)
            {
                Destroy(line);
            }
        }
        drawnLines.Clear();
        currentLine = null;
        currentPoints.Clear();
    }
}
