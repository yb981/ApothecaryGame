using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedbackLogic
{

    int[] diff = new int[3];

    public FeedbackLogic()
    {

    }

    public (int, PlayerFeedbackStates, int) OnlyOneFeedbackText(int[] playerInput, int[] clientValue)
    {
        PlayerFeedbackStates[] _tmp             = ReturnFeedbackEnumOfInputs(playerInput, clientValue);
        (int, PlayerFeedbackStates, int) tmp    = FindOnePositionStateAndDelta(_tmp);
        return tmp;
    }

    public (int, PlayerFeedbackStates, int)[] CalculateFeedback(int[] playerInput, int[] clientValue)
    {
        PlayerFeedbackStates[] feedbackEnums = ReturnFeedbackEnumOfInputs(playerInput, clientValue);
        (int, PlayerFeedbackStates, int)[] allFeedbackStates = new (int, PlayerFeedbackStates, int)[playerInput.Length];

        for (int i = 0; i < playerInput.Length; i++)
        {
            allFeedbackStates[i] = (i,feedbackEnums[i], GetInputDeltaOfPosition(i));
        }

        return allFeedbackStates;
    }

    private PlayerFeedbackStates[] ReturnFeedbackEnumOfInputs(int[] playerInput, int[] clientValue)
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

            // save the diff for later in the text output
            diff[i] = playerInput[i] - clientValue[i];

        }

        return inputResultsEnum;
    }

    private (int, PlayerFeedbackStates, int) FindOnePositionStateAndDelta(PlayerFeedbackStates[] enumResults)
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
        int delta;
        PlayerFeedbackStates currentState;

        // order of returns in enum order
        for (int i = 0; i < howManyEnums.Length; i++)
        {
            if(howManyEnums[i] >= 1)
            {
                currentState = (PlayerFeedbackStates) i;
                position = OnePositionOfValue(currentState, enumResults);
                delta = GetInputDeltaOfPosition(position);
                return (position, currentState, delta);
            }
        }

        // if fail
        Debug.Log("SOMETHING WENT WRONG");
        return (0, PlayerFeedbackStates.hit, 0);

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
        if(number == 1){ 
            return positions[number-1];
        }
        else if(number > 1){ 
            return UnityEngine.Random.Range(0,number-1);
        }else{
            return -1;
        }
    }

    
    private int GetInputDeltaOfPosition(int position)
    {
        return diff[position];
    }
}
