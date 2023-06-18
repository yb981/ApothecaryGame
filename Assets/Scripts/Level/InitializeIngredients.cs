using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializeIngredients : MonoBehaviour
{
    [SerializeField] private IngredientNamesSO ingredientNamesSO;
    private int numberOfIngredientContainer;
    IngredientContainer[] ingredientContainers;
    List<IngredientSO> ingredients;

    private void Start() 
    {
        ingredientContainers = FindObjectsOfType<IngredientContainer>();
        numberOfIngredientContainer = ingredientContainers.Length;
        
        // Create Random Ingredients
        ingredients = CreateRandomIngredientSOs();
        
        // Add to each container one ingredient
        if(ingredients.Count != ingredientContainers.Length) Debug.LogError("Amount of ingredients and containers are not the same");
        int count = 0;
        foreach (IngredientContainer ingredientContainer in ingredientContainers)
        {
            ingredientContainer.SetIngredientSO(ingredients[count++]);
        }
        
    }

    private List<IngredientSO> CreateRandomIngredientSOs()
    {
        List<IngredientSO> newIngredients = new List<IngredientSO>();

        // Take optional names
        List<string> namesLeft = ingredientNamesSO.GetIngredientNames();

        for (int i = 0; i < numberOfIngredientContainer; i++)
        {
            // Set Name
            int random = Random.Range(0,namesLeft.Count);
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
                    values[j] = Random.Range(0,maxValue+1);
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
                    Debug.Log("looping");
                    if(loop++ > maxLoops) {
                        
                        Debug.Log("stopping infinite loop");
                        break;
                    }
                }
            }while(!uniqueIngredientValues);
            
             Debug.Log("created 1 ing");
             
            // Add ingredient to List
            newIngredients.Add(newIngredientSO);
        }

        return newIngredients;
    }
}
