
using UnityEngine;

public class BlackHole : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private CircleCollider2D _collider;
    [SerializeField] private float moveSpeedToCentre;
    [SerializeField] private bool isMovingToCentre = false;
    [SerializeField] private float stickDistance = 0.1f; 
    [SerializeField] private GameManager gameManager;
    private Rigidbody2D targetRb;

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("black hole trigger");
        if (collision.gameObject.layer == LayerMask.NameToLayer("LightSource"))
        {
            Debug.Log("Starting move to centre");
            target = collision.gameObject;
            targetRb = target.GetComponent<Rigidbody2D>();
            if (targetRb != null)
            {
                targetRb.gravityScale = 0;
                targetRb.velocity = Vector2.zero; 
            }
            
            isMovingToCentre = true;
            //gameManager.SetGameOver();
        }
    }

    void Update()
    {
        if (isMovingToCentre)
        {
            BlackHoleFunction();
        }
    }
    public void UpdateTarget(GameObject newTarget)
    {
        target = newTarget;
        Debug.Log("Target reference updated!");
    }

    void BlackHoleFunction()
    {
        Vector2 colliderCenter = _collider.bounds.center;
        float distance = Vector2.Distance(target.transform.position, colliderCenter);
        
        // If close enough, snap to center and stop moving
        if (distance <= stickDistance)
        {
            target.transform.position = colliderCenter;
            isMovingToCentre = false;
        }
        else
        {
            target.transform.position = Vector2.MoveTowards(target.transform.position, colliderCenter, moveSpeedToCentre * Time.deltaTime);
        }
    }

}
