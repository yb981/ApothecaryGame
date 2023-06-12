using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightHandler : MonoBehaviour
{
    [Header("Adjustment")]
    [SerializeField] private float minThunderTimer = 25f;
    [SerializeField] private float maxThunderTimer = 60f;
    private float timer = 0f;
    private float triggerTime = 30f;

    public event EventHandler ThunderTriggered;

    private void Start()
    {
        triggerTime = RandomThunder();
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if(timer >= triggerTime)
        {
            ThunderTriggered?.Invoke(this, EventArgs.Empty);
            timer = 0f;
            triggerTime = RandomThunder();
        }
    }

    private float RandomThunder()
    {
        return UnityEngine.Random.Range(minThunderTimer,maxThunderTimer);
    }
}
