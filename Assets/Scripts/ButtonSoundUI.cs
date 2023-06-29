using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSoundUI : HoverSoundUI
{
    private Button button;

    public static event Action OnClickButton;

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(TriggerClickSound);
    }

    private void TriggerClickSound()
    {
        OnClickButton.Invoke();
    }
}
