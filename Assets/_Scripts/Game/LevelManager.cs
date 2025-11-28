using UnityEngine;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour
{
    [Header("Prefab References")]
    public GameObject lightSourcePrefab; // The ball that spawns at startPt
    public GameObject endPointPrefab; // The goal/end point
    public GameObject platformPrefab; // For creating static platforms
    public GameObject[] obstaclePrefabs; // Index matches ObstacleType enum
    
    [Header("Current Level")]
    public LevelData currentLevel;
    
    [Header("Runtime References")]
    private GameObject lightSourceInstance;
    private GameObject endPointInstance;
    private List<GameObject> spawnedObjects = new List<GameObject>();
    [SerializeField] private DrawManager drawingController;
    
    void Start()
    {
        drawingController = FindObjectOfType<DrawManager>();
        
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
        
        // Spawn light source (ball) at start position
        lightSourceInstance = Instantiate(lightSourcePrefab, level.lightSource.position, Quaternion.identity);
        spawnedObjects.Add(lightSourceInstance);
        
        // Spawn end point (goal)
        endPointInstance = Instantiate(endPointPrefab, level.endPoint.position, Quaternion.identity);
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
        
        // Position at midpoint between start and end
        // Vector2 midpoint = (data.startPoint + data.endPoint) / 2f;
        // platform.transform.position = midpoint;
        
        // // Calculate angle and length for the platform
        // Vector2 direction = data.endPoint - data.startPoint;
        // float length = direction.magnitude;
        // float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        
        // platform.transform.rotation = Quaternion.Euler(0, 0, angle);
        // platform.transform.localScale = new Vector3(length, data.thickness, 1);
        
        // Set platform color
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
            // case ObstacleType.RotationPortal:
            // case ObstacleType.RotatingPlatform:
            //     var rotatingComp = obstacle.GetComponent<RotatingObstacle>();
            //     if (rotatingComp != null)
            //     {
            //         rotatingComp.rotationSpeed = data.rotationSpeed;
            //     }
            //     break;
                
            // case ObstacleType.SpringPad:
            //     var springComp = obstacle.GetComponent<SpringPad>();
            //     if (springComp != null)
            //     {
            //         springComp.springForce = data.springForce;
            //     }
            //     break;
                
            // case ObstacleType.Platform:
            //     // Static platform, no special setup needed
            //     break;
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