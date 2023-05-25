using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateDublicateOnClick : MonoBehaviour
{
    [SerializeField] IngredientSO Ingredient;

    private int antiCaugh;
    private int antiBleeding;
    private int antiFever;
    private string ingredientName = "noNameSet";
    private bool canInteract = true;

    private void Start() 
    {
        // Set the heal values from the Scritable Object
        int[] values = new int[3];
        values          = Ingredient.getValues();
        antiCaugh       = values[0];
        antiBleeding    = values[1];
        antiFever       = values[2];
        ingredientName  = Ingredient.getName();
    }

    private void OnMouseDown() 
    {
        // See if interaction is enabled
        if(!canInteract) return;
        GameObject dragCopy = Instantiate(gameObject); 
        dragCopy.GetComponent<DragAndDrop>().enabled = true;
        //dragCopy.GetComponent<CreateDublicateOnClick>().enabled = false; // cant disable because setting the values in this script
    }

    public int[] getIngredientValues()
    {
        return new int[] {antiCaugh, antiBleeding, antiFever};
    }

    public void setInteraction(bool state) 
    {
        canInteract = state;
    }
}
