using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ingredient", fileName = "new Ingredient")]
public class IngredientSO : ScriptableObject
{
    [SerializeField] string ingredientName = "Ingredient Name";
    [SerializeField] int antiCaugh;
    [SerializeField] int antiBleeding;
    [SerializeField] int antiFever;
    [SerializeField] int[] assumedValues = {0,0,0};
    [SerializeField] bool[] lockValues = {false,false,false};

    public int[] getValues()
    {
        return new int[] {antiCaugh, antiBleeding, antiFever};
    }

    public void SetValues(int[] values)
    {
        antiCaugh = values[0];
        antiBleeding = values[1];
        antiFever = values[2];
    }

    public string getName()
    {
        return ingredientName;
    }

    public void SetIngredientName(string newName)
    {
        ingredientName = newName;
    }

    public int[] GetAssumedValues()
    {
        return assumedValues;
    }

    public void SetAssumedValues(int[] values)
    {
        assumedValues = values;
    }

    public bool[] GetLockValues()
    {
        return lockValues;
    }

    public void SetLockValues(bool[] values)
    {
        lockValues = values;
    }
}
