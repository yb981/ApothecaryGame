using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LocationUI : MonoBehaviour
{
    private Location location;
    [SerializeField] private Button myButton;
    [SerializeField] private TextMeshProUGUI levelTMP;
    [SerializeField] private TextMeshProUGUI villagersTMP;
    private string villagerText = "Villager: ";

    void Start()
    {
        GetComponent<Canvas>().enabled = false;
        
        location = GetComponentInParent<Location>();
        location.OnGotSelected += Location_OnGotSelected;
        location.OnGotDeselected += Location_OnGotDeselected;
        location.LocationNumberChanged += Location_LocationNumberChanged;

        myButton.interactable = false;
        
        villagersTMP.text = villagerText + location.GetVillagerNumber();
    }

    private void Location_LocationNumberChanged(object sender, EventArgs e)
    {
        levelTMP.text = "Level "+ location.GetLocationsNumber();
    }

    private void Location_OnGotSelected(object sender, EventArgs e)
    {
        GetComponent<Canvas>().enabled = true;

        // only enable button if locatoin in reach
        if(location.IsReachable())
        {
            myButton.interactable = true;
        }else{
            myButton.interactable = false;
        }
    }

    private void Location_OnGotDeselected(object sender, EventArgs e)
    {
        GetComponent<Canvas>().enabled = false;
        myButton.interactable = false;
    }
}
