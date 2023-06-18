using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameConstants 
{
    public const string VALUE1 = "Cough";
    public const string VALUE2 = "Blood";
    public const string VALUE3 = "Fever";
    // Scenes
    public const string SCENE_WAGON = "WagonGameScene";
    public const string SCENE_MENU  = "Menu";
    public const string SCENE_LOADING = "LoadingScene";
    public const string SCENE_MAP = "Map";
}

public enum PlayerFeedbackStates
{
    hit,
    totalmiss,
    miss,
    close
}
