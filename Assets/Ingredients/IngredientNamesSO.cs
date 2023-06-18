using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class IngredientNamesSO : ScriptableObject
{
   [SerializeField] private List<string> ingredientNames;

   public List<string> GetIngredientNames()
   {
        return ingredientNames;
   }
}
