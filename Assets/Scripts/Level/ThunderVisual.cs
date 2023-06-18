using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ThunderVisual : MonoBehaviour
{
    private LightHandler lightHandler;
    private Light2D myLight;
    private Animator myAnimator;


    void Awake()
    {
        myLight = GetComponent<Light2D>();
        myAnimator = GetComponent<Animator>();
        myLight.intensity = 0;
    }

    private void Start() 
    {
        lightHandler = GetComponentInParent<LightHandler>();
        lightHandler.ThunderTriggered += Thunder_ThunderTriggered;
    }

    private void Thunder_ThunderTriggered(object sender, EventArgs e)
    {
        myAnimator.SetTrigger("isLightning");
    }

    public void DisableLight()
    {
        myLight.intensity = 0;
    }
}
