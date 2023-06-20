using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapHandler : MonoBehaviour
{
    
    public static MapHandler Instance {get; private set;}

    [SerializeField] LocationsHandler locationsHandler;
    [SerializeField] Player player;
    [SerializeField] CreateIngredientSOs createIngredientSOs;
    private int currentLevelCount;
    //private List<IngredientSO> currentIngredientsEndlessRun;
    private List<CompleteIngredient> currentEndlessRunCompleteIngredients;

    private void Awake() 
    {
        if(Instance == null) Instance = this; else Debug.LogError("2 MapHandler were created");
    }

    private void Start() 
    {
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
        if(currentEndlessRunCompleteIngredients == null){
           currentEndlessRunCompleteIngredients = RunHandler.Instance.GetCurrentEndlessRunCompleteIngredients();
        }


        if(currentEndlessRunCompleteIngredients == null){
            // Create ingredients
            currentEndlessRunCompleteIngredients = new List<CompleteIngredient>();
            List<IngredientSO> newIng = createIngredientSOs.CreateRandomIngredientSOs();
            for (int i = 0; i < newIng.Count; i++)
            {
                currentEndlessRunCompleteIngredients.Add(new CompleteIngredient(newIng[i]));
            }
             
            RunHandler.Instance.SetCurrentEndlessRunCompleteIngredients(currentEndlessRunCompleteIngredients);
        }
        RunHandler.Instance.LoadLevel(level);
        Debug.Log(this +" telling to start");
    }
}
