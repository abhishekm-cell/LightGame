using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawManager : MonoBehaviour
{
    private Camera cam;
    [SerializeField] private Line linePrefab;
    private Line _currentLine;
    public const float Resolution = 0.15f;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        HandleDraw();
        
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

    void HandleDraw()
    {
        Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        // Start line on mouse click
        if (Input.GetMouseButtonDown(0))
        {
            _currentLine = Instantiate(linePrefab, mousePos, Quaternion.identity);
        }

        // Continue drawing while mouse held
        if (Input.GetMouseButton(0))
        {
            _currentLine?.SetPosition(mousePos);
        }

        // TOUCH SUPPORT
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
