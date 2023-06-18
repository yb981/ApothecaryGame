using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ContainerDisplay : MonoBehaviour
{

    ContainerCalculation myContainer;
    // Visuals
    [SerializeField] ParticleSystem particleSystemAddIng;
    [SerializeField] TextMeshProUGUI[] tmps;
    private TextMeshProUGUI textFFilledIngredients;
    string inputNumberText = " / ";

    // Start is called before the first frame update
    void Start()
    {
        myContainer = GetComponentInParent<ContainerCalculation>();
        tmps = GetComponentsInChildren<TextMeshProUGUI>();

        for (int i = 0; i < tmps.Length; i++)
        {
            if(tmps[i].name == "TextFilled" ) textFFilledIngredients = tmps[i];
        }

        updateInputNumberText();
    }

    private void InputTriggerVisuals()
    {

    }

    private void playParticleEffect()
    {
        particleSystemAddIng.Stop();
        particleSystemAddIng.Play();
    }

    public void updateInputNumberText()
    {
        if(textFFilledIngredients.name == "TextFilled"){
            inputNumberText = myContainer.getListSize() + " / " + myContainer.GetMaxIngredients();
            textFFilledIngredients.text = inputNumberText;
        }
    }

    public void SuccessfullyAddedIngredient()
    {
        playParticleEffect();
        updateInputNumberText();
    }
}
