using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameConstants 
{
    public const string VALUE1 = "cough";
    public const string VALUE2 = "blood";
    public const string VALUE3 = "fever";
    // Scenes
    public const string SCENE_WAGON = "WagonGameScene";
    public const string SCENE_MENU  = "Menu";
}

public enum PlayerFeedbackStates
{
    hit,
    totalmiss,
    miss,
    close
}
