using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Level", menuName = "LetThereBeLight/LevelData")]
public class LevelData : ScriptableObject
{
    [Header ("LightSource & Spawn point, EndPoint")]
    public Transform lightSource;
    public Transform spawnPoint;
    public Transform endPoint;

    [Header("Platforms")]
    public List<PlatformData>platforms = new List<PlatformData>();

    [Header("Obstacles")]
    public List<ObstacleData> obstacles = new List<ObstacleData>();
    
    [Header("Level Visuals")]
    public Color backgroundColor = Color.black;
    public Sprite backgroundSprite;

}

[System.Serializable]
public class PlatformData
{
    public PlatformType type;
    public Vector2 spawnPoint;
    public float thickness = 0.2f;
    public Color platformColor = Color.grey;
}


[System.Serializable]
public class ObstacleData
{
    public ObstacleType type;
    public Vector2 position;
    // public Vector2 size = Vector2.one;
    //public float rotation = 0f; // For initial rotation
    
    // [Header("Rotating Settings")]
    // public float rotationSpeed = 50f; // For RotationTriangle and RotatingPlatform
    
    // [Header("Spring Pad Settings")]
    // public float springForce = 10f; // For SpringPad
    
    // [Header("Moving Platform Settings")]
    // public Vector2 moveTarget; // For moving platforms if needed
    // public float moveSpeed = 2f;
    // public bool loopMovement = true;

}