using UnityEngine;

public class CamAspect : MonoBehaviour
{
    [SerializeField] private float iPhoneCamSize = 10f;
    [SerializeField] private float iPadCamSize = 16f;

    private Camera cam;

    private void Awake()
    {
        cam = GetComponent<Camera>();
        //cam.orthographic = true;
        UpdateCameraSize();
    }

    private void UpdateCameraSize()
    {
        if (IsTablet())
            cam.orthographicSize = iPadCamSize;
        else
            cam.orthographicSize = iPhoneCamSize;
    }

    private bool IsTablet()
    {
        // fallback if DPI is unavailable
        if (Screen.dpi <= 0)
            return Mathf.Max(Screen.width, Screen.height) >= 1200;

        // approximate physical screen size in inches
        float widthInches  = Screen.width  / Screen.dpi;
        float heightInches = Screen.height / Screen.dpi;
        float diagonalInches = Mathf.Sqrt(
            widthInches * widthInches +
            heightInches * heightInches
        );

        // iPads are ~7"+, iPhones are smaller
        return diagonalInches >= 7f;
    }
}

