using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public enum MovementState
    {
        moving,
        stopped
    }

    public event EventHandler<OnMovementChangedEventArgs> OnMovementChanged;
    public class OnMovementChangedEventArgs : EventArgs 
    {
        public Transform destination;
        public MovementState movementState;
    }


    [SerializeField] private LocationsHandler locationsHandler;

    void Start()
    {
        locationsHandler.OnPressedStart += LocationsHandler_OnPressedStart;
    }

    private void LocationsHandler_OnPressedStart(object sender, LocationsHandler.OnPressedStartEventArgs e)
    {
        OnMovementChanged?.Invoke(this, new OnMovementChangedEventArgs
        {
            destination = e.location.GetComponent<Transform>(),
            movementState = MovementState.moving
        });
    }
}
