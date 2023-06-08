using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Helper : MonoBehaviour
{
    // Components
    [Header("For Coding")]
    [SerializeField] TextMeshPro dropTMP;
    [SerializeField] TextMeshPro dropTMParrow;
    [SerializeField] TextMeshPro pickTMP;
    [SerializeField] TextMeshPro pickTMParrow;
    [SerializeField] TextMeshPro combineTMP;
    [SerializeField] TextMeshPro adjustIngredientsTMP;
    Animator myAnimator;

    // Variables
    private bool containerIsFull = false;
    private bool helperEnabled = true;

    void Start()
    {
        displayHelperText(false);
        DisplayPickUpHelp(true);
        myAnimator = GetComponent<Animator>();
    }

    public void HelperActivated(bool state)
    {
        helperEnabled = state;
        if(!state) displayHelperText(false);
    }

    public void displayHelperText(bool state)
    {
        DisplayDropHelp(state);
        DisplayPickUpHelp(state);
        DisplayCombineHelp(state);
        adjustIngredientsTMP.gameObject.SetActive(false);
    }

    public void InformIsHolding(bool state)
    {
        if(!helperEnabled) return;

        if(!containerIsFull)
        {
            myAnimator.SetBool("isHolding", state);
            DisplayDropHelp(state);
            DisplayPickUpHelp(!state);
        }
    }

    public void InformIsFull(bool state)
    {
        if(!helperEnabled) return;

        containerIsFull = state;
        DisplayCombineHelp(state);
        if(state)
        {
            DisplayDropHelp(false);
            DisplayPickUpHelp(false);
        }
    }

    public void InformIsFeedbackScreen()
    {
        if(!helperEnabled) return;

        adjustIngredientsTMP.gameObject.SetActive(true);
    }

    private void DisplayDropHelp(bool state)
    {
        dropTMP.gameObject.SetActive(state);
    }

    private void DisplayPickUpHelp(bool state)
    {
        pickTMP.gameObject.SetActive(state);
    }

    private void DisplayCombineHelp(bool state)
    {
        combineTMP.gameObject.SetActive(state);
    }
}
