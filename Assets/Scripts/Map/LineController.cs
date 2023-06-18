using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineController : MonoBehaviour
{
    [SerializeField] private LocationsHandler locationsHandler;
    private LineRenderer lineRenderer;

    private void Awake() 
    {
        lineRenderer = GetComponentInChildren<LineRenderer>();
    }

    void Start()
    {
        List<Location> allLocations = locationsHandler.GetLocations();
        lineRenderer.positionCount = allLocations.Count;
        for (int i = 0; i < allLocations.Count; i++)
        {
            lineRenderer.SetPosition(i,allLocations[i].transform.position);  
        }
    }

}
