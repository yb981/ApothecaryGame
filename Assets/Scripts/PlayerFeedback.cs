using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class PlayerFeedback : MonoBehaviour
{
    TextMeshProUGUI playerFeedbackTMP;
    GameManager manager;

    enum PlayerFeedbackStates
    {
        hit,
        totalmiss,
        miss,
        close
    }

    [SerializeField] int totalMissValue = 4;
    [SerializeField] int miss = 2;

    void Start()
    {
        playerFeedbackTMP = GetComponentInChildren<TextMeshProUGUI>();
        manager = GameManager.Instance;
        DisplayFeedback(false);
    }

    public void StartFeedback(int[] playerInput, int[] clientValue)
    {
        Debug.Log("Started feedback");
        DisplayFeedback(true);
        PlayerFeedbackStates[] _tmp = LookThroughInputs(playerInput, clientValue);
        string textOutput = PlayerFeedbackTextOutput(PlayerFeedbackAlgo(_tmp));
        playerFeedbackTMP.text = textOutput;
        taskCompleted();
    }

    public void DisplayFeedback(bool state)
    {
        playerFeedbackTMP.gameObject.SetActive(state);
        Debug.Log("textbox active");
    }

    private PlayerFeedbackStates[] LookThroughInputs(int[] playerInput, int[] clientValue)
    {
        PlayerFeedbackStates[] inputResultsEnum = new PlayerFeedbackStates[playerInput.Length];
        for (int i = 0; i < playerInput.Length; i++)
        {

            // Compare all results and save results in Enum Array
            if(playerInput[i] == clientValue[i])
            {
                inputResultsEnum[i] = PlayerFeedbackStates.hit;
            }
            else if(Mathf.Abs(playerInput[i] - clientValue[i]) > 4)
            {
                inputResultsEnum[i] = PlayerFeedbackStates.totalmiss;
            }
            else if(Mathf.Abs(playerInput[i] - clientValue[i]) > 2)
            {
                inputResultsEnum[i] = PlayerFeedbackStates.miss;
            }else{
                inputResultsEnum[i] = PlayerFeedbackStates.close;
            }
        }

        for (int i = 0; i < playerInput.Length; i++)
        {
            Debug.Log("Results: "+i+" "+inputResultsEnum[i]);
        }

        return inputResultsEnum;
    }

    private (int, PlayerFeedbackStates) PlayerFeedbackAlgo(PlayerFeedbackStates[] enumResults)
    {
        // Create Array for all States and then fill the number of occurences + position (similar to a hashmap)
        int[] howManyEnums = new int[System.Enum.GetNames(typeof(PlayerFeedbackStates)).Length];

        // Fill the array (map)
        for(int i = 0 ; i < enumResults.Length ; i++)
        {
            PlayerFeedbackStates myVal = enumResults[i];
            howManyEnums[(int) myVal]++;
        }

        int position;
        PlayerFeedbackStates currentState;
        // order of returns
        // Check first Value first (hit)
        currentState = PlayerFeedbackStates.hit;
        if(howManyEnums[(int) currentState] >= 1)
        {
            position = OnePositionOfValue(currentState, enumResults);
            return (position, currentState);
        }

        currentState = PlayerFeedbackStates.totalmiss;
        if(howManyEnums[(int) currentState] > 1)
        {
            position = OnePositionOfValue(currentState, enumResults);
            return (position, currentState);
        }

        currentState = PlayerFeedbackStates.miss;
        if(howManyEnums[(int) currentState] > 1)
        {
            position = OnePositionOfValue(currentState, enumResults);
            return (position, currentState);
        }

        currentState = PlayerFeedbackStates.close;
        if(howManyEnums[(int) currentState] > 1)
        {
            position = OnePositionOfValue(currentState, enumResults);
            return (position, currentState);
        }

        // fastVersion
        return (0, PlayerFeedbackStates.hit);

    }

    private int OnePositionOfValue(PlayerFeedbackStates state, PlayerFeedbackStates[] enumResults)
    {
        int number = 0;
        int[] positions = new int[enumResults.Length];
        for (int i = 0; i < enumResults.Length; i++)
        {
            if(enumResults[i] == state){
                positions[number] = i;
                number++;
            }
        }
        if(number == 1) return positions[number-1];

        if(number > 1) return UnityEngine.Random.Range(0,number-1);

        return -1;
    }

    private string PlayerFeedbackTextOutput((int pos, PlayerFeedbackStates state) posAndState)
    {
        string affectedStat = "";
        string[] outputStat = {"cough","blood","fever"};
        affectedStat = outputStat[posAndState.pos];
        


        if(posAndState.state == PlayerFeedbackStates.hit) return "You cured "+ affectedStat;
        if(posAndState.state == PlayerFeedbackStates.totalmiss) return "You totally failed" + affectedStat;
        if(posAndState.state == PlayerFeedbackStates.miss) return "You failed " + affectedStat;
        if(posAndState.state == PlayerFeedbackStates.close) return "You were very close to curing " + affectedStat;
        return "";
    }

    // have to inform when phase is over
    private void taskCompleted()
    {
        manager.informPhaseCompleted();
    }
}
