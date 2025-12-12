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
    [SerializeField] private LevelDataManager levelDataManager;
    [SerializeField] private UIManager uiManager;
    public bool gameOver { get; private set; }
    public bool gamestarted = false;  
    public bool levelCleared = false;
    private bool onLastLevel = false;

    //public event System.Action GameOver;

    void Awake()
    {
        levelDataManager = new LevelDataManager();
        //setting refrence
        levelManager.SetReferece(this);
        levelDataManager.SetReferece(this);
        uiManager.SetReferece(this);
        drawManager.SetRefernce(this);
        //loading data
        levelDataManager.LoadData();
    }

    /*
        Game will start / be inplay when there are no ui panels active, if panels are active game will be in pause state



        Game states = 1. Game in play ( no panels active) 2. game pause (panels active)
        loop should be between the above 2 states,  
    */
    public void StartGame(int level)
    {
        levelManager.LoadLevel(level);
        uiManager.ActivateGameplayUI();
        drawManager.OnGameStart();
    }
    public void NextLevel()
    {
        levelManager.LoadNext();
        drawManager.OnGameStart();

    }
    public void SetGameOver()
    {
        gameOver = true;
        levelManager.RestartLevel();
        uiManager.ActivateGameplayUI();
        drawManager.OnGameStart();
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
            levelDataManager.UnlockNextLevel(levelManager.currentLevelIndex); 
            drawManager.OnGameWin();
            uiManager.ActivateLevelCompletePanel();
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
    public int GetLevelData()=>levelManager.allLevels.levels.Count;

    //managers
    public LevelManager GetLevelManager()=>levelManager;
    public DrawManager GetDrawManager()=>drawManager;
    public LevelDataManager GetLevelDataManager()=>levelDataManager;
    public BlackHole GetBlackHole()=>blackHole;
}