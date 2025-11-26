using UnityEngine;

public class LineDraw : MonoBehaviour
{
    [SerializeField] private GameObject linePrefab;

    [Space (30f)]
    [SerializeField] private Gradient lineColor;
    [SerializeField] private float linePtMinDistance;
    [SerializeField] private float lineWidth;
    
    Test currentLine;
    Camera cam;


    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            BeginDraw();
        }

        if(currentLine!=null)
        {
            Draw();
        }

        if(Input.GetMouseButtonUp(0))
        {
            EndDraw();
        }
        // if (Input.GetMouseButtonDown(0))
        // {
        //     currentLine = Instantiate(linePrefab, cam.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity).GetComponent<Test>();
        //     currentLine.SetLineColor(lineColor);
        //     currentLine.SetLineWidth(lineWidth);
        //     currentLine.SetPointMinDistance(linePtMinDistance);
        // }
        // if (Input.GetMouseButton(0))
        // {
        //     currentLine?.AddPoint(cam.ScreenToWorldPoint(Input.mousePosition));
        // }
    }

    void BeginDraw()
    {
        currentLine = Instantiate(linePrefab,this.transform).GetComponent<Test>();

        currentLine.SetLineColor(lineColor);
        currentLine.SetLineWidth(lineWidth);
        currentLine.SetPointMinDistance(linePtMinDistance);

    }

    void Draw()
    {
        Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        currentLine.AddPoint(mousePos);
    }

    void EndDraw()
    {
        if(currentLine!=null)
        {
            if(currentLine.pointCount < 2)// ifline has one point
            {
                Destroy(currentLine.gameObject);    
            }
            currentLine = null;
        }
    }


}