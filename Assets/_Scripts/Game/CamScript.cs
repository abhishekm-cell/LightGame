using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamScript : MonoBehaviour
{
    public float targetAspect = 16f / 9f; // Your target aspect ratio
    public float baseOrthographicSize = 5f; // Your designed camera size
    
    void Start()
    {
        
    }

    void Update()
    {
        ChangeAspect();
    }

    void ChangeAspect()
    {
        float currentAspect = (float)Screen.width / Screen.height;
        
        if (currentAspect < targetAspect)
        {
            // Screen is narrower than target - adjust camera size
            float ratio = targetAspect / currentAspect;
            Camera.main.orthographicSize = baseOrthographicSize * ratio;
        }        
    }
}

