using Unity.VisualScripting;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    [SerializeField] private SpawnManager spawnManager;
    [SerializeField] private DrawManager drawManager;   
    [SerializeField] private GameObject floor;
    [SerializeField] private Transform startPT;
    [SerializeField] private GameObject target;
    [SerializeField] private GameObject endpoint;
    public bool gameOver { get; private set; }
    public bool gamestarted = false;  
    public bool levelCleared = false;


    public void UpdateTargetReference(GameObject newTarget)
    {
        target = newTarget;
        Debug.Log("Target reference updated!");
    }

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
            drawManager.ClearAllLines();
            
        }
    }

    public void SetGameOver()
    {
        gameOver = true;
    }

    public void LevelClearCheck()
    {
        if (target != null)
        {
            target.transform.position = endpoint.transform.position;
        }
        else
        {
            Debug.LogWarning("Target is null! Was it destroyed or not spawned yet?");
        }
    }

}