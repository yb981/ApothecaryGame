using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FeedbackIngredient : MonoBehaviour
{
    
    public event Action OnAssumedValueChanged;
    public event Action OnIngredientSOChanged;

    private IngredientSO ingredientSO;
    [SerializeField] private Image ingredientImage;

    [SerializeField] List<FeedbackIngredient> otherIngredientSliders;
    private GameObject[] ingredientContainerObjects;

    private void Awake() 
    {
        ingredientContainerObjects  = GameObject.FindGameObjectsWithTag("Ingredient");
    }

    private void SetIngredientImage()
    {
        foreach (GameObject ingredient in ingredientContainerObjects)
        {
            if(ingredient.GetComponent<IngredientContainer>().GetComponentInChildren<TextMeshProUGUI>().text == ingredientSO.getName())
            {
                SpriteRenderer originalSpriteRenderer = ingredient.GetComponentInChildren<SpriteRenderer>();
                ingredientImage.sprite = originalSpriteRenderer.sprite;
                ingredientImage.color = originalSpriteRenderer.color;
                return;
            }
        }
    } 

    private void UpdateOriginalIngredientValue()
    {
        for (int i = 0; i < ingredientContainerObjects.Length; i++)
        {
            // If Ingredient Name equals to the name this ingredient copy
            // Basically finding the right original ingredient
            if(String.Equals(ingredientContainerObjects[i].GetComponent<IngredientContainer>().GetIngredientName(),ingredientSO.getName()))
            {
                Debug.Log("updating:" + ingredientSO.getName());
                ingredientContainerObjects[i].GetComponent<IngredientContainer>().SetIngredientSO(ingredientSO);
            }
        }

        foreach (FeedbackIngredient feedbackIng in otherIngredientSliders)
        {
            if(feedbackIng.GetIngredientName() == ingredientSO.getName())
            {   
                feedbackIng.SetIngredientSO(ingredientSO);
            } 
        }
    }

    public void SetIngredientSO(IngredientSO newIngredientSO)
    {
        ingredientSO = newIngredientSO;
        SetIngredientImage();
        OnIngredientSOChanged?.Invoke();
    }

    public void UpdateIngredientSOForSliderChanged(IngredientSO newIngredientSO)
    {
        ingredientSO = newIngredientSO;
        UpdateOriginalIngredientValue();
    }

    public void SetAssumedValues(int[] values)
    {
        ingredientSO.SetAssumedValues(values);
        OnAssumedValueChanged?.Invoke();
    }

    public string GetIngredientName()
    {
        return ingredientSO.getName();
    }

    public IngredientSO GetIngredientSO()
    {
        return ingredientSO;
    }
}
