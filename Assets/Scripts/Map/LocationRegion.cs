using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocationRegion : MonoBehaviour
{

    [SerializeField] private Button myStartButton;
    [SerializeField] private MapSettingsSO mapSettingsSO;
    private RegionHandler regionHandler;

    private void Start() 
    {
        regionHandler = GetComponentInParent<RegionHandler>();
        myStartButton.onClick.AddListener( () => {InformHandlerButtonIsClicked(this);});
    }

    private void InformHandlerButtonIsClicked(LocationRegion locationRegion)
    {
        regionHandler.PlayerPressedStart();
    }

}
