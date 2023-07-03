using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class IngredientSliders : MonoBehaviour
{
    [Header("Coding")]
    [SerializeField] TextMeshProUGUI nameField;
    [SerializeField] Button[] lockButtons;

    [Header("Coloring")]
    [SerializeField] private Color sliderColorLocked;
    [SerializeField] private Color sliderColorNormal;

    private FeedbackIngredient feedbackIngredient;
    private IngredientSO ingredientSO;

    private Slider[] mySliders;
    private TextMeshProUGUI[] myTexts;

    private void Awake() {
        // Has to be called in Awake
        // Because Feedback controller is disabling these instances in Start
        mySliders                   = GetComponentsInChildren<Slider>(true);
        myTexts                     = GetComponentsInChildren<TextMeshProUGUI>(true);
        feedbackIngredient          = GetComponentInParent<FeedbackIngredient>(true);

        // Subscribe to Slider Event
        for (int i = 0; i < mySliders.Length; i++)
        {
            int sliderIndex = i;
            mySliders[i].onValueChanged.AddListener((value) => OnSliderChanged(sliderIndex, value));
            lockButtons[i].onClick.AddListener(() => ToggleSliderLock(sliderIndex));
        }
    }

    private void Start()
    {
        // Subscribe to Slider Event
        for (int i = 0; i < mySliders.Length; i++)
        {
            int sliderIndex = i;
            mySliders[i].onValueChanged.AddListener((value) => OnSliderChanged(sliderIndex, value));
            lockButtons[i].onClick.AddListener(() => ToggleSliderLock(sliderIndex));
        }

        feedbackIngredient.OnAssumedValueChanged += FeedbackIngredient_OnAssumedValueChanged;
        feedbackIngredient.OnIngredientSOChanged += FeedbackIngredient_OnIngredientSOChanged;
    }

    private void FeedbackIngredient_OnIngredientSOChanged()
    {
        UpdateIngredientSO(feedbackIngredient.GetIngredientSO());
    }

    private void FeedbackIngredient_OnAssumedValueChanged()
    {
        UpdateIngredientSO(feedbackIngredient.GetIngredientSO());
    }

    private void OnSliderChanged(int sliderIndex, float value)
    {
        Helper.Instance.SetUserAdjustedValues(true);
        
        int[] newValues = ingredientSO.GetAssumedValues();
        newValues[sliderIndex] = (int) value;

        // Update SO 
        ingredientSO.SetAssumedValues(newValues);

        // Update Text Value
        UpdateSliderTextValues();

        // update feedback.SO
        feedbackIngredient.UpdateIngredientSOForSliderChanged(ingredientSO);
    }


    private void ToggleSliderLock(int clickedButtonId)
    {
        // Update So
        bool[] newValues = ingredientSO.GetLockValues();
        newValues[clickedButtonId] = !newValues[clickedButtonId];
        ingredientSO.SetLockValues(newValues);

        // Update Color
        UpdateSliderLockAndColors();
        
        // Update feedback.SO
        feedbackIngredient.UpdateIngredientSOForSliderChanged(ingredientSO);
    }

    private void UpdateIngredientSO(IngredientSO newIngredientSO)
    {
        ingredientSO = newIngredientSO;
        // Update Name
        SetIngredientName();

        // Update Sliders
        SetSliderValues(ingredientSO.GetAssumedValues());

        // Update Lock
        UpdateSliderLockAndColors();
    }

    private void SetIngredientName()
    {
        nameField.text = ingredientSO.getName();
    }

    public virtual void UpdateSliderTextValues()
    {
        for (int i = 0; i < mySliders.Length; i++)
        {
            myTexts[i].text = mySliders[i].value.ToString();
        }       
    }

    public void UpdateSliderLockAndColors()
    {
        for (int i = 0; i < mySliders.Length; i++)
        {
            if(ingredientSO.GetLockValues()[i] == true)
            {
                mySliders[i].fillRect.GetComponent<Image>().color = sliderColorLocked;
                mySliders[i].enabled = false;
            }else{
                mySliders[i].fillRect.GetComponent<Image>().color = sliderColorNormal;
                mySliders[i].enabled = true;
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

    private void OnDestroy() 
    {
        for (int i = 0; i < mySliders.Length; i++)
        {
            int sliderIndex = i;
            mySliders[i].onValueChanged.RemoveListener((value) => OnSliderChanged(sliderIndex, value));
            lockButtons[i].onClick.RemoveListener(() => ToggleSliderLock(sliderIndex));
        }

        feedbackIngredient.OnAssumedValueChanged -= FeedbackIngredient_OnAssumedValueChanged;
        feedbackIngredient.OnIngredientSOChanged -= FeedbackIngredient_OnIngredientSOChanged;

    }
}
