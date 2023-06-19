using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RunHandler : MonoBehaviour
{

    public static RunHandler Instance { get {return instance;}}
    private static RunHandler instance;

    [SerializeField] int levelAmount = 2;
    private int levelCount = 0;
    private LevelSettingsSO currentLevel;

    void Awake()
    {
        if(instance != null)
        {
            Destroy(this.gameObject);
        }else{
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    public void NextLevel()
    {

        // Loading Screen if starting game
        if(SceneManager.GetActiveScene().name == GameConstants.SCENE_MENU)
        {
            Loader.LoadNextScene(GameConstants.SCENE_MAP);
            return;
        }

        if(levelCount >= levelAmount)
        {
            // go To Final Screen

            // no final screen also:
            // Re-initialize
            levelCount = 0;

            // Go back to Menu
            SceneManager.LoadScene(GameConstants.SCENE_MENU);
            return;
        }

        levelCount++;
        Loader.LoadNextScene(GameConstants.Scenes.Map.ToString());
    }

    public void LoadLevel(LevelSettingsSO level)
    {
        currentLevel = level;
        Loader.LoadNextScene(level.GetLevelScene());
        Debug.Log(this +" telling loader to start");
    }   

    public void ExitGame()
    {
        Debug.Log("Exit Game");
        Application.Quit();
    }

    public int GetLevelCount()
    {
        return levelCount;
    }

    public LevelSettingsSO GetCurrentLevel()
    {
        return currentLevel;
    }
}
