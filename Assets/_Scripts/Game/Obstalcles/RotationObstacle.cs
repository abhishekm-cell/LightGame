using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationObstacle : MonoBehaviour
{
    [SerializeField] public float rotSPeed {get; private set;}
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
