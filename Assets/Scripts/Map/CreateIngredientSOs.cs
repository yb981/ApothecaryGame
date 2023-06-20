using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateIngredientSOs : MonoBehaviour
{

    [SerializeField] private IngredientNamesSO ingredientNamesSO;
    [SerializeField] private int numberOfIngredientContainer;
    List<IngredientSO> ingredients;
    

    public List<IngredientSO> CreateRandomIngredientSOs()
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
    }

    
}
