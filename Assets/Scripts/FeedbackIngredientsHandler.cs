using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FeedbackIngredientsHandler : MonoBehaviour
{

    public event EventHandler SliderValuesChanged;

    private Slider[] mySliders;

    private void Awake() 
    {
        mySliders = GetComponentsInChildren<Slider>();
        for (int i = 0; i < mySliders.Length; i++)
        {
            int sliderIndex = i;
            mySliders[i].onValueChanged.AddListener((value) => OnSliderChanged(sliderIndex, value));
        }
    }

    private void OnSliderChanged(int sliderIndex, float value)
    {
        for (int i = 0; i < mySliders.Length; i++)
        {
            mySliders[i].GetComponent<Animator>().SetBool("changedValues", true);
            mySliders[i].onValueChanged.RemoveListener((value) => OnSliderChanged(sliderIndex, value));
        }
        
        SliderValuesChanged?.Invoke(this,EventArgs.Empty);
    }
}
