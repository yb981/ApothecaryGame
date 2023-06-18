using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FeedbackTextHandler : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI goodTMP;
    [SerializeField] TextMeshProUGUI badTMP;

    GameManager gameManager;

    private void Start() 
    {
        GameManager.Instance.VillageHealthChanged += Instance_VillageHealthChanged;
    }

    private void Instance_VillageHealthChanged(object sender, GameManager.VillageHealthChangedEventArgs e)
    {
        if(e.villageHealthChange > 0)
        {
            PlayFeedbackTextGood();
        }
        else if(e.villageHealthChange < 0)
        {
            PlayFeedbackTextBad();
        }
    }

    private void PlayFeedbackTextGood()
    {
        goodTMP.gameObject.SetActive(false);
        goodTMP.gameObject.SetActive(true);
        goodTMP.GetComponent<Animator>().Play("Base Layer.VillageTMPFadeOut",0,0);
    }

    private void PlayFeedbackTextBad()
    {
        badTMP.gameObject.SetActive(false);
        badTMP.gameObject.SetActive(true);
        goodTMP.GetComponent<Animator>().Play("Base Layer.VillageTMPFadeOutBad",0,0);
    }

}
