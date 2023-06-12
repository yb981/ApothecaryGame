using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderHandler : MonoBehaviour
{

    protected Slider[] mySliders;
    protected TextMeshProUGUI[] myTexts;

    private void Start() 
    {
        mySliders           = GetComponentsInChildren<Slider>(true);
        myTexts             = GetComponentsInChildren<TextMeshProUGUI>(true);
        //UpdateSliderTextValues();
    }

    public void SetSliderValues(int[] values)
    {
        for (int i = 0; i < values.Length; i++)
        {
            mySliders[i].value = values[i];
        }
        UpdateSliderTextValues();
    }

    public virtual void UpdateSliderTextValues()
    {
        for (int i = 0; i < mySliders.Length; i++)
        {
            myTexts[i].text = mySliders[i].value.ToString();
        }       
    }
}
