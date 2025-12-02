using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalScript : MonoBehaviour
{
    [Header("Portal Setup")]
    [SerializeField] private GameObject portalOut;   
    [Header("Exit Settings")]
    [SerializeField] private float exitOffset = 0.5f; 
    [SerializeField] private float velocityMultiplier = 1f; 
    private Vector2 savedVelocity;
    private Vector2 contactLocalPos;

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("teleport trigger");
        Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            Debug.Log("teleporting: " + collision.gameObject.name);
            TeleportObject(collision.gameObject, rb);
        }
    }

    private void TeleportObject(GameObject obj, Rigidbody2D rb)
    {
        
        savedVelocity = rb.velocity;

        
        contactLocalPos = transform.InverseTransformPoint(obj.transform.position);

        
        TeleportToExit(obj, rb);
    }

    private void TeleportToExit(GameObject obj, Rigidbody2D rb)
    {
        
        Vector3 exitPos = portalOut.transform.TransformPoint(contactLocalPos);

        
        Vector2 exitForward = portalOut.transform.forward;   
        Vector2 finalExitPos = (Vector2)exitPos + exitForward * exitOffset;

        obj.transform.position = finalExitPos;

        
        float angleDiff = portalOut.transform.eulerAngles.z - transform.eulerAngles.z;
        //Vector2 newVelocity = Quaternion.Euler(0, 0, 0)/*angleDiff*/ * savedVelocity;

        Vector2 newVelocity = savedVelocity * velocityMultiplier;

        rb.velocity = newVelocity;
    }

} 