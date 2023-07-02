using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ContainerCalculation : MonoBehaviour
{
    [Header("For Coding")]
    [SerializeField] Helper helper;

    // Events
    public event EventHandler OnContainerFull;
    public event EventHandler OnContainerAdd;
    public event EventHandler OnContainerEmpty;

    // Input Values
    int totalCaugh = 0;
    int totalBlood = 0;
    int totalFever = 0;
    
    int maxIngredients = 3;
    int[] assumedValuesinContainer;
    List<IngredientSO> filledIngredients = new List<IngredientSO>();
    List<IngredientSO> oldIngredients = new List<IngredientSO>();

    // Visual Feedback
    ContainerDisplay myPresentation;
    HandleAdminSliders handleSliders;
    HandleAdminSliders cheatSliders;

    private void Start() 
    {
        myPresentation = GetComponentInChildren<ContainerDisplay>(true);
        handleSliders = GetComponentsInChildren<HandleAdminSliders>(true)[0];
        cheatSliders = GetComponentsInChildren<HandleAdminSliders>(true)[1];

        assumedValuesinContainer = new int[maxIngredients];
    }

    public void AddValuesAssumed(int[] values)
    {
        for (int i = 0; i < assumedValuesinContainer.Length; i++)
        {
            assumedValuesinContainer[i] += values[i];
        }
    }

    public bool addIngredient(IngredientSO ingredient, int[] assumedValues)
    {
            if(assumedValues == null){
                Debug.Log("CONTAINER: called add ingredient but int[] assumed Value is not set! return");
                return false;
            }  

        if(filledIngredients.Count < 3)
        {
            // Trigger Event
            OnContainerAdd?.Invoke(this, EventArgs.Empty);

            // add the SO (copy) of the ingredient into the List
            filledIngredients.Add(createCopyOfSOObject(ingredient));
            CalculateIngredientInContainer();
            AddValuesAssumed(assumedValues);
            
            // Visuals
            myPresentation.SuccessfullyAddedIngredient();
            handleSliders.SetSliderValues(assumedValuesinContainer);
            cheatSliders.SetSliderValues(new int[] {totalCaugh,totalBlood,totalFever});

            // If Full handle options
            if(filledIngredients.Count == maxIngredients)
            {
                helper.InformIsFull(true);
                OnContainerFull?.Invoke(this, EventArgs.Empty);
            }

            return true;
        }else{
            Debug.Log("could not add, already full");
            return false;
        }
    }

    private IngredientSO createCopyOfSOObject(IngredientSO old)
    {
        IngredientSO ingredientSO = old;
        return ingredientSO;
    }

    private void CalculateIngredientInContainer()
    {
        // Reset Values first
        setValues(0,0,0);

        for(int i = 0; i < filledIngredients.Count; i++)
        {
            int[] tmp = new int[3];
            tmp = filledIngredients[i].getValues();
            addValues(tmp[0], tmp[1], tmp[2]);
        }
    }

    public int[] mixInsertIngredients()
    {
        CalculateIngredientInContainer();

        // create one potion or something

        // Display

        clearContainer();

        return new int[] {totalCaugh, totalBlood, totalFever};
    }


    public void inPutCombine()
    {
        
        if(GameManager.Instance.getGamePhase() == patientPhase.Wait) {

            if(filledIngredients.Count == 3)
            {
                GameManager.Instance.informPhaseCompleted();
            }else{
                // Tell player to put more ingredients
                Debug.Log("still missing ingredients to combine");
            }
        }else{
            Debug.Log("trying to mix when not in correct gamephase.");
            Debug.Log("phase: "+ GameManager.Instance.getGamePhase());
        }
    }

    private void clearContainer()
    {
        // Save into old ingredients
        oldIngredients.Clear();
        oldIngredients = new List<IngredientSO>(filledIngredients);

        // Clear List
        filledIngredients.Clear();
        myPresentation.updateInputNumberText();
        SetValuesAssumed(new int[] {0,0,0});
        handleSliders.SetSliderValues(assumedValuesinContainer);

        // Invoke Event
        OnContainerEmpty?.Invoke(this, EventArgs.Empty);
    }

    public void inPutClear()
    {
        clearContainer();
    }

    public int getListSize()
    {
        return filledIngredients.Count;
    }

    // Set & Get
    public void SetValuesAssumed(int[] values)
    {
        for (int i = 0; i < assumedValuesinContainer.Length; i++)
        {
            assumedValuesinContainer[i] = values[i];
        }        
    }

    public int[] getValues()
    {
        return new int[] {totalCaugh, totalBlood, totalFever};
    }

    public int GetMaxIngredients()
    {
        return maxIngredients;
    }

    public List<IngredientSO> GetLatestContainerIngredients()
    {
        return oldIngredients;
    }
    

    public void addValues(int c, int b, int f) 
    {
        totalCaugh += c;
        totalBlood += b;
        totalFever += f;
    }

    public void setValues(int c, int b, int f) 
    {
        totalCaugh = c;
        totalBlood = b;
        totalFever = f;
    }
}