using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Level", menuName = "LetThereBeLight/LevelData")]

public class LevelDataSO : ScriptableObject
{
    public List<LevelData> levels = new List<LevelData>();
}
[Serializable]
public class LevelData
{
    public int levelNumber;
    [Header ("LightSource & Spawn point, EndPoint")]
    public Transform lightSource;
    public Transform startPoint;
    public Transform endPoint;

    [Header("Start and End point spawn positions")]
    public Vector2 startPointPosition;
    public Vector2 endPointPosition;

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
    public float targetRotation;
    public float thickness = 0.2f;
    public Color platformColor = Color.grey;
}


[System.Serializable]
public class ObstacleData
{
    public ObstacleType type;
    public Vector2 position;
    public float targetRotation;

}