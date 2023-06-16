using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FeedbackClient : MonoBehaviour
{
    [SerializeField] PlayerFeedback playerFeedback;
    [SerializeField] Patient patient;
    [SerializeField] Image imageHead;
    [SerializeField] Image imageBody;

    private void Start() 
    {
        playerFeedback.OnStartFeedback += PlayerFeedback_OnStartFeedback;
    }

    private void PlayerFeedback_OnStartFeedback(object sender, EventArgs e)
    {
        PatientBodySO patientBodySO = patient.GetPatientSprite();
        imageHead.sprite = patientBodySO.GetHead();
        imageBody.sprite = patientBodySO.GetBody();
    }
}
