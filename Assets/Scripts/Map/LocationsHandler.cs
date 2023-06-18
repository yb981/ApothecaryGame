using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationsHandler : MonoBehaviour
{

    public event EventHandler<OnPressedStartEventArgs> OnPressedStart;
    public class OnPressedStartEventArgs : EventArgs
    {
        public Location location;
    }

    [SerializeField] List<Location> locationsList;
    private Location selectedLocation;

    public void SetNewActiveLocation(Location newLocation)
    {
        foreach (Location location in locationsList)
        {
            if(location == newLocation)
            {
                location.SetSelected();
                selectedLocation = location;
            }
            else
            {
                location.SetDeselected();
            }
        }
    }

    public void PlayerPressedStart()
    {
        OnPressedStart?.Invoke(this, new OnPressedStartEventArgs{
            location = selectedLocation
        });
    }

    // Get/Set
    public Location GetSelectedLocation()
    {
        return selectedLocation;
    }

    public List<Location> GetLocations()
    {
        return locationsList;
    }
}
