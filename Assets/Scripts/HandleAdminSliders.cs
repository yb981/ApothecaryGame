using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HandleAdminSliders : MonoBehaviour
{

    [SerializeField] Slider sliderCough;
    [SerializeField] Slider sliderBlood;
    [SerializeField] Slider sliderFever;
    [SerializeField] TextMeshProUGUI TMPv1;
    [SerializeField] TextMeshProUGUI TMPv2;
    [SerializeField] TextMeshProUGUI TMPv3;
    TextMeshProUGUI[] TMPs;

    // Start is called before the first frame update
    void Start()
    {
        TMPs = GetComponentsInChildren<TextMeshProUGUI>();

        for (int i = 0; i < TMPs.Length; i++)
        {
            if(TMPs[i].name == "Name1" )
            {
                TMPs[i].text = GameConstants.VALUE1;
            }else if(TMPs[i].name == "Name2")
            {
                TMPs[i].text = GameConstants.VALUE2;
            }else if(TMPs[i].name == "Name3")
            {
                TMPs[i].text = GameConstants.VALUE3;
            }
        }


        TMPv1.text = GameConstants.VALUE1;
        TMPv2.text = GameConstants.VALUE2;
        TMPv3.text = GameConstants.VALUE3;
        SetSliderValues(new int[] {0,0,0});
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetSliderValues(int[] newValues)
    {
        int v1 = newValues[0];
        sliderCough.value = v1;
        sliderBlood.value = newValues[1];
        sliderFever.value = newValues[2];
        TMPv1.text = newValues[0].ToString();
        TMPv2.text = newValues[1].ToString();
        TMPv3.text = newValues[2].ToString();
    }

    public void SetSliderVisiblity(bool state)
    {
        sliderCough.gameObject.SetActive(state);
        sliderBlood.gameObject.SetActive(state);
        sliderFever.gameObject.SetActive(state);
    }
}
