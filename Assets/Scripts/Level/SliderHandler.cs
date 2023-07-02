using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderHandler : MonoBehaviour
{

    private Slider[] mySliders;
    private TextMeshProUGUI[] myTexts;
    [SerializeField] private Color sliderColorLocked;
    [SerializeField] private Color sliderColorNormal;
    private IngredientContainer ingredientContainer;
    private IngredientSO ingredientSO;

    private void Start() 
    {
        mySliders  = GetComponentsInChildren<Slider>(true);
        foreach(Slider slider in mySliders)
        {
            slider.enabled = false;
        }

        ingredientContainer = GetComponentInParent<IngredientContainer>(true);
        ingredientSO = ingredientContainer.GetIngredientSO();
        ingredientContainer.OnAssumedValueChanged += IngredientContainer_OnAssumedValueChanged;

        myTexts = GetComponentsInChildren<TextMeshProUGUI>(true);
    }

    private void IngredientContainer_OnAssumedValueChanged()
    {
        UpdateIngredientSO(ingredientContainer.GetIngredientSO());
    }

    private void UpdateIngredientSO(IngredientSO newIngredientSO)
    {
        ingredientSO = newIngredientSO;

        // Update Sliders
        SetSliderValues(ingredientSO.GetAssumedValues());

        // Update Lock
        UpdateSliderLockValueAndColors();
    }

    public virtual void UpdateSliderTextValues()
    {
        for (int i = 0; i < mySliders.Length; i++)
        {
            myTexts[i].text = mySliders[i].value.ToString();
        }       
    }

    public void UpdateSliderLockValueAndColors()
    {
        for (int i = 0; i < mySliders.Length; i++)
        {
            if(ingredientSO.GetLockValues()[i] == true)
            {
                mySliders[i].fillRect.GetComponent<Image>().color = sliderColorLocked;
            }else{
                mySliders[i].fillRect.GetComponent<Image>().color = sliderColorNormal;
            }
            
        } 
    }

    public int[] GetSliderValues()
    {
        int[] returnValues = new int[mySliders.Length];
        for (int i = 0; i < mySliders.Length; i++)
        {
            returnValues[i] = (int) mySliders[i].value;
        }

        return returnValues;
    }

    public void SetSliderValues(int[] values)
    {
        for(int i = 0 ; i < values.Length ; i++)
        {
            mySliders[i].value = values[i];
        }
        UpdateSliderTextValues();
    }
}
