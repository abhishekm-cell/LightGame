
using UnityEngine;

public class BlackHole : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private CircleCollider2D _collider;
    [SerializeField] private float moveSpeedToCentre;
    [SerializeField] private bool isMovingToCentre = false;
    [SerializeField] private float stickDistance = 0.1f; // How close before it "sticks"

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("black hole trigger");
        if (collision.gameObject.layer == LayerMask.NameToLayer("LightSource"))
        {
            Debug.Log("Starting move to centre");
            target = collision.gameObject;
            isMovingToCentre = true;
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
            
            // Optional: Disable physics so it stays stuck
            Rigidbody2D rb = target.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = Vector2.zero;
                rb.isKinematic = true; // Makes it not affected by physics
            }
        }
        else
        {
            // Keep moving toward center
            target.transform.position = Vector2.MoveTowards(target.transform.position, colliderCenter, moveSpeedToCentre * Time.deltaTime);
        }
    }

}
