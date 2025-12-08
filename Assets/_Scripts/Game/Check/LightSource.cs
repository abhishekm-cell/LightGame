using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSource : MonoBehaviour
{

    [SerializeField] private float gravityMOD;
    [SerializeField] private float speedBoost;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private float camHeightY, camWidthX;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezePosition;
        
    }
    public void SetGameManager(GameManager manager)
    {
        gameManager = manager;
    }

    // Update is called once per frame
    void Update()
    {
        LightSourceMove();
        if(gameManager.levelCleared)
        {
            //Debug.Log("level cleared");
            //rb.constraints = RigidbodyConstraints2D.FreezePosition;
            gameManager.LevelClearCheck();
        }
        
        if (transform.position.y < GetBottomBound() || transform.position.y > GetTopBound())
        {
            gameManager.SetGameOver();
        }

        if (transform.position.x < GetLeftBound() || transform.position.x > GetRightBound())
        {
            gameManager.SetGameOver();
        }

    }
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("EndPoint")) //LayerMask.NameToLayer("EndPoint")
        {
            gameManager.levelCleared = true;
            transform.position = collision.transform.position;
            rb.constraints = RigidbodyConstraints2D.FreezePosition;
        }

        if(collision.gameObject.layer == LayerMask.NameToLayer("SpeedBoost"))
        {
            Debug.Log("speed boost");

            rb.velocity *= speedBoost;
        }
       
    }
    void LightSourceMove()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)// touch check
        {
            rb.gravityScale = gravityMOD;
            rb.constraints = RigidbodyConstraints2D.None;
            
        } 
    }

    public float GetTopBound()
    {
        var cam = Camera.main;
        return cam.transform.position.y + cam.orthographicSize;
    }

    public float GetBottomBound()
    {
        var cam = Camera.main;
        return cam.transform.position.y - cam.orthographicSize;
    }

    public float GetRightBound()
    {
        var cam = Camera.main;
        return cam.transform.position.x + cam.orthographicSize * cam.aspect;
    }

    public float GetLeftBound()
    {
        var cam = Camera.main;
        return cam.transform.position.x - cam.orthographicSize * cam.aspect;
    }

    
}
