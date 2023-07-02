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
    List<IngredientSO> ingredientsSO;

    private void Start() 
    {
        GameManager.Instance.LevelFinished += GameManager_OnLevelFinished;

        ingredientContainers = FindObjectsOfType<IngredientContainer>();
        numberOfIngredientContainer = ingredientContainers.Length;
        
        // Get Ingredients From Run Handler
        ingredientsSO = RunHandler.Instance.GetCurrentEndlessIngredients();
        
        // Add to each container one ingredient
        if(ingredientsSO.Count != ingredientContainers.Length) Debug.LogError("Amount of ingredients and containers are not the same");
        for(int i = 0; i < ingredientContainers.Length; i++)
        {
            ingredientContainers[i].SetIngredientSO(ingredientsSO[i]);
        }
    }

    private void GameManager_OnLevelFinished(object sender, GameManager.LevelFinishedEventArgs e)
    {
        Debug.Log("saving SOs");
        // Save into RunHandler
        for (int i = 0; i < ingredientContainers.Length; i++)
        {   
            ingredientsSO[i] = ingredientContainers[i].GetIngredientSO();
            Debug.Log("-"+ingredientsSO[i].getName());
        }

        RunHandler.Instance.SetCurrentEndlessRunIngredients(ingredientsSO);
    }

    public List<IngredientSO> GetIngredientSOs()
    {
        return ingredientsSO;
    }
}
