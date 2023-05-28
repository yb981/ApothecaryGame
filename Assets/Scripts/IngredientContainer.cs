using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientContainer : MonoBehaviour
{

    [SerializeField] GameObject ingredientPiece;

    private GameObject ingredientInstace;
    Vector2 mouseOffset;

    // ingredientSO Stats
    [SerializeField] IngredientSO ingredientSO;
    private int antiCaugh;
    private int antiBleeding;
    private int antiFever;
    private string ingredientName = "noNameSet";

    private void Start() 
    {
        // Set the heal values from the Scritable Object
        int[] values = new int[3];
        values          = ingredientSO.getValues();
        antiCaugh       = values[0];
        antiBleeding    = values[1];
        antiFever       = values[2];
        ingredientName  = ingredientSO.getName();
    }

    private void OnMouseDown() 
    {
        ingredientInstace = Instantiate(ingredientPiece, Camera.main.ScreenToWorldPoint(Input.mousePosition),Quaternion.identity);
        ingredientInstace.GetComponent<IngredientPiece>().setIngredientSO(ingredientSO);
    }



}
