using Unity.VisualScripting;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    [SerializeField] private SpawnManager spawnManager;
    [SerializeField] private GameObject floor;
    [SerializeField] private Transform startPT;
    [SerializeField] private GameObject target;
    public bool gameOver { get; private set; }
    public bool gamestarted = false;  
    public bool levelCleared = false;


    /*
        Game will start / be inplay when there are no ui panels active, if panels are active game will be in pause state



        Game states = 1. Game in play ( no panels active) 2. game pause (panels active)
        loop should be between the above 2 states,  
    */


    public void RestartLevel()
    {
        Debug.Log("restarting level69");
        if(gameOver)
        {
            spawnManager.SpawnLightSource();
            Debug.Log("restarting level");
            
        }
    }

    public void SetGameOver()
    {
        gameOver = true;
    }

}