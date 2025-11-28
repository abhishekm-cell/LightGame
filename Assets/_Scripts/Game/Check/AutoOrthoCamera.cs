using UnityEngine;

[ExecuteAlways]
[RequireComponent(typeof(Camera))]
public class AutoOrthoCamera : MonoBehaviour
{
    [Header("Reference (design) values")]
    public float referenceWidth = 1920f;
    public float referenceHeight = 1080f;

    [Tooltip("Orthographic size used for the reference aspect ratio")]
    public float referenceOrthoSize = 5f;

    [Header("Smoothing")]
    public bool smooth = false;
    public float smoothSpeed = 8f;

    private Camera _cam;
    private float _lastScreenW, _lastScreenH;


    void OnEnable()
    {
        _cam = GetComponent<Camera>();
        Apply(true);
    }

    void Update()
    {
        if (Screen.width != _lastScreenW || Screen.height != _lastScreenH)
        {
            Apply(false);
            _lastScreenW = Screen.width;
            _lastScreenH = Screen.height;
        }

        if (smooth)
            Apply(false);
    }


    void Apply(bool instant)
    {
        if (_cam == null) _cam = GetComponent<Camera>();
        _cam.orthographic = true;
        _cam.rect = new Rect(0, 0, 1, 1);

        float currentAspect = (float)Screen.width / Screen.height;
        float referenceAspect = referenceWidth / referenceHeight;

        float targetOrthoSize;

        // --- AUTO MATCH HEIGHT/WIDTH ---
        //
        // If screen is wider than design aspect → match height
        // If screen is taller than design    → match width
        //
        if (currentAspect > referenceAspect)
        {
            // MATCH HEIGHT (FitHeight)
            targetOrthoSize = referenceOrthoSize;
        }
        else
        {
            // MATCH WIDTH (FitWidth)
            targetOrthoSize = referenceOrthoSize * (referenceAspect / currentAspect);
        }

        // --- Apply / Smooth ---
        if (instant || !smooth)
        {
            _cam.orthographicSize = targetOrthoSize;
        }
        else
        {
            _cam.orthographicSize = Mathf.Lerp(
                _cam.orthographicSize,
                targetOrthoSize,
                Time.deltaTime * smoothSpeed
            );
        }
    }


#if UNITY_EDITOR
    void OnValidate()
    {
        if (referenceWidth <= 0) referenceWidth = 1920f;
        if (referenceHeight <= 0) referenceHeight = 1080f;
        if (referenceOrthoSize <= 0.01f) referenceOrthoSize = 5f;

        Apply(true);
    }
#endif
}
