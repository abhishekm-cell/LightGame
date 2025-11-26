using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawManager : MonoBehaviour
{
    private Camera cam;
    [SerializeField] private Line linePrefab;
    
    private Line _currentLine;
    
    public const float Resolution = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        HandleDrawTouch();
        HandleMouseDraw();
        
    }

    // void HandleDraw()
    // {
    //     Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
    //     if (Input.GetMouseButtonDown(0) || Input.GetTouch(0).phase == TouchPhase.Began)
    //     {
    //         _currentLine = Instantiate(linePrefab, mousePos, Quaternion.identity);
            
    //     }
    //     if(Input.GetMouseButton(0) || Input.GetTouch(0).phase == TouchPhase.Began)
    //     {
    //         _currentLine.SetPosition(mousePos);
    //     }
    // }

    void HandleMouseDraw()
    {
        if (!Application.isEditor) return; // Only mouse in editor

        Vector2 pos = cam.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            _currentLine = Instantiate(linePrefab, pos, Quaternion.identity);
        }

        if (Input.GetMouseButton(0))
        {
            _currentLine?.SetPosition(pos);
        }
    }
    
    void HandleDrawTouch()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 touchPos = cam.ScreenToWorldPoint(touch.position);

            if (touch.phase == TouchPhase.Began)
            {
                _currentLine = Instantiate(linePrefab, touchPos, Quaternion.identity);
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                _currentLine?.SetPosition(touchPos);
            }
        }
    }

    public bool TouchCheck()
    {
        Debug.Log("touch count: " + Input.touchCount);
        return Input.touchCount > 0;
    }
}
