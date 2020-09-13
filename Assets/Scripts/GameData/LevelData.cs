using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "GameData/CreateLevelData", order = 1)]
public class LevelData : ScriptableObject
{
    public Vector3[] entrancePoints;
    public Vector3[] targetPoints;
    public List<ObstacleInfo> Obstacles;
}
