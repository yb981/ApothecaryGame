using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ingredient Name List", fileName = "new Ingredient Name List")]
public class IngredientNamesSO : ScriptableObject
{
   [SerializeField] private List<string> ingredientNames;

   public List<string> GetIngredientNames()
   {
        return ingredientNames;
   }
}
