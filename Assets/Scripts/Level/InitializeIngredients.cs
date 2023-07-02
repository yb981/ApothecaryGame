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
        for(int i = 0; i < ingredientContainers.Length; i++)
        {
            ingredientContainers[i].SetIngredientSO(ingredients[i].GetIngridientSO());
        }
    }

    private void GameManager_OnLevelFinished(object sender, GameManager.LevelFinishedEventArgs e)
    {
        Debug.Log("saving SOs");
        // Save into RunHandler
        for (int i = 0; i < ingredientContainers.Length; i++)
        {   
            ingredients[i].SetIngredientSO( ingredientContainers[i].GetIngredientSO());
            Debug.Log("-"+ingredients[i].GetIngridientSO().getName());
        }

        RunHandler.Instance.SetCurrentEndlessRunCompleteIngredients(ingredients);
    }

    public List<IngredientSO> GetIngredientSOs()
    {
        return ingredientsSO;
    }
}
