using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuButtonLoad : MonoBehaviour 
{
    private Button myButton;

    [SerializeField] private string nextScene = GameConstants.SCENE_CREDITS;

    private void Awake() 
    {
        myButton = GetComponent<Button>();
    }

    void Start()
    {
        myButton.onClick.AddListener(() => {Loader.NextSceneWithoutLoadingScreen(GameConstants.SCENE_CREDITS);});
    }
}
