using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UITracker : MonoBehaviour
{
    [SerializeField] Patient patientHandler;
    private TextMeshProUGUI[] myTMPs;
    private Slider villageHealthSlider;

    void Awake()
    {
        // Get Components
        myTMPs                  = GetComponentsInChildren<TextMeshProUGUI>();
        villageHealthSlider     = GetComponentInChildren<Slider>();
    }

    void Start()
    {
        myTMPs[0].text = CreateClientText();
    }

    public void NewClient()
    {
        myTMPs[0].text = CreateClientText();
    }

    public void UpdateVillageBar(int value)
    {
        villageHealthSlider.value = value;
    }

    private string CreateClientText()
    {
        int visitedClients = patientHandler.GetCurrentPatientNumber();
        int maxClients = patientHandler.GetTotalNumberOfPatients();
        string outputString = " / ";
        return visitedClients + outputString + maxClients;
    }

    // Set and Get
    public void SetVillageMaxHealthBar(int value)
    {
        villageHealthSlider.maxValue = value;
    }    
}
