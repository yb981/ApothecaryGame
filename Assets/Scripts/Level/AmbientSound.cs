using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientSound : MonoBehaviour
{
    [SerializeField] AudioClipsSO audioClipsSO;
    private AudioSource audioSource;
    private float timer;
    private float nextSoundTime;
    private int nextFollowUpCaws;
    private float timeForCaw = 1.2f;
    private float cawTimer = 0f;
    private int cawCount = 0;
    private int caws = 0;
    [SerializeField] private float minPauseForNextSound = 10f;
    [SerializeField] private float maxPauseForNextSound = 30f;
    [SerializeField] private int maxFollowUpCaws = 3;

    private void Awake() 
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start() 
    {
        audioSource.clip = audioClipsSO.raven;    
        nextSoundTime = Random.Range(minPauseForNextSound,maxPauseForNextSound);
        caws = Random.Range(1,maxFollowUpCaws);
    }

    private void Update() 
    {
        timer += Time.deltaTime;
        if(timer > nextSoundTime)
        {
            PlayRavenCaws();
        }
    }

    private void PlayRavenCaws()
    {
        if(cawCount < caws ){
            if(cawTimer >= timeForCaw)
            {
                audioSource.Play();
                cawCount++;
                cawTimer = 0f;
            }
            cawTimer += Time.deltaTime;
        }else{
            ResetTimerAndRandomize();
        }
    }

    private void ResetTimerAndRandomize()
    {
        timer = 0;
        nextSoundTime = Random.Range(minPauseForNextSound,maxPauseForNextSound);
        caws = Random.Range(1,maxFollowUpCaws);
        cawCount = 0;
    }
}
