using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocationRegion : MonoBehaviour
{

    [SerializeField] private Button myStartButton;
    [SerializeField] private MapSettingsSO mapSettingsSO;
    [SerializeField] private Canvas canvas;
    private RegionHandler regionHandler;

    private void Start() 
    {
        regionHandler = GetComponentInParent<RegionHandler>();
        regionHandler.OnGoingToMap += RegionHandler_OnGoingToMap;
        myStartButton.onClick.AddListener( () => {InformHandlerButtonIsClicked(this);});
    }

    private void RegionHandler_OnGoingToMap(object sender, EventArgs e)
    {
        Hide();
    }

    private void InformHandlerButtonIsClicked(LocationRegion locationRegion)
    {
        regionHandler.PlayerPressedStart();
    }

    private void Hide()
    {
        canvas.enabled = false;
    }

    private void Show()
    {
        canvas.enabled = true;
    }
}
