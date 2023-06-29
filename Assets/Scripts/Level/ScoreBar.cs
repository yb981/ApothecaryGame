using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreBar : MonoBehaviour
{
    [SerializeField] GameObject fullScoreBar;
    [SerializeField] Slider score;
    [SerializeField] Image scoreFill;
    [SerializeField] TextMeshProUGUI scoreNumber;
    [SerializeField] float fillSpeed = 4f;
    [SerializeField] AudioClipsSO audioClipsSO;
    private AudioSource audioSource;
    private Animator myAnimator;
    private bool soundIsPlaying = false;
    private float scoreValue = 0;
    private bool displayScore = false;
    private float currentValue = 0;
    private bool animationComplete = false;

    void Awake()
    {
        myAnimator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        audioSource.clip = audioClipsSO.bubbling;
        fullScoreBar.gameObject.SetActive(false);
    }

    void Update()
    {
        if (displayScore && !animationComplete)
        {
            StartFillingSoundEffect();
            currentValue += fillSpeed * Time.deltaTime;

            scoreFill.fillAmount = (float)currentValue / 1;
            if (scoreFill.fillAmount >= scoreValue)
            {
                animationComplete = true;
                SetScoreTextTo(scoreFill.fillAmount);
                GameManager.Instance.informPhaseCompleted();
                StopFillingSoundEffect();
            }
            else
            {
                SetScoreTextTo(currentValue);
            }
        }
    }

    public void toggleScoreDisplay(bool state)
    {
        displayScore = state;

        fullScoreBar.gameObject.SetActive(state);
        scoreNumber.gameObject.SetActive(state);
    }

    public bool isScoreBarDisplayed()
    {
        return displayScore;
    }

    private void ResetSlider()
    {
        animationComplete = false;
        currentValue = 0f;
    }

    private void SetScoreTextTo(float value)
    {
        float wholeNumberPercent = value * 100;
        scoreNumber.text = wholeNumberPercent.ToString("0.0") + "%";
    }

    private void StartFillingSoundEffect()
    {
        if(soundIsPlaying) return;

        PlaySoundEffect();
        soundIsPlaying = true;
    }

    private void PlaySoundEffect()
    {
        audioSource.Play();
    }

    private void StopFillingSoundEffect()
    {
        soundIsPlaying = false;
        audioSource.Stop();
    }

    public void setScore(float newScore)
    {
        ResetSlider();

        scoreValue = newScore;

        // Start Animation
        if (displayScore) animationComplete = false;
    }
}
