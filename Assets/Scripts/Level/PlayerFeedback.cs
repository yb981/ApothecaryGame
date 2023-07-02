using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class PlayerFeedback : MonoBehaviour
{

    public event EventHandler OnStartFeedback;

    [Header("Coding")]
    [SerializeField] private ContainerCalculation container;
    private TextMeshProUGUI playerFeedbackTMP;
    private GameManager manager;
    private GameObject myBackground;
    private FeedbackLogic myFeedbackLogic;
    private FeedbackTextGenerator myTextGenerator;
    private HandleAdminSliders clientSliders;
    private GameObject[] ingredients;
    private IngredientSliders[] ingredientSliders;
    private FeedbackIngredient[] feedbackIngredients;

    //[SerializeField] int totalMissValue = 4;
    //[SerializeField] int miss = 2;

    void Start()
    {
        // Get Components
        playerFeedbackTMP   = GetComponentInChildren<TextMeshProUGUI>();
        myBackground        = GameObject.Find("FeedbackBackground");
        clientSliders       = GetComponentInChildren<HandleAdminSliders>();
        manager             = GameManager.Instance;
        ingredients         = GameObject.FindGameObjectsWithTag("Ingredient");
        ingredientSliders   = GetComponentsInChildren<IngredientSliders>();
        feedbackIngredients = GetComponentsInChildren<FeedbackIngredient>();

        // Create internal Components
        myFeedbackLogic     = new FeedbackLogic();
        myTextGenerator     = new FeedbackTextGenerator();

        // Start Functions
        DisplayFeedback(false);
    }

    // public Methods
    public void StartFeedback(int[] playerInput, int[] clientValue)
    {
        DisplayFeedback(true);

        // Invoke Feedback Event
        OnStartFeedback?.Invoke(this, EventArgs.Empty);

        // Update Client Slider Values
        clientSliders.SetSliderValues(clientValue);

        // Create Output Text
        GenerateOutputText(playerInput, clientValue);

        // Set the correct starting Values
        UpdateIngredientSliders();
    }

    public void DisplayFeedback(bool state)
    {
        myBackground.SetActive(state);
    }

    public void PlayerWantsToContinue()
    {
        taskCompleted();
    }

    // private Methods
    private void UpdateIngredientSliders()
    {
        string[] ingredientNames;
        ingredientNames = GetIngredientNamesFromSOList();

        /* // Check for Duplicates
        if(FindDuplicates(ingredientNames)){
            // Order Duplicates
            ingredientNames = OrderDuplicates(ingredientNames);
        } */
        
        List<IngredientSO> ingList = container.GetLatestContainerIngredients();
        for(int i = 0; i < ingList.Count ; i++)
        {
            feedbackIngredients[i].SetIngredientSO(ingList[i]);
        }

       /*  // Handle Ingredient Sliders
        for (int i = 0; i < ingredientSliders.Length; i++)
        {
            ingredientSliders[i].SetIngredientName(ingredientNames[i]);

            for (int j = 0; j < ingredients.Length; j++)
            {
                if(ingredients[j].GetComponentInChildren<TextMeshProUGUI>().text == ingredientNames[i])
                {
                    ingredientSliders[i].SetSliderValues(ingredients[j].GetComponent<IngredientContainer>().GetAssumedValues());
                    ingredientSliders[i].SetLockValues(ingredients[j].GetComponent<IngredientContainer>().GetLockValues());


                }
            }
        } */
    }

    private void GenerateOutputText(int[] playerInput, int[] clientValue)
    {
        string outputText = "";
        
        outputText += myTextGenerator.GeneratePlayerFeedbackTextOutput(myFeedbackLogic.CalculateFeedback(playerInput,clientValue), GetIngredientNamesFromSOList());

        playerFeedbackTMP.text = outputText;
    }

    // have to inform when phase is over
    private void taskCompleted()
    {
        if(GameManager.Instance.getGamePhase() == patientPhase.Score){
            DisplayFeedback(false);
            manager.informPhaseCompleted();
        } 
    }

    private string[] GetIngredientNamesFromSOList()
    {
        string[] ingNames;

        List<IngredientSO> ingList = container.GetLatestContainerIngredients();
        ingNames = new string[ingList.Count];

        for (int i = 0; i < ingList.Count; i++)
        {
            ingNames[i] = ingList[i].getName();
        }

        return ingNames;
    }

    /* public string[] GetInsertedIngredientNames()
    {
        return GetIngredientNamesFromSOList();
    }

     private bool FindDuplicates(IngredientSO[] duplicates)
    {
        for (int i = 0; i < duplicates.Length; i++)
        {
            for (int j = 0; j < duplicates.Length; j++)
            {
                if(j==i) continue;
                if(duplicates[j].getName() == duplicates[i].getName()) return true;
            }
        }
        return false;
    }

    // Helper
    private string[] OrderDuplicates(string[] duplicates)
    {
        string[] newOrder = new string[duplicates.Length];
        int swapPos = 0;
        for (int i = 0; i < duplicates.Length; i++)
        {
            swapPos++;
            if(swapPos >= duplicates.Length) break;
            for (int j = i; j < duplicates.Length; j++)
            {
                if(i == j) continue;
                if(duplicates[i] == duplicates[j])
                {
                    // swap values
                    string tmp = "";
                    tmp = duplicates[swapPos];
                    duplicates[swapPos] = duplicates[j];
                    duplicates[j] = tmp;
                    swapPos++;
                    j++;
                }
            }
        }

        return duplicates;
    } */
}
