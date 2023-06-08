using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ContainerDisplay : MonoBehaviour
{

    ContainerCalculation myContainer;
    // Visuals
    [SerializeField] ParticleSystem particleSystemAddIng;
    TextMeshPro textFFilledIngredients;
    string inputNumberText = " / ";

    // Start is called before the first frame update
    void Start()
    {
        myContainer = GetComponentInParent<ContainerCalculation>();
        textFFilledIngredients = GetComponentInChildren<TextMeshPro>();
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
        if(textFFilledIngredients == null) textFFilledIngredients = GetComponentInChildren<TextMeshPro>();
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
