using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocationUI : MonoBehaviour
{

    private Location location;
    [SerializeField] private Button myButton;

    void Start()
    {
        GetComponent<Canvas>().enabled = false;
        
        location = GetComponentInParent<Location>();
        location.OnGotSelected += Location_OnGotSelected;
        location.OnGotDeselected += Location_OnGotDeselected;

        myButton.interactable = false;
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
