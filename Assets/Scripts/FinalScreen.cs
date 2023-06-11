using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinalScreen : MonoBehaviour
{
    [SerializeField] Slider villageHealthSlider;
    [SerializeField] TextMeshProUGUI villagersSurvived;
    [SerializeField] GameObject visual;
    private GameManager gameManager;

    private void Start() 
    {
        gameManager = GameManager.Instance;
        gameManager.LevelFinished += FinalScreen_LevelFinished;
    }

    private void FinalScreen_LevelFinished(object sender, GameManager.LevelFinishedEventArgs e)
    {
        visual.gameObject.SetActive(true);

        villageHealthSlider.maxValue = e.eMaxVillageHealth;
        villageHealthSlider.value = e.eVillageHealth;
        SetVillagerSurvivedText(e.eMaxClients, e.eSurvivers);
    }

    public void ContinueButton()
    {
        SceneManager.LoadScene(GameConstants.SCENE_MENU);
    }

    private void SetVillagerSurvivedText(int max, int survivers)
    {
        villagersSurvived.text = survivers.ToString() + " / " + max.ToString();
    }
}
