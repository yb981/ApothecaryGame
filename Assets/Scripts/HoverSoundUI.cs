using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoverSoundUI : MonoBehaviour, IPointerEnterHandler
{
    public static event Action OnHoverButton;

    public void OnPointerEnter(PointerEventData eventData)
    {
        OnHoverButton?.Invoke();
    }
}
