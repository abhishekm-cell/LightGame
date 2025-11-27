using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSource : MonoBehaviour
{
    [SerializeField] private LayerMask endPointlayerMask ;
    [SerializeField] private bool endPointReached = false;
    [SerializeField] private Transform endPoint;
    [SerializeField] private float gravityMOD;
    [SerializeField] private float speedBoost;
    [SerializeField] private GameManager gameManager;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        endPointReached = false;
    }

    // Update is called once per frame
    void Update()
    {
        LightSourceMove();
        if(endPointReached)
        {
            transform.position = endPoint.position;
            gameManager.levelCleared = true;
        }
    }
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("EndPoint"))
        {
            endPointReached = true;
        }
    }
    void LightSourceMove()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            rb.gravityScale = gravityMOD;
        }

        if (Input.GetMouseButtonDown(0))
        {
            rb.gravityScale = gravityMOD;
        }   
    }

    
}
