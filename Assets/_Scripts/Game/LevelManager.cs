using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class LevelManager : MonoBehaviour
{
    [Header("Prefab References")]
    public GameObject lightSourcePrefab; // The ball that spawns at startPt
    public GameObject startPointPrefab;
    public GameObject endPointPrefab; // The goal/end point
    public GameObject platformPrefab; // For creating static platforms
   public PreFabData  obstacleData;

   private Dictionary<ObstacleType, GameObject> obstaclePrefabData = new Dictionary<ObstacleType, GameObject>();
    
    [Header("Current Level")]
    public LevelDataSO allLevels;
    public int currentLevelIndex = 1;
    public LevelData currentLevel;
    
    [Header("Runtime References")]
    
    private GameObject startPointInstance;
    private GameObject lightSourceInstance;
    private GameObject endPointInstance;
    
    private List<GameObject> spawnedObjects = new List<GameObject>();
    [SerializeField] private DrawManager drawingController;
    [SerializeField] private GameManager gameManager;
    
    void Start()
    {   
        foreach(Prefab p in obstacleData.prefabDataList)
        {
            obstaclePrefabData.Add(p.obstacleType, p.prefab);
        }
        if (allLevels != null && allLevels.levels.Count > 0)
        {
            currentLevel = allLevels.levels[currentLevelIndex-1];
            LoadLevel(currentLevel);
        }
    
    }
    
    public void LoadLevel(LevelData level)
    {
        if (level == null)
        {
            Debug.LogError("No level data assigned!");
            return;
        }
        
        ClearLevel();
        currentLevel = level;
        
        // Set background
       // Camera.main.backgroundColor = level.backgroundColor;
        
        startPointInstance = Instantiate(startPointPrefab, level.startPointPosition, Quaternion.identity);
        spawnedObjects.Add(startPointInstance);
        // Spawn light source (ball) at start position
        lightSourceInstance = Instantiate(lightSourcePrefab, level.startPointPosition, Quaternion.identity);
        spawnedObjects.Add(lightSourceInstance);
        lightSourceInstance.GetComponent<LightSource>().SetGameManager(gameManager);
        
        // Spawn end point (goal)
        endPointInstance = Instantiate(endPointPrefab, level.endPointPosition, Quaternion.identity);
        spawnedObjects.Add(endPointInstance);
        
        // Create static platforms from level data
        foreach (var platformData in level.platforms)
        {
            CreatePlatform(platformData);
        }
        
        // Create obstacles from level data
        foreach (var obstacleData in level.obstacles)
        {
            CreateObstacle(obstacleData);
        }
        
    }
    
    void CreatePlatform(PlatformData data)
    {
        
        GameObject platform = Instantiate(platformPrefab);

        
        platform.transform.position = data.spawnPoint;

        platform.transform.rotation = Quaternion.Euler(0, 0, data.targetRotation);
        
        platform.transform.localScale = new Vector3(platform.transform.localScale.x,1);

        SpriteRenderer sr = platform.GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            sr.color = data.platformColor;
        }

        
        spawnedObjects.Add(platform);
    }

    void CreateObstacle(ObstacleData data)
    {
        GameObject obstacle = Instantiate(obstaclePrefabData[data.type], data.position, Quaternion.Euler(0, 0, data.targetRotation));
        if(obstacle.GetComponent<BlackHole>() != null)
        {
            obstacle.GetComponent<BlackHole>().SetGameManager(gameManager);
        }
        
        
        // Setup specific obstacle types based on their components
        switch (data.type)
        {
            case ObstacleType.RotationPortal:
                break;
            case ObstacleType.BlackHole:
                break;
            case ObstacleType.SpringPad:
                break;
            case ObstacleType.SpeedBoost:
                break;
        }
        Debug.Log("Obstacle Data Type = " + data.type + " (int = " + (int)data.type + ")");
        
        spawnedObjects.Add(obstacle);
    }
    
    void ClearLevel()
    {
        // Destroy all spawned objects
        foreach (GameObject obj in spawnedObjects)
        {
            if (obj != null)
                Destroy(obj);
        }
        spawnedObjects.Clear();
        
        // Clear any drawn lines from previous level
        if (drawingController != null)
        {
            drawingController.ClearAllLines();
        }
    }
    
    public void RestartLevel()
    {
        LoadLevel(currentLevel);
    }

    public void LoadNext()
    {
        currentLevelIndex++;

        if (currentLevelIndex > allLevels.levels.Count)
        {
            Debug.Log("Completed All Levels!");
            return;
        }

        currentLevel = allLevels.levels[currentLevelIndex-1];
        // LoadLevel(currentLevel);
        StartCoroutine(DelayLoadNext(currentLevel));// for TESTING
        gameManager.levelCleared = false;
    }
    private IEnumerator DelayLoadNext(LevelData nextLevel) // FOR TESTING
    {
        yield return new WaitForSeconds(2f);
        LoadLevel(nextLevel);   
    }

    
    public void LoadNextLevel(LevelData nextLevel)
    {
        LoadLevel(nextLevel);
        
    }
    

    
    // Helper methods for getting references
    public GameObject GetLightSource()
    {
        return lightSourceInstance;
    }
    
    public GameObject GetEndPoint()
    {
        return endPointInstance;
    }
}