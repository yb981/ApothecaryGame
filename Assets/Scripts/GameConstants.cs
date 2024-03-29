using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameConstants 
{
    public const string VALUE1 = "Cough";
    public const string VALUE2 = "Blood";
    public const string VALUE3 = "Fever";

    // Scenes
    public enum Scenes
    {
        WagonGameScene,
        LoadingScene,
        Menu,
        Map
    }

    public const string SCENE_WAGON = "WagonGameScene";
    public const string SCENE_MENU  = "Menu";
    public const string SCENE_LOADING = "LoadingScene";
    public const string SCENE_MAP = "Map";
    public const string SCENE_CREDITS = "Credits";
    public const string SCENE_RUNOVER = "FinishedRun";
}

public enum PlayerFeedbackStates
{
    hit,
    totalmiss,
    miss,
    close
}
