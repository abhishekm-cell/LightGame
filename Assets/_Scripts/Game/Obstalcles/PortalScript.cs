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
        Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
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
        
        Vector2 exitPos = portalOut.transform.TransformPoint(contactLocalPos);
        
        Vector2 newExitPos =new Vector2( exitPos.x + exitOffset,exitPos.y + exitOffset) ;

        Vector2 finalExitPos = newExitPos; 
        
        obj.transform.position = finalExitPos;
        
        Vector2 rotatedVelocity = portalOut.transform.rotation * savedVelocity;
        rb.velocity = rotatedVelocity * velocityMultiplier;
    }
} 