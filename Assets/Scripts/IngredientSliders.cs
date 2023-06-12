using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class IngredientSliders : SliderHandler
{
    [Header("Coding")]
    [SerializeField] TextMeshProUGUI nameField;

    private GameObject[] ingredientObjects;
    private string ingName = "not set";
    private Animator[] myAnimator;

    private void Awake() {
        // Has to be called in Awake
        // Becuase Feedback controller is disabling these instances in Start
        mySliders           = GetComponentsInChildren<Slider>(true);
        myTexts             = GetComponentsInChildren<TextMeshProUGUI>(true);
        ingredientObjects   = GameObject.FindGameObjectsWithTag("Ingredient");
        myAnimator          = GetComponentsInChildren<Animator>();
    }

    private void OnSliderChanged(int sliderIndex, float value)
    {
        Helper.Instance.SetUserAdjustedValues(true);
        UpdateSliderTextValues();
    }

    private void Start()
    {
        // Subscribe to Slider Event
        for (int i = 0; i < mySliders.Length; i++)
        {
            int sliderIndex = i;
            mySliders[i].onValueChanged.AddListener((value) => OnSliderChanged(sliderIndex, value));
        }
    }

    public override void UpdateSliderTextValues()
    {
        base.UpdateSliderTextValues();
        UpdateOriginalIngredientValue(); 
    }

    public void SetIngredientName(string name)
    {
        ingName = name;
        for (int i = 0; i < myTexts.Length; i++)
        {
            if(myTexts[i].name == "TMP Name") myTexts[i].text = ingName;
        }
    }

    private void UpdateOriginalIngredientValue()
    {
        for (int i = 0; i < ingredientObjects.Length; i++)
        {
            // If Ingredient Name equals to the name this ingredient copy
            // Basically finding the right original ingredient
            if(ingredientObjects[i].GetComponent<IngredientContainer>().GetComponentInChildren<TextMeshProUGUI>().text == ingName)
            {
                ingredientObjects[i].GetComponentInChildren<SliderHandler>().SetSliderValues(GetSliderValues());
            }
        }
    }

    private int[] GetSliderValues()
    {
        int[] returnValues = new int[mySliders.Length];
        for (int i = 0; i < mySliders.Length; i++)
        {
            returnValues[i] = (int) mySliders[i].value;
        }

        return returnValues;
    }

    
}
