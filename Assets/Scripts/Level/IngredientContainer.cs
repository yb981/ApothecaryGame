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
    private SliderHandler mySliderHandler;

    private bool canInteract;

    // ingredientSO Stats
    private int antiCaugh;
    private int antiBleeding;
    private int antiFever;
    private string ingredientName = "noNameSet";
    private Slider[] ValueSliders;
    private int[] SliderValues;

    private void Start() 
    {
        mySliderHandler = GetComponentInChildren<SliderHandler>();

        // Disable slider activity (only change sliders in feedback screen)
        mySliders = GetComponentsInChildren<Slider>();
        for (int i = 0; i < mySliders.Length; i++)
        {
            mySliders[i].enabled = false;
        }

        ValueSliders = GetComponentsInChildren<Slider>();

        InitializeIngridient();

        GameManager.Instance.OnGamePhaseChanged += GameManager_OnGamePhaseChanged;
    }

    private void OnMouseDown() 
    {
        if(canInteract){
            CreateIngredient();
        }
    }

    private void GameManager_OnGamePhaseChanged()
    {
        SetInteractionBasedOnGamePhase();
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

    private void SetInteractionBasedOnGamePhase()
    {
        if(GameManager.Instance.getGamePhase() == patientPhase.Score)
        {
            canInteract = false;
        }else{
            canInteract = true;
        }
    }

    private void CreateIngredient()
    {
        ingredientInstace = Instantiate(ingredientPiece, Camera.main.ScreenToWorldPoint(Input.mousePosition),Quaternion.identity);
        ingredientInstace.GetComponent<IngredientPiece>().setIngredientSO(ingredientSO);
        ingredientInstace.GetComponent<IngredientPiece>().setAssumedValues(GetAssumedValues());
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
