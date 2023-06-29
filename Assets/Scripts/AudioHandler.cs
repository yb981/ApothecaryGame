using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioHandler : MonoBehaviour
{
    public static AudioHandler Instance { get; private set; }

    [SerializeField] private AudioClipsSO audioClipsSO;

    [SerializeField] private AudioSource mainBackgroundMusic;
    [SerializeField] private AudioSource rainBackgroundSound;
    private float volume = 0.2f;
    
    private Player player;
    private bool cartSoundPlaying;
    private AudioClip cart;

    private void Awake()
    {
        HandleSingelton();
        SceneManager.sceneLoaded += AudioHandler_OnSceneLoaded;

        SubscribeToEvents(); 
    }

    private void AudioHandler_OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        GetReferences();

        if (string.Equals(SceneManager.GetActiveScene().name, GameConstants.SCENE_WAGON))
        {
            OnlyPlayRainBackground();
        }
        else
        {
            OnlyPlayMainBackgroundMusic();
        }
    }

    private void GetReferences()
    {
        player = FindObjectOfType<Player>();
    }

    private void OnlyPlayRainBackground()
    {
        rainBackgroundSound.Play();
        mainBackgroundMusic.Stop();
    }

    private void OnlyPlayMainBackgroundMusic()
    {
        if (rainBackgroundSound.isPlaying)
        {
            rainBackgroundSound.Stop();
        }
        if (mainBackgroundMusic.isPlaying == false)
        {
            mainBackgroundMusic.Play();
        }
    }

    private void ButtonSoundUI_OnClickButton()
    {
        PlaySoundClip(audioClipsSO.clickUI, Camera.main.transform.position, volume);
    }

    private void ButtonSoundUI_OnHoverButton()
    {
        PlaySoundClip(audioClipsSO.hoverUI, Camera.main.transform.position, volume);
    }

    public void PlaySoundClipAutoVolume(AudioClip clip, Vector3 position)
    {
        PlaySoundClip(clip, position, volume);
    }

    private void PlaySoundClip(AudioClip clip, Vector3 position, float volume)
    {
        AudioSource.PlayClipAtPoint(clip, position, volume);
    }

    private void HandleSingelton()
    {
        if (Instance != null)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void SubscribeToEvents()
    {
        HoverSoundUI.OnHoverButton += ButtonSoundUI_OnHoverButton;
        ButtonSoundUI.OnClickButton += ButtonSoundUI_OnClickButton;
    }
}
