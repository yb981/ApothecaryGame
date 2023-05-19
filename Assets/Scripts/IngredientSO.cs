using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(menuName = "Ingredient", fileName = "new Ingredient")]
public class IngredientSO : ScriptableObject
{
    [SerializeField] string ingredientName = "moonflower";
    [SerializeField] int antiCaugh;
    [SerializeField] int antiBleeding;
    [SerializeField] int antiFever;

}
