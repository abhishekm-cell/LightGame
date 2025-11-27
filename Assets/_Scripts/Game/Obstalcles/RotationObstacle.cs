using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationObstacle : MonoBehaviour
{
    [SerializeField] private float rotSPeed;
    //[SerializeField] private GameObject

    // Update is called once per frame
    void Update()
    {
        OnRotate();
    }

    private void OnRotate()
    {
        gameObject.transform.Rotate(0, 0, 1f * rotSPeed * Time.deltaTime);
    }
}
