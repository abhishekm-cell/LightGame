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

    void HandleDraw()
    {
        Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
        {
            _currentLine = Instantiate(linePrefab, mousePos, Quaternion.identity);
            
        }
        if(Input.GetMouseButton(0))
        {
            _currentLine.SetPosition(mousePos);
        }
    }
}
