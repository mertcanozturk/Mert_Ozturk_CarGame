using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CarPathData
{
   public List<DataCarMovement> carMovement;
}

[Serializable]
public class DataCarMovement
{
    public enum MoveType { Left, Right }

    public MoveType MovementType;
    public float startTime;
    public float endTime;
}