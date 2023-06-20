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


    /*
        private List<IngredientSO> CreateRandomIngredientSOs()
        {
            List<IngredientSO> newIngredients = new List<IngredientSO>();

            // Take optional names
            List<string> namesLeft = new List<string>( ingredientNamesSO.GetIngredientNames());

            for (int i = 0; i < numberOfIngredientContainer; i++)
            {
                // Set Name
                int random = Random.Range(0,namesLeft.Count);
                Debug.Log(random);
                string newName = namesLeft[random];
                namesLeft.RemoveAt(random);

                IngredientSO newIngredientSO = ScriptableObject.CreateInstance<IngredientSO>();
                newIngredientSO.SetIngredientName(newName);



                // Set Values
                int amountOfValues = 3;
                int[] values = new int[amountOfValues];
                bool uniqueIngredientValues = true;
                int maxLoops = 10;
                int loop = 0;

                // Repeat if it is like any of the other ingredients
                do{
                    uniqueIngredientValues = true;
                    for (int j = 0; j < amountOfValues; j++)
                    {

                        int maxValue = 4;
                        // decrease likeliness of 4
                        // 0 = 0,5
                        // 1 = 1,6
                        // 2 = 2,7
                        // 3 = 3,8
                        // 4 = 4,
                        int numbers = (maxValue*2);
                        int newValue = Random.Range(0,numbers);
                        newValue = newValue % (maxValue+1);

                        values[j] = newValue;
                    }
                    newIngredientSO.SetValues(values);

                    // check if not like other ingredients
                    foreach (IngredientSO otherIngredient in newIngredients)
                    {
                        int[] otherValues = otherIngredient.getValues();

                        // Repeat if same values
                        if(otherValues[0] == values[0] && otherValues[1] == values[1] && otherValues[2] == values[2]) 
                        {
                            uniqueIngredientValues = false;
                            break;
                        }
                        if(loop++ > maxLoops) {

                            Debug.Log("stopping infinite loop");
                            break;
                        }
                    }
                }while(!uniqueIngredientValues);

                 Debug.Log("created 1 ing after " + loop + " loops");

                // Add ingredient to List
                newIngredients.Add(newIngredientSO);
            }

            return newIngredients;
        }*/

    public List<IngredientSO> GetIngredientSOs()
    {
        return ingredientsSO;
    }
}
