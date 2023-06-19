using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapHandler : MonoBehaviour
{
    
    public static MapHandler Instance {get; private set;}

    [SerializeField] LocationsHandler locationsHandler;
    [SerializeField] Player player;
    private int currentLevelCount;

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
        RunHandler.Instance.LoadLevel(level);
        Debug.Log(this +" telling to start");
    }
}
