using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class PatientText : MonoBehaviour
{
    [Header("PatientUI")]
    [SerializeField] TextMeshProUGUI myText;
    [SerializeField] Image speechBubble;
    [SerializeField] Patient patient;
    [SerializeField] GameObject patientStats;
    
    [Header("SicknessBarUI")]
    [SerializeField] Slider slider1;
    [SerializeField] Slider slider2;
    [SerializeField] Slider slider3;
    [SerializeField] TextMeshProUGUI TMPv1;
    [SerializeField] TextMeshProUGUI TMPv2;
    [SerializeField] TextMeshProUGUI TMPv3;

    void Start()
    {
        patient.OnPatientTalkPhaseChanged += Patient_OnPatientTalkPhaseChanged;   
        HideSpeechText();
        HideStats();
    }

    private void Patient_OnPatientTalkPhaseChanged(object sender, Patient.OnPatientTalkPhaseChangedEventArgs e)
    {
        switch(e.talkPhase)
        {
            case PatientTalkPhase.Talk:
                DisplaySpeechText(e.currentPatient);
                HideStats();
                break;
            case PatientTalkPhase.Stats:
                HideSpeechText();
                DisplayStats(e.currentPatient);
                break;
            case PatientTalkPhase.Nothing:
                HideSpeechText();
                HideStats();
                break;
        }
    }


    private void DisplaySpeechText(PatientSO currentPatient)
    {
        myText.enabled = true;
        myText.text = currentPatient.getNPCname()+ " : " + currentPatient.getRequestText();
        speechBubble.gameObject.SetActive(true);
    }

    private void HideSpeechText()
    {
        myText.enabled = false;
        speechBubble.gameObject.SetActive(false);
    }
    
    private void DisplayStats(PatientSO currentPatient)
    {
        patientStats.SetActive(true);
        SetSliderValues(currentPatient);
    }
    
    private void HideStats()
    {
        patientStats.SetActive(false);
    }

    private void SetSliderValues(PatientSO currentPatient)
    {
        int[] values = currentPatient.getSicknessValues();
        slider1.value = values[0];
        slider2.value = values[1];
        slider3.value = values[2];
        TMPv1.text = values[0].ToString();
        TMPv2.text = values[1].ToString();
        TMPv3.text = values[2].ToString();
    }
}
