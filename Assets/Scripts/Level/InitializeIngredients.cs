using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializeIngredients : MonoBehaviour
{
    [SerializeField] private IngredientNamesSO ingredientNamesSO;
    private int numberOfIngredientContainer;

    private void Start() 
    {
        numberOfIngredientContainer = FindObjectsOfType<IngredientContainer>().Length;
    }

    private IngredientSO CreateRandomIngredientSO()
    {

        // Take optional names

        IngredientSO newIngredientSO = ScriptableObject.CreateInstance<IngredientSO>();
        return newIngredientSO;
    }
}
