using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameMenu : MonoBehaviour
{

    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject cheatSlider;
    private bool paused = false;
    private bool cheat = false;

    private void Update() 
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Pause(!paused);
        }

        if(Input.GetKeyDown(KeyCode.F2))
        {
            ActivateCheat();
        }
    }

    public void Pause(bool state)
    {
        paused = state;
        menu.SetActive(state);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(GameConstants.SCENE_MENU);
    }

    public void Exit()
    {
        Debug.Log("Exit Game");
        Application.Quit();
    }

    public void ActivateCheat()
    {
        cheat = !cheat;
        cheatSlider.SetActive(cheat);
    }
}
