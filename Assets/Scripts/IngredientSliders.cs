using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class IngredientSliders : MonoBehaviour
{
    [Header("Coding")]
    [SerializeField] TextMeshProUGUI nameField;

    private Slider[] mySliders;
    private TextMeshProUGUI[] myTexts;
    private GameObject[] ingredientObjects;
    private string ingName = "not set";

    private void Awake() 
    {
        // Get Components
        mySliders           = GetComponentsInChildren<Slider>();
        myTexts             = GetComponentsInChildren<TextMeshProUGUI>();
        
    }

    private void Start() 
    {
        ingredientObjects   = GameObject.FindGameObjectsWithTag("Ingredient");

        // Set Text
        UpdateSliderBar();
    }

    public void UpdateSliderValues()
    {
        UpdateSliderBar();
        UpdateOriginalIngredientValue();        
    }

    public void SetSliderValues(int[] values)
    {
        for (int i = 0; i < values.Length; i++)
        {
            mySliders[i].value = values[i];
        }
        UpdateSliderValues();
    }

    private void UpdateSliderBar()
    {
        for (int i = 0; i < mySliders.Length; i++)
        {
            myTexts[i].text = mySliders[i].value.ToString();
        }
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
                ingredientObjects[i].GetComponentInChildren<HandleAdminSliders>().SetSliderValues(GetSliderValues());
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
