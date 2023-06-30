using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ContainerHover : MonoBehaviour , IPointerEnterHandler, IPointerExitHandler
{
    public event EventHandler MouseHovering;
    public event EventHandler MouseLeaving;

    private bool canHover = true;

    private void Start() 
    {
        GameManager.Instance.OnGamePhaseChanged += GameManager_OnGamePhaseChanged;    
    }

    private void GameManager_OnGamePhaseChanged()
    {
        CanHoverBasedOnGamePhase();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(canHover) MouseHovering?.Invoke(this, EventArgs.Empty);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        MouseLeaving(this, EventArgs.Empty);
    }

    private void CanHoverBasedOnGamePhase()
    {
        if(GameManager.Instance.getGamePhase() == patientPhase.Score)
        {
            canHover = false;
        }else{
            canHover = true;
        }
    }
}
