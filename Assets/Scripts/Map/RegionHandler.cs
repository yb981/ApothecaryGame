using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegionHandler : MonoBehaviour
{

    public static RegionHandler Instance { get; private set; }

    public event EventHandler OnGoingToMap;

    [SerializeField] private List<LocationRegion> locationsList;
    [SerializeField] private GameObject villageMap;
    private int currentRegion;
    private bool inRegion = false;

    private void Awake() 
    {
        Instance = this;    
    }

    private void Start() 
    {
        if(RunHandler.Instance.GetRegionActive())
        {
            inRegion = true;
            OnGoingToMap?.Invoke(this, EventArgs.Empty);
        }
    }

    public void PlayerPressedStart()
    {
        // Disable Region Map
        OnGoingToMap?.Invoke(this, EventArgs.Empty);
        RunHandler.Instance.SetRegionActive(true);

        // Load Map of Location
        // Initialize Map of Location


    }

    private void InsitantiateNewMap()
    {

    }

    public bool GetInRegionState()
    {
        return inRegion;
    }
}
