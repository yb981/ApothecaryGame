using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationInReach : MonoBehaviour
{

    [SerializeField] private SpriteRenderer inReachSpriteRenderer;
    [SerializeField] private SpriteRenderer outOfReachSpriteRenderer;
    private Location location;

    void Start()
    {
        location = GetComponentInParent<Location>();
        location.OnReachChanged += Location_OnReachChanged;
        SetSpritesForReach();
    }

    private void Location_OnReachChanged(object sender, EventArgs e)
    {
        SetSpritesForReach();
    }

    private void SetSpritesForReach()
    {
        if(location.IsReachable())
        {
            inReachSpriteRenderer.enabled = true;
            outOfReachSpriteRenderer.enabled = false;
        }
        else
        {
            inReachSpriteRenderer.enabled = false;
            outOfReachSpriteRenderer.enabled = true;
        }
    }
}
