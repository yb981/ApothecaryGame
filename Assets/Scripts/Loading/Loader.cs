using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Loader
{
  
    private static string nextScene;

    public static void LoadNextScene(string scene)
    {
        nextScene = scene;
        SceneManager.LoadScene(GameConstants.SCENE_LOADING);
    }

    public static void NextSceneWithoutLoadingScreen(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public static string GetNextScene()
    {
        return nextScene;
    }

    public static void SetNextScene(string scene)
    {
        nextScene = scene;
    }

}
