using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;

public class IngredientContainer : MonoBehaviour
{
    public event Action OnAssumedValueChanged;

    [Header("For Coding")]
    [SerializeField] private GameObject ingredientPiece;
    [SerializeField] private TextMeshProUGUI nameField;

    [Header("For Individual Instance")]
    [SerializeField] IngredientSO ingredientSO;

    private GameObject ingredientInstace;
    private Vector2 mouseOffset;

    

    private bool canInteract;

    private string ingredientName = "noNameSet";

    private void Start() 
    {
        InitializeIngridientContainer();

        GameManager.Instance.OnGamePhaseChanged += GameManager_OnGamePhaseChanged;
    }

    private void OnMouseDown() 
    {
        if(canInteract){
            CreateIngredient();
        }
    }

    private void GameManager_OnGamePhaseChanged()
    {
        SetInteractionBasedOnGamePhase();
    }

    private void InitializeIngridientContainer()
    {
        ingredientName  = ingredientSO.getName();

        nameField.text = ingredientName;
    }

    private void SetInteractionBasedOnGamePhase()
    {
        if(GameManager.Instance.getGamePhase() == patientPhase.Score)
        {
            canInteract = false;
        }else{
            canInteract = true;
        }
    }

    private void CreateIngredient()
    {
        ingredientInstace = Instantiate(ingredientPiece, Camera.main.ScreenToWorldPoint(Input.mousePosition),Quaternion.identity);
        ingredientInstace.GetComponent<IngredientPiece>().setIngredientSO(ingredientSO);
        ingredientInstace.GetComponent<IngredientPiece>().setAssumedValues(GetAssumedValues());
    }

    // Set/Get
    public void SetIngredientSO(IngredientSO newIngredientSO)
    {
        ingredientSO = newIngredientSO;
        InitializeIngridientContainer();
        SetAssumedValues(ingredientSO.GetAssumedValues());
        SetLockValues(ingredientSO.GetLockValues());
    }
    
    public int[] GetAssumedValues()
    {
        return ingredientSO.GetAssumedValues();
    }

    public void SetAssumedValues(int[] values)
    {
        ingredientSO.SetAssumedValues(values);
        OnAssumedValueChanged?.Invoke();
    }

    public bool[] GetLockValues()
    {
        return ingredientSO.GetLockValues();
    }

    public void SetLockValues(bool[] values)
    {
        ingredientSO.SetLockValues(values);
        OnAssumedValueChanged?.Invoke();
    }

    public string GetIngredientName()
    {
        return ingredientName;
    }

    public IngredientSO GetIngredientSO()
    {
        return ingredientSO;
    }
}
