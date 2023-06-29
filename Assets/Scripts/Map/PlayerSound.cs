using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    [SerializeField] AudioClipsSO audioClipsSO;
    private AudioSource audioSource;
    private Player player;

    private void Awake() 
    {
        audioSource = GetComponent<AudioSource>();
        player = GetComponentInParent<Player>();
        
    }

    private void Start() 
    {
        audioSource.clip = audioClipsSO.cart;
        player.OnMovementChanged += Player_OnMovementChanged;
        
    }

    private void Player_OnMovementChanged(object sender, Player.OnMovementChangedEventArgs e)
    {
        if(e.movementState == Player.MovementState.moving)
        {
            audioSource.Play();
        }else{
            if(audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }
    }
}
