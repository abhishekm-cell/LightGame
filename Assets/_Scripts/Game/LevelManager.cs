using UnityEngine;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour
{
    [Header("Prefab References")]
    public GameObject lightSourcePrefab; // The ball that spawns at startPt
    public GameObject startPointPrefab;
    public GameObject endPointPrefab; // The goal/end point
    public GameObject platformPrefab; // For creating static platforms
    public GameObject[] obstaclePrefabs; // Index matches ObstacleType enum
    
    [Header("Current Level")]
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
        //drawingController = FindObjectOfType<DrawManager>();
        
        if (currentLevel != null)
        {
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
        Camera.main.backgroundColor = level.backgroundColor;
        
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
        
        // Set ink limit for drawing lines
        // if (drawingController != null && level.hasInkLimit)
        // {
        //     drawingController.SetInkLimit(level.inkLimit);
        // }
    }
    
    void CreatePlatform(PlatformData data)
    {
        
        GameObject platform = Instantiate(platformPrefab);

        
        platform.transform.position = data.spawnPoint;

        
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
        int obstacleIndex = (int)data.type;
        if (obstacleIndex >= obstaclePrefabs.Length || obstaclePrefabs[obstacleIndex] == null)
        {
            Debug.LogWarning($"No prefab assigned for obstacle type: {data.type}");
            return;
        }
        
        GameObject obstacle = Instantiate(obstaclePrefabs[obstacleIndex], data.position, Quaternion.Euler(0, 0, 0));
        //obstacle.transform.localScale = data.size;
        
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