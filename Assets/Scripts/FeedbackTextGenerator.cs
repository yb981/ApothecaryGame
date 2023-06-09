using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedbackTextGenerator
{
    // Constructors
    public FeedbackTextGenerator()
    {

    }

    public string GeneratePlayerFeedbackTextOutput((int pos, PlayerFeedbackStates state, int difference)[] posAndState, string[] inputIngredients)
    {
        string output = "";

        // Inserted Ingredients
        output += CreateStringForIngredientInput(inputIngredients);
        output += "\n \n";

        // Stats comparisim for each Stat
        for (int i = 0; i < posAndState.Length; i++)
        {
            output += CreateOutputForOneIngredient(posAndState[i]);
            output += "\n";
        }
        return output;
    }

    private string CreateStringForIngredientInput(string[] inputIngredients)
    {
        string ingredientInputText = "";

        ingredientInputText = "You added: ";
        for (int i = 0; i < inputIngredients.Length; i++)
        {
            ingredientInputText += inputIngredients[i]+", ";
        }
        return ingredientInputText;
    }

    private string CreateOutputForOneIngredient((int pos, PlayerFeedbackStates state, int difference) posAndState)
    {
        string affectedStat = "";
        string amountStat = "";
        string[] outputStat = {"cough","blood","fever"};
        string[] amountText = {"", "less ", "much "};
        affectedStat = outputStat[posAndState.pos];
        
        switch(Mathf.Sign(posAndState.difference))
        {
            case 0:  break;
            case 1: amountStat = amountText[1]; break;
            case -1: amountStat = amountText[2]; break;
            default: break;
        }
        



        if(posAndState.state == PlayerFeedbackStates.hit) return "\t You cured "+ affectedStat;
        if(posAndState.state == PlayerFeedbackStates.totalmiss) return "\t You put way too "+ amountStat + affectedStat;
        if(posAndState.state == PlayerFeedbackStates.miss) return "\t You put too "+ amountStat + affectedStat;
        if(posAndState.state == PlayerFeedbackStates.close) return "\t You put just a little too "+ amountStat + affectedStat;
        return "";
    }

}
