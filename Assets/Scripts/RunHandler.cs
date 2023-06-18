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
            Loader.LoadNextScene(GameConstants.SCENE_LOADING);
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
        SceneManager.LoadScene(GameConstants.SCENE_WAGON);
    }

    public void ExitGame()
    {
        Debug.Log("Exit Game");
        Application.Quit();
    }
}
