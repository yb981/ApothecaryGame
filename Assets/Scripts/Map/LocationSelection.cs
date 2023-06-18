using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationSelection : MonoBehaviour
{
    private SpriteRenderer mySpriteRenderer;
    private Location location;
    
    private void Start() 
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        mySpriteRenderer.enabled = false;

        location = GetComponentInParent<Location>();
        location.OnGotSelected += Location_OnGotSelected;
        location.OnGotDeselected += Location_OnGotDeselected;
    }

    private void Location_OnGotSelected(object sender, EventArgs e)
    {
        mySpriteRenderer.enabled = true;
    }

    private void Location_OnGotDeselected(object sender, EventArgs e)
    {
        mySpriteRenderer.enabled = false;
    }


}
