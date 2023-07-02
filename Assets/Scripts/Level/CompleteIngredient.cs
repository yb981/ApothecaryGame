using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompleteIngredient
{
    private IngredientSO ingredientSO;
    private int amountOfSliders = 3; 

    public CompleteIngredient(IngredientSO newIngredientSO)
    {
        ingredientSO = newIngredientSO;
    }

    public IngredientSO GetIngridientSO()
    {
        return ingredientSO;
    }

    public void SetIngredientSO(IngredientSO newIngredientSO)
    {
        ingredientSO = newIngredientSO;
    }
}
