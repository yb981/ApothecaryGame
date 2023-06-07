using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class PlayerFeedback : MonoBehaviour
{
    TextMeshProUGUI playerFeedbackTMP;
    Image background;
    GameManager manager;

    FeedbackLogic myFeedbackLogic;

    enum PlayerFeedbackStates
    {
        hit,
        totalmiss,
        miss,
        close
    }

    //[SerializeField] int totalMissValue = 4;
    //[SerializeField] int miss = 2;

    void Start()
    {
        playerFeedbackTMP = GetComponentInChildren<TextMeshProUGUI>();
        background =  GetComponentInChildren<Image>();
        manager = GameManager.Instance;
        myFeedbackLogic = new FeedbackLogic();
        DisplayFeedback(false);
    }

    public void StartFeedback(int[] playerInput, int[] clientValue)
    {
        Debug.Log("Started feedback");
        DisplayFeedback(true);
        playerFeedbackTMP.text = myFeedbackLogic.CalculateFeedback(playerInput,clientValue);

        //taskCompleted();
    }

    public void DisplayFeedback(bool state)
    {
        playerFeedbackTMP.gameObject.SetActive(state);
        //background.gameObject.SetActive(state);
        background.enabled = state;
    }

    // have to inform when phase is over
    private void taskCompleted()
    {
        if(GameManager.Instance.getGamePhase() == patientPhase.Score){
            DisplayFeedback(false);
            manager.informPhaseCompleted();
        } 
    }

    public void PlayerWantsToContinue()
    {
        taskCompleted();
    }
}
