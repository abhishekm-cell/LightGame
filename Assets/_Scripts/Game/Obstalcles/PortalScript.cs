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

    public void SetOutPortal(GameObject outPortal)
    {
        this.portalOut = outPortal;
    }
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

        AudioManager.Instance.PlaySFX(SoundType.PortalIn);
        
        TeleportToExit(obj, rb);
    }

    private void TeleportToExit(GameObject obj, Rigidbody2D rb)
    {
        // Convert entry point (local) to exit position (world)
        Vector3 exitPos = portalOut.transform.TransformPoint(contactLocalPos);

        // Portal's facing direction (local up)
        Vector2 exitForward = portalOut.transform.up;

        // Add offset along local-facing direction
        Vector2 finalExitPos = (Vector2)exitPos + exitForward * exitOffset;

        // Move object
        obj.transform.position = finalExitPos;

        // Preserve velocity
        rb.velocity = savedVelocity * velocityMultiplier;

        AudioManager.Instance.PlaySFX(SoundType.PortalOut);
    }




} 