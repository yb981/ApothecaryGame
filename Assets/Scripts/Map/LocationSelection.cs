using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationSelection : MonoBehaviour
{

    [SerializeField] private SpriteRenderer selectionSpriteRenderer;
    private Location location;
    
    private void Start() 
    {
        selectionSpriteRenderer = GetComponent<SpriteRenderer>();
        selectionSpriteRenderer.enabled = false;

        location = GetComponentInParent<Location>();
        location.OnGotSelected += Location_OnGotSelected;
        location.OnGotDeselected += Location_OnGotDeselected;
    }

    private void Location_OnGotSelected(object sender, EventArgs e)
    {
        selectionSpriteRenderer.enabled = true;
    }

    private void Location_OnGotDeselected(object sender, EventArgs e)
    {
        selectionSpriteRenderer.enabled = false;
    }



}
