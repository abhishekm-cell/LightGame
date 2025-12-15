using System.Collections;
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
    public void StartGame(int level)
    {
        levelManager.LoadLevel(level);
        uiManager.ActivateGameplayUI();
        drawManager.OnGameStart();
        levelCleared = false;
    }
    public void NextLevel()
    {
        levelManager.LoadNext();
        drawManager.OnGameStart();
        levelCleared = false;
    }
    public void SetGameOver()
    {
        drawManager.OnGameStart();
        levelManager.RestartLevel();
        uiManager.ActivateGameplayUI();
        levelCleared = false;
        Debug.Log("Game over"); 
    }

    public void ClearGamePLay()
    {
        drawManager.SetActive(false);
        drawManager.ClearAllLines();
        levelManager.ClearLevel();
        levelCleared = false;
    }

    public void LevelClearCheck()
    {
        if(onLastLevel)
        {
            return;
        }
        if (target != null && target.transform.position == endpoint.transform.position)
        {
            Debug.Log("Level cleared now!!!!!!!!!!");
            target.transform.position = endpoint.transform.position;
            levelManager.LightActivate();
            levelDataManager.UnlockNextLevel(levelManager.currentLevelIndex); 
            StartCoroutine(DelayPanelLoad());
            // drawManager.OnGameWin();
            // levelManager.ClearLevel();
        }
        else
        {
            var endpointLight = endpoint.GetComponentInChildren<Light2D>();
            endpointLight.intensity = 0f; 
            Debug.LogWarning("Target is null! Was it destroyed or not spawned yet?");
        }
    }
    private IEnumerator DelayPanelLoad()
    {
        yield return new WaitForSeconds(1.5f);
        drawManager.OnGameWin();
        levelManager.ClearLevel();
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
    public UIManager GetUIManager()=>uiManager;
}