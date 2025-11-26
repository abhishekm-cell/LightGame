using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private SpawnManager spawnManager;
    public bool gamestarted = false;    

    /*
        Game will start / be inplay when there are no ui panels active, if panels are active game will be in pause state



        Game states = 1. Game in play ( no panels active) 2. game pause (panels active)
        loop should be between the above 2 states,  
    */


    
}