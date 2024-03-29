using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapHandler : MonoBehaviour
{
    
    public static MapHandler Instance {get; private set;}

    [SerializeField] LocationsHandler locationsHandler;
    [SerializeField] Player player;
    [SerializeField] CreateIngredientSOs createIngredientSOs;
    [SerializeField] GameObject AllChildren;
    private int currentLevelCount;
    //private List<IngredientSO> currentIngredientsEndlessRun;
    private List<IngredientSO> currentEndlessRunIngredients;

    private void Awake() 
    {
        if(Instance == null) Instance = this; else Debug.LogError("2 MapHandler were created");
    }

    private void Start() 
    {
        RegionHandler.Instance.OnGoingToMap += MapHandler_OnGoingToMap;
        
        // Set initial state
        if(RegionHandler.Instance.GetInRegionState())
        {
            Initialize();
        }else{
            AllChildren.SetActive(false);
        }
    }

    private void MapHandler_OnGoingToMap(object sender, EventArgs e)
    {
        
        Initialize();
    }

    private void Initialize()
    {
        AllChildren.SetActive(true);
        currentLevelCount = RunHandler.Instance.GetLevelCount();
        locationsHandler.SetLocationsReach(currentLevelCount);
        if(currentLevelCount == 0){
            player.SetLocation(locationsHandler.GetLocations()[0]);
        }else{
            player.SetLocation(locationsHandler.GetLocations()[currentLevelCount-1]);
        }
        
        RunHandler.Instance.SetLevelAmount(locationsHandler.GetNumberOfLocations());
    }

    public void StartLevel(LevelSettingsSO level)
    {
        // Check if Ingredients exist
        if(currentEndlessRunIngredients == null){
           currentEndlessRunIngredients = RunHandler.Instance.GetCurrentEndlessIngredients();
        }


        if(currentEndlessRunIngredients == null){
            // Create ingredients
            currentEndlessRunIngredients = createIngredientSOs.CreateRandomIngredientSOs();

            RunHandler.Instance.SetCurrentEndlessRunIngredients(currentEndlessRunIngredients);
        }
        RunHandler.Instance.LoadLevel(level);
        Debug.Log(this +" telling to start");
    }
}
