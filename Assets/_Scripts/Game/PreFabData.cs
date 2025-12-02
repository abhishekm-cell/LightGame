using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "LetThereBeLight/PreFabData")]
public class PreFabData : ScriptableObject
{
    public List<Prefab> prefabDataList;
}

[Serializable]
public class Prefab
{
    public GameObject prefab;
    public ObstacleType obstacleType;
}