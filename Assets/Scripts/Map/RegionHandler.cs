using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegionHandler : MonoBehaviour
{

    [SerializeField] List<LocationRegion> locationsList;
    [SerializeField] GameObject villageMap;

    [Header("All Visuals")]
    [SerializeField] Canvas regionCanvas;
    [SerializeField] GameObject mapSprite;

    public void PlayerPressedStart()
    {
        // Disable Region Map
        HideRegion();

        // Load Map of Location
        // Initialize Map of Location


    }

    private void InsitantiateNewMap()
    {

    }

    private void HideRegion()
    {
        regionCanvas.enabled = false;
        mapSprite.SetActive(false);
    }

    private void ShowRegion()
    {
        regionCanvas.enabled = true;
        mapSprite.SetActive(true);
    }
}
