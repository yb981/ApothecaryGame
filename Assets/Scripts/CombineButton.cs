using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombineButton : MonoBehaviour
{
    [SerializeField] ContainerCalculation container;
    private Button myButton;
    private bool canInteract = false;

    private void Start() 
    {
        myButton = GetComponent<Button>();
        myButton.interactable = false;

        container.OnContainerFull += Container_OnContainerFull;
        container.OnContainerEmpty += Container_OnCOntainerEmpty;
    }

    private void Container_OnCOntainerEmpty(object sender, EventArgs e)
    {
        if(myButton.interactable) myButton.interactable = false;
    }

    private void Container_OnContainerFull(object sender, EventArgs e)
    {
        myButton.interactable = true;
    }
}
