using UnityEngine;

public class UpThrust : MonoBehaviour
{
    [SerializeField] private float _force = 10f;

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("LightSource"))
        {
            Debug.Log("Up thrust");
            other.gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up * _force, ForceMode2D.Impulse);
        }
    }

    

}