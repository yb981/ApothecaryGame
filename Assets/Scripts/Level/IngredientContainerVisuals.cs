using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientContainerVisuals : MonoBehaviour
{

    [SerializeField] ContainerHover containerHover;
    [SerializeField] GameObject highlight;

    void Start()
    {
        containerHover.MouseHovering += Visuals_MouseHovering;
        containerHover.MouseLeaving += Visuals_MouseLeaving;
    }

    private void Visuals_MouseLeaving(object sender, EventArgs e)
    {
        highlight.SetActive(false);
    }

    private void Visuals_MouseHovering(object sender, EventArgs e)
    {
        highlight.SetActive(true);
    }
}
