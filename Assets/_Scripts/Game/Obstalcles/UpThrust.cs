using UnityEngine;

public class UpThrust : MonoBehaviour
{
    [SerializeField] private float _force = 10f;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("LightSource"))
        {
            Debug.Log("Up thrust");
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * _force, ForceMode2D.Impulse);
        }
    }

}