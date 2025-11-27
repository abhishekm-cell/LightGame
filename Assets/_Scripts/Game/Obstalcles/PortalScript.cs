using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalScript : MonoBehaviour
{
    [SerializeField] private GameObject target;      // object that teleports
    [SerializeField] private GameObject portalIn;    // entry portal
    [SerializeField] private GameObject portalOut;   // exit portal
    [SerializeField] private Vector2 direction;      // direction after exit
    [SerializeField] private int outForce;
    [SerializeField] private Vector2 savedVelocity;
    [SerializeField] private Vector2 contactLocalPos;
    [SerializeField] private float offsetExit;

    void OnTriggerEnter2D(Collider2D collision)
    {
        // only react to the target (or LightSource layer as you want)
        if (collision.gameObject == target)
        {
            TeleportIn();
        }
    }

    public void TeleportIn()
    {
        
                                    

        Rigidbody2D rb = target.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            savedVelocity = rb.velocity;
        }

        contactLocalPos = portalIn.transform.InverseTransformPoint(target.transform.position);
        //direction = portalIn.transform.position - target.transform.position;
        TeleportOut();
    }

    void TeleportOut()
    {
        
        Vector2 exitPos = portalOut.transform.TransformPoint(contactLocalPos);
        Vector2 newExitPos =new Vector2( exitPos.x + offsetExit,exitPos.y + offsetExit) ;

        target.transform.position = newExitPos;
        //target.transform.rotation = portalOut.transform.rotation;


        // OPTIONAL → if target has RB, apply force
        Rigidbody2D rb = target.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            //savedVelocity = rb.velocity;
            rb.velocity =  portalOut.transform.rotation * savedVelocity * outForce ;
        }
    }   
}