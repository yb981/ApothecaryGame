using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;

public class IngredientContainer : MonoBehaviour
{
    [Header("For Coding")]
    [SerializeField] private GameObject ingredientPiece;
    [SerializeField] private TextMeshProUGUI nameField;

    [Header("For Individual Instance")]
    [SerializeField] IngredientSO ingredientSO;

    private GameObject ingredientInstace;
    private Vector2 mouseOffset;
    private Slider[] mySliders;
    private IngredientSliders mySliderHandler;

    // ingredientSO Stats
    private int antiCaugh;
    private int antiBleeding;
    private int antiFever;
    private string ingredientName = "noNameSet";
    private Slider[] ValueSliders;
    private int[] SliderValues;

    public event EventHandler MouseHovering;
    public event EventHandler MouseLeaving;

    private void Start() 
    {

        // Disable slider activity (only change sliders in feedback screen)
        mySliders = GetComponentsInChildren<Slider>();
        for (int i = 0; i < mySliders.Length; i++)
        {
            mySliders[i].enabled = false;
        }

        ValueSliders = GetComponentsInChildren<Slider>();

        InitializeIngridient();
    }

    private void OnMouseDown() 
    {
        ingredientInstace = Instantiate(ingredientPiece, Camera.main.ScreenToWorldPoint(Input.mousePosition),Quaternion.identity);
        ingredientInstace.GetComponent<IngredientPiece>().setIngredientSO(ingredientSO);
        ingredientInstace.GetComponent<IngredientPiece>().setAssumedValues(GetAssumedValues());
    }

    private void InitializeIngridient()
    {
        // Set the heal values from the Scritable Object
        SliderValues = new int[ingredientSO.getValues().Length];
        int[] values = new int[ingredientSO.getValues().Length];

        values          = ingredientSO.getValues();
        antiCaugh       = values[0];
        antiBleeding    = values[1];
        antiFever       = values[2];
        ingredientName  = ingredientSO.getName();

        nameField.text = ingredientName;
    }

    private void OnMouseEnter() 
    {
        if(EventSystem.current.IsPointerOverGameObject()) return;
        MouseHovering?.Invoke(this, EventArgs.Empty);
    }

    private void OnMouseExit() 
    {
        //if(EventSystem.current.IsPointerOverGameObject()) return;
        MouseLeaving(this, EventArgs.Empty);
    }

    // Set/Get
    public void SetIngredientSO(IngredientSO newIngredientSO)
    {
        ingredientSO = newIngredientSO;
        InitializeIngridient();
    }
    
    public int[] GetAssumedValues()
    {
        for (int i = 0; i < SliderValues.Length; i++)
        {   
            SliderValues[i] = (int) ValueSliders[i].value;          
        }

        return SliderValues;
    }

    public void SetSliderValues(int[] values)
    {
        mySliderHandler.SetSliderValues(values);
    }
}
