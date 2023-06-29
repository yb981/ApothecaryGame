using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class AudioClipsSO : ScriptableObject
{
    [SerializeField] public AudioClip hoverUI;
    [SerializeField] public AudioClip clickUI;
    [SerializeField] public AudioClip bubbling;
    [SerializeField] public AudioClip raven;
    [SerializeField] public AudioClip thunder;
    [SerializeField] public AudioClip cart;
    [SerializeField] public AudioClip dropItem;
}
