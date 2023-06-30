using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializeIngredients : MonoBehaviour
{
    [SerializeField] private IngredientNamesSO ingredientNamesSO;
    private int numberOfIngredientContainer;
    [SerializeField] private InitializeClients initializeClients;
    IngredientContainer[] ingredientContainers;
    List<CompleteIngredient> ingredients;
    List<IngredientSO> ingredientsSO;

    private void Start() 
    {
        GameManager.Instance.LevelFinished += GameManager_OnLevelFinished;

        ingredientContainers = FindObjectsOfType<IngredientContainer>();
        numberOfIngredientContainer = ingredientContainers.Length;
        
        // Get Ingredients From Run Handler

        ingredients = RunHandler.Instance.GetCurrentEndlessRunCompleteIngredients();

        // Set SOs
        ingredientsSO = new List<IngredientSO>();
        for (int i = 0; i < ingredients.Count; i++)
        {
            ingredientsSO.Add(ingredients[i].GetIngridientSO());
        }
        
        // Add to each container one ingredient
        if(ingredients.Count != ingredientContainers.Length) Debug.LogError("Amount of ingredients and containers are not the same");
        int count = 0;
        foreach (IngredientContainer ingredientContainer in ingredientContainers)
        {
            ingredientContainer.SetIngredientSO(ingredients[count].GetIngridientSO());
            // Update Stats
            ingredientContainer.SetSliderValues(ingredients[count].GetIngredientPlayerValues());
            count++;
        }
    }

    private void GameManager_OnLevelFinished(object sender, GameManager.LevelFinishedEventArgs e)
    {
        // Save into RunHandler
        for (int i = 0; i < ingredientContainers.Length; i++)
        {   
            ingredients[i].SetIngredientPlayerValues( ingredientContainers[i].GetAssumedValues());
        }

        RunHandler.Instance.SetCurrentEndlessRunCompleteIngredients(ingredients);
    }

    public List<IngredientSO> GetIngredientSOs()
    {
        return ingredientsSO;
    }
}
