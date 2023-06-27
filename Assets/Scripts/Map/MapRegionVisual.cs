using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapRegionVisual : MonoBehaviour
{
    
    private RegionHandler regionHandler;
    [SerializeField] private SpriteRenderer mapSprite;

    void Start()
    {
        regionHandler = GetComponentInParent<RegionHandler>();
        regionHandler.OnGoingToMap += RegionHandler_OnGoingToMap;

        // Set initial State
        if(regionHandler.GetInRegionState())
        {
            mapSprite.enabled = false;
        }
    }

    private void RegionHandler_OnGoingToMap(object sender, EventArgs e)
    {
        mapSprite.enabled = false;
    }
}
