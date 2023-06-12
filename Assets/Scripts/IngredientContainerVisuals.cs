using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientContainerVisuals : MonoBehaviour
{

    [SerializeField] IngredientContainer ingredientContainer;
    [SerializeField] GameObject highlight;


    // Start is called before the first frame update
    void Start()
    {
        ingredientContainer.MouseHovering += Visuals_MouseHovering;
        ingredientContainer.MouseLeaving += Visuals_MouseLeaving;
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
