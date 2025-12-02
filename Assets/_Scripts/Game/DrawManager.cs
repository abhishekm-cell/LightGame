using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class DrawManager : MonoBehaviour
{
    private Camera cam;
    [SerializeField] private Line linePrefab;
    [SerializeField] private float inkLimit = 5f;   
    private float inkUsed = 0f;                     
    private Vector2 lastPoint;
    [SerializeField] private Slider inkBar;
    

    [SerializeField] private List<Line> drawnLines = new List<Line>();
    
    public Line _currentLine {get; set;}
    
    public const float Resolution = 0.1f;
    
    void Start()
    {
        cam = Camera.main;
        if (inkBar != null)
        {
            inkBar.minValue = 0f;
            inkBar.maxValue = 1f;
            UpdateInkUI();
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        HandleDrawTouch();
        
        
    }

    void HandleDrawTouch()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 touchPos = cam.ScreenToWorldPoint(touch.position);

            // === START DRAW ===
            if (touch.phase == TouchPhase.Began)
            {
                
                if (inkUsed >= inkLimit) return;

                _currentLine = Instantiate(linePrefab, touchPos, Quaternion.identity);
                drawnLines.Add(_currentLine);

                lastPoint = touchPos; 
            }

            // === CONTINUE DRAWING ===
            else if (touch.phase == TouchPhase.Moved)
            {
                if (_currentLine == null) return;
                if (inkUsed >= inkLimit) return;

                float distance = Vector2.Distance(lastPoint, touchPos);

                
                if (distance >= Resolution)
                {
                    if (inkUsed + distance > inkLimit)
                    {
                        float allowed = inkLimit - inkUsed;

                        
                        Vector2 finalPoint = lastPoint + (touchPos - lastPoint).normalized * allowed;

                        _currentLine.SetPosition(finalPoint);
                        inkUsed = inkLimit; 
                        UpdateInkUI();

                        _currentLine = null; 
                        return;
                    }

                    
                    inkUsed += distance;
                    lastPoint = touchPos;

                    _currentLine.SetPosition(touchPos);
                    UpdateInkUI();
                }
            }
        }
    }

    void UpdateInkUI()
    {
        if(inkBar != null)
        {
            inkBar.value = 1f - (inkUsed / inkLimit);
        }
    }

    public void ClearAllLines()
    {
        foreach (Line line in drawnLines)
        {
            if (line != null)
            {
                Destroy(line.gameObject);
            }
        }
        drawnLines.Clear();
        _currentLine = null;
        inkUsed = 0f;
        UpdateInkUI();
    }

    public bool TouchCheck()
    {
        return Input.touchCount > 0;
    }
}