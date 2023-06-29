using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderSound : MonoBehaviour
{
    
    [SerializeField] private LightHandler lightHandler;
    [SerializeField] private AudioClipsSO audioClipsSO;

    void Start()
    {
        lightHandler.ThunderTriggered += LightHandler_ThunderTriggered;
    }

    private void LightHandler_ThunderTriggered(object sender, EventArgs e)
    {
        StartCoroutine(DelayedThunder());
    }

    private IEnumerator DelayedThunder()
    {
        yield return new WaitForSeconds(1f);
        AudioHandler.Instance.PlaySoundClipAutoVolume(audioClipsSO.thunder,Camera.main.transform.position);
    }
}
