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
    private Location nextLocation;
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
        transform.position = currentLocation.GetWagonPoint().position;
    }

    private void MoveToPoint_OnDestinationReached()
    {
        UpdatePositionAndMovementOnArrival();
        MapHandler.Instance.StartLevel(currentLocation.GetLevel());
    }

    private void LocationsHandler_OnPressedStart(object sender, LocationsHandler.OnPressedStartEventArgs e)
    {
        movementState = MovementState.moving;
        nextLocation = e.location;
        OnMovementChanged?.Invoke(this, new OnMovementChangedEventArgs
        {
            destination = nextLocation.GetWagonPoint(),
            movementState = this.movementState
        });
    }

    private void UpdatePositionAndMovementOnArrival()
    {
        currentLocation = nextLocation;
        nextLocation = null;
        movementState = MovementState.stopped;
    }

    private void OnDestroy() 
    {
        locationsHandler.OnPressedStart -= LocationsHandler_OnPressedStart;
    }
}
