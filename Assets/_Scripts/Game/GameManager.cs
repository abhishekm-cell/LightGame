using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;


public class GameManager : MonoBehaviour
{
    //[SerializeField] private SpawnManager spawnManager;
    [SerializeField] private DrawManager drawManager;   
   // [SerializeField] private GameObject floor;
    [SerializeField] private Transform startPT;
    [SerializeField] private GameObject target;
    [SerializeField] private GameObject endpoint;
    [SerializeField] private BlackHole blackHole;
    [SerializeField] private LevelManager levelManager;
    public bool gameOver { get; private set; }
    public bool gamestarted = false;  
    public bool levelCleared = false;
    private bool onLastLevel = false;

    //public event System.Action GameOver;


    public void UpdateTargetReference(GameObject newTarget)
    {
        target = newTarget;
        if(blackHole != null)
        {
            blackHole.UpdateTarget(newTarget);
            Debug.Log("Target reference updated!");
        }
    }  
        

    /*
        Game will start / be inplay when there are no ui panels active, if panels are active game will be in pause state



        Game states = 1. Game in play ( no panels active) 2. game pause (panels active)
        loop should be between the above 2 states,  
    */


    public void SetGameOver()
    {
        gameOver = true;
        levelManager.RestartLevel();
        Debug.Log("Game over"); 
    }

    public void LevelClearCheck()
    {
        if(onLastLevel)
        {
            return;
        }
        if (target != null)
        {
            Debug.Log("Level cleared now!!!!!!!!!!");
            target.transform.position = endpoint.transform.position;
            var endpointLight = endpoint.GetComponentInChildren<Light2D>();
            endpointLight.intensity = 3f;   
            levelManager.LoadNext();
        }
        else
        {
            var endpointLight = endpoint.GetComponentInChildren<Light2D>();
            endpointLight.intensity = 0f; 
            Debug.LogWarning("Target is null! Was it destroyed or not spawned yet?");
        }
    }

    public void OnLastLevel()
    {
        onLastLevel = true;
    }

    public bool IsOnLastLevel()
    {
        return onLastLevel;
    }
}