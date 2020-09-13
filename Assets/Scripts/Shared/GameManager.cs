using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StateTypes
{
    WaitingForPlay,
    Playing,
    LevelUp,
    Failed
}

public class GameManager : Singleton<GameManager>
{
    public StateTypes GameState;

    public static event Action<StateTypes> OnChangeGameState;

    protected void Awake()
    {
        Application.targetFrameRate = 60;
        SetGameState(StateTypes.WaitingForPlay);
    }

    public void SetGameState(StateTypes type)
    {
        GameState = type;
        if (OnChangeGameState != null)
            OnChangeGameState(type);
    }

}
