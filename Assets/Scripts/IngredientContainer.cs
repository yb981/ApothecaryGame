using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class IngredientContainer : MonoBehaviour
{

    [SerializeField] GameObject ingredientPiece;
    [SerializeField] IngredientSO ingredientSO;

    private GameObject ingredientInstace;
    Vector2 mouseOffset;
    TextMeshPro textName;

    // ingredientSO Stats
    private int antiCaugh;
    private int antiBleeding;
    private int antiFever;
    private string ingredientName = "noNameSet";

    private void Start() 
    {
        textName = GetComponentInChildren<TextMeshPro>();

        // Set the heal values from the Scritable Object
        int[] values = new int[3];
        values          = ingredientSO.getValues();
        antiCaugh       = values[0];
        antiBleeding    = values[1];
        antiFever       = values[2];
        ingredientName  = ingredientSO.getName();

        textName.text = ingredientName;
    }

    private void OnMouseDown() 
    {
        ingredientInstace = Instantiate(ingredientPiece, Camera.main.ScreenToWorldPoint(Input.mousePosition),Quaternion.identity);
        ingredientInstace.GetComponent<IngredientPiece>().setIngredientSO(ingredientSO);
    }



}
