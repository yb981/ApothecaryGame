using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreBar : MonoBehaviour
{

    [SerializeField] Slider score;
    [SerializeField] TextMeshProUGUI scoreNumber;
    [SerializeField] float fillSpeed = 0.001f;
    Animator myAnimator;
    float scoreValue = 0;
    bool displayScore = false;
    bool justStarted = true;
    float currentValue = 0;
    bool animationComplete = false;

    void Awake()
    {
        myAnimator = GetComponent<Animator>();
    }

    private void Start() 
    {
        score.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(displayScore && !animationComplete)
        {
            currentValue += fillSpeed;

            score.value = currentValue;
            //Debug.Log("Slider: "+ score.value);
            //Debug.Log("ScoreV: "+ scoreValue);
            if(score.value >= scoreValue)
            {
                animationComplete = true;
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

        float wholeNumberPercent = newScore * 100;
        scoreNumber.text = wholeNumberPercent.ToString("0.0")+"%";
    }

    public void toggleScoreDisplay(bool state)
    {
        displayScore = state;

        score.gameObject.SetActive(state);
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

}
