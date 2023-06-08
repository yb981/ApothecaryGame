using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class IngredientContainer : MonoBehaviour
{
    [Header("For Coding")]
    [SerializeField] GameObject ingredientPiece;
    
    [Header("For Individual Instance")]
    [SerializeField] IngredientSO ingredientSO;

    private GameObject ingredientInstace;
    Vector2 mouseOffset;
    TextMeshPro textName;

    // ingredientSO Stats
    private int antiCaugh;
    private int antiBleeding;
    private int antiFever;
    private string ingredientName = "noNameSet";
    private Slider[] ValueSliders;
    private int[] SliderValues;

    private void Start() 
    {
        textName = GetComponentInChildren<TextMeshPro>();

        // Set the heal values from the Scritable Object
        SliderValues = new int [ingredientSO.getValues().Length];
        int[] values = new int[ingredientSO.getValues().Length];

        values          = ingredientSO.getValues();
        antiCaugh       = values[0];
        antiBleeding    = values[1];
        antiFever       = values[2];
        ingredientName  = ingredientSO.getName();

        textName.text = ingredientName;


        ValueSliders = GetComponentsInChildren<Slider>();


    }

    private void OnMouseDown() 
    {
        ingredientInstace = Instantiate(ingredientPiece, Camera.main.ScreenToWorldPoint(Input.mousePosition),Quaternion.identity);
        ingredientInstace.GetComponent<IngredientPiece>().setIngredientSO(ingredientSO);
        ingredientInstace.GetComponent<IngredientPiece>().setAssumedValues(GetAssumedValues());
    }

    public int[] GetAssumedValues()
    {
        for (int i = 0; i < SliderValues.Length; i++)
        {   
            SliderValues[i] = (int) ValueSliders[i].value;          
        }

        return SliderValues;
    }

}
