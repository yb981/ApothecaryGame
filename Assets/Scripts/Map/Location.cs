using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Location : MonoBehaviour
{

    public event EventHandler OnGotSelected;
    public event EventHandler OnGotDeselected;
    public event EventHandler OnReachChanged;

    [SerializeField] private LevelSettingsSO level;

    private LocationsHandler locationsHandler;
    private BoxCollider2D myCollider;
    private bool selected = false;
    [SerializeField] private bool reachable = false;

    void Start()
    {
        myCollider = GetComponent<BoxCollider2D>();
        locationsHandler = GetComponentInParent<LocationsHandler>();
    }

    private void OnMouseDown() 
    {
        locationsHandler.SetNewActiveLocation(this);
    }

    public void PlayerPressedStart()
    {
        Debug.Log(this +": pressed Start");
        locationsHandler.PlayerPressedStart();    
    }

    // Get/Set
    public void SetSelected()
    {
        OnGotSelected?.Invoke(this, EventArgs.Empty);
        selected = true;
    }

    public void SetDeselected()
    {
        if(selected == true) OnGotDeselected?.Invoke(this,EventArgs.Empty);
        selected = false;
    }

    public void SetReach(bool state)
    {
        reachable = state;
        OnReachChanged?.Invoke(this, EventArgs.Empty);
    }

    public bool IsReachable()
    {
        return reachable;
    }

    public LevelSettingsSO GetLevel()
    {
        return level;
    }
}
