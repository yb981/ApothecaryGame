using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public enum MovementState
    {
        waiting,
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
    private MovementState movementState = MovementState.waiting;
    private Location currentLocation;
    private MoveToPoint moveToPoint;

    void Start()
    {
        locationsHandler.OnPressedStart += LocationsHandler_OnPressedStart;
        moveToPoint = GetComponent<MoveToPoint>();
        moveToPoint.OnDestinationReached += MoveToPoint_OnDestinationReached;
    }

    public void SetLocation(Location newLocation)
    {
        currentLocation = newLocation;
        transform.position = currentLocation.GetComponent<Transform>().position;
    }

    private void MoveToPoint_OnDestinationReached(object sender, MoveToPoint.OnDestinationReachedEventArgs e)
    {
        currentLocation = e.destionation;
        MapHandler.Instance.StartLevel(currentLocation.GetLevel());
        Debug.Log(this +" telling maphandler to start");
    }

    private void LocationsHandler_OnPressedStart(object sender, LocationsHandler.OnPressedStartEventArgs e)
    {
        movementState = MovementState.moving;
        OnMovementChanged?.Invoke(this, new OnMovementChangedEventArgs
        {
            destination = e.location.GetComponent<Transform>(),
            movementState = this.movementState
        });
    }

    private void OnDestroy() 
    {
        locationsHandler.OnPressedStart -= LocationsHandler_OnPressedStart;
    }
}
