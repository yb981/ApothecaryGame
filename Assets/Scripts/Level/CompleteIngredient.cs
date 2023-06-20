using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompleteIngredient
{
    private IngredientSO ingredientSO;
    private int[] ingredientPlayerSettings;
    private int amountOfSliders = 3; 

    public CompleteIngredient(IngredientSO ingredientSO)
    {
        ingredientPlayerSettings = new int[amountOfSliders];
        this.ingredientSO = ingredientSO;
    }

    public IngredientSO GetIngridientSO()
    {
        return ingredientSO;
    }

    public void SetIngredientSO(IngredientSO newIngredientSO)
    {
        ingredientSO = newIngredientSO;
    }

    public void SetIngredientPlayerValues(int[] newValues)
    {
        for (int i = 0; i < ingredientPlayerSettings.Length; i++)
        {
            ingredientPlayerSettings[i] = newValues[i];
        }
    }

    public int[] GetIngredientPlayerValues()
    {
        return ingredientPlayerSettings;
    }
}
