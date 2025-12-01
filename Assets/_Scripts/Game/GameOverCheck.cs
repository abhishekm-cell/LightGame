using UnityEngine;

public class GameOverCheck : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("LightSource"))
        {
            gameManager.SetGameOver();
            //gameManager.RestartLevel();
            Destroy(collision.gameObject);
            Debug.Log("game over");
        }
    }
}