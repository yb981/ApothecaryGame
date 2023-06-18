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
    Animator myAnimator;
    float scoreValue = 0;
    bool displayScore = false;
    float currentValue = 0;
    bool animationComplete = false;

    void Awake()
    {
        myAnimator = GetComponent<Animator>();
    }

    private void Start() 
    {
        //score.enabled = false;
        fullScoreBar.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(displayScore && !animationComplete)
        {
            currentValue += fillSpeed * Time.deltaTime;

            scoreFill.fillAmount = (float) currentValue/1;
            //score.value = currentValue;
            if(scoreFill.fillAmount >= scoreValue)
            {
                animationComplete = true;
                SetScoreTextTo(scoreFill.fillAmount);
                GameManager.Instance.informPhaseCompleted();
            }else{
                SetScoreTextTo(currentValue);
            }
        }

    }

    // Set and Get

    public void setScore(float newScore)
    {
        ResetSlider();

        scoreValue = newScore;
        
        // Start Animation
        if(displayScore) animationComplete = false;        
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

    // methods
    private void ResetSlider()
    {
        animationComplete = false;
        currentValue = 0f;
    }

    private void SetScoreTextTo(float value)
    {
        float wholeNumberPercent = value * 100;
        scoreNumber.text = wholeNumberPercent.ToString("0.0")+"%";
    }

}