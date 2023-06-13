using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonClearUI : MonoBehaviour
{
    [SerializeField] ContainerCalculation container;
    private Button myButton;
    
    void Start()
    {
        myButton = GetComponent<Button>();
        myButton.interactable = false;

        container.OnContainerAdd += Container_OnContainerAdd;
        container.OnContainerEmpty += Container_OnCOntainerEmpty;
    }

    private void Container_OnCOntainerEmpty(object sender, EventArgs e)
    {
         myButton.interactable = false;
    }

    private void Container_OnContainerAdd(object sender, EventArgs e)
    {
         myButton.interactable = true;
    }
}
