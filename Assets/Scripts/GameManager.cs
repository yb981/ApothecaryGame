using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance { get {return instance;} }
    private static GameManager instance;
    
    public event EventHandler<VillageHealthChangedEventArgs> VillageHealthChanged;
    public class VillageHealthChangedEventArgs : EventArgs 
    {
        public int villageHealthChange;
    }

    public event EventHandler<LevelFinishedEventArgs> LevelFinished;
    public class LevelFinishedEventArgs : EventArgs
    {
        public int eMaxVillageHealth;
        public int eVillageHealth;
        public int eMaxClients;
        public int eSurvivers;
    }

    [SerializeField] UITracker uiTracker;
    [SerializeField] FinalScreen finalScreen;

    patientPhase gamePhase = patientPhase.Enter;
    Patient pat;
    Slider scoreBar;
    TextMeshProUGUI scoreText;
    ScoreBar score;
    GameObject helper;
    bool displayHelper = true;
    HandleAdminSliders handleSliders;
    private int maxVillageHealth = 10;
    private int villageHealth = -1; 
    private int maxClients = -1;
    private int survivers = 0;

    int[] resultPotion = new int[3];
    int[] valueSickness = new int[3];

    // Start is called before the first frame update
    void Awake()
    {
        if(instance != null && instance != this) 
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            //DontDestroyOnLoad(this.gameObject);
        }

        // Set village health
        villageHealth = maxVillageHealth;
    }

    void Start()
    { 
        referenceScore();
        displayScoreBar(false);
        helper = GameObject.Find("Helper");
        handleSliders = FindObjectOfType<HandleAdminSliders>();
        uiTracker.UpdateVillageBar(villageHealth);
        uiTracker.SetVillageMaxHealthBar(maxVillageHealth);
        pat = GameObject.FindObjectOfType<Patient>();
        maxClients = pat.GetTotalNumberOfPatients();
    }

    void setGamePhaseNext()
    {
        
        //pat = GameObject.FindObjectOfType<Patient>();
        
        // Setting next Gamephase for Client
        switch(gamePhase)
        {
            case patientPhase.Enter:
                displayScoreBar(false);   
                gamePhase = patientPhase.Talk;   
                pat.setPhase(gamePhase); 
                break;

            case patientPhase.Talk:     
                gamePhase = patientPhase.Wait;  
                pat.setPhase(gamePhase);   
                uiTracker.NewClient(); 
                break;

            case patientPhase.Wait:
                if(displayHelper) disableHelper();
                gamePhase = patientPhase.Leave;
                pat.setPhase(gamePhase);     
                break;

            case patientPhase.Leave:    
                gamePhase = patientPhase.Score;
                pat.setPhase(gamePhase);
                TriggerFeedbackController();
                helper.GetComponent<Helper>().InformIsFeedbackScreen();     
                break;

            case patientPhase.Score:    
                gamePhase = patientPhase.Enter;
                pat.setPhase(gamePhase);
                helper.GetComponent<Helper>().HelperActivated(false);
                break;

            default:                                break;
        }
    }

    private void TriggerFeedbackController()
    {
        PlayerFeedback playerFeedbackController = FindAnyObjectByType<PlayerFeedback>();
        playerFeedbackController.StartFeedback(resultPotion,valueSickness);
    }

    public void informPhaseCompleted()
    {
        // Calculate Potion and result if gamephase is waiting for player input
        if(gamePhase == patientPhase.Wait)
        {
            mixContainerIngredients();
        }

        setGamePhaseNext();
    }

    public patientPhase getGamePhase()
    {
        return gamePhase;
    }

    private void mixContainerIngredients()
    {
        // Get both ObjectReferences (bad practice, will need to learn a better way later on)
        if(pat == null) pat = GameObject.FindObjectOfType<Patient>();
        ContainerCalculation containerScript = GameObject.FindObjectOfType<ContainerCalculation>();

        // Init both tmp Arrays to compare
        resultPotion = new int[3];
        valueSickness = new int[3];
        
        resultPotion = containerScript.mixInsertIngredients();
        valueSickness = pat.getPatientSicknessValues();

        // Calculate Result
        int result = resultFormula(valueSickness, resultPotion);

        // Handle Village
        CalculateVillageHealth(result);

        // Display Result
        displayResultSuccess(result);

        // Reset Container Values
        containerScript.setValues(0,0,0);
    }

    private int resultFormula(int[] sickness, int[] input)
    {
        int score = 0;
        int[] diff = new int[3];

        // get all differences
        for (int i = 0; i < diff.Length; i++)
        {
            // take the diff
            // eg. 8 - 4 = 4
            diff[i] = sickness[i] - input[i];

            // take double of diff
            // eg 4*2 = 8
            diff[i] *= 2;

            // Take Absolute of diff
            // eg |8|
            diff[i] = Math.Abs(diff[i]);
            
            // substract from max value
            // eg 10 - 8 = 2
            diff[i] = 10 - diff[i];

            // make sure value is in range and not negative
            // CAN REMOVE THIS FOR DIFFICULTY INCREASE
            diff[i] = Mathf.Clamp(diff[i],0,10);

            // add up
            score += diff[i];
        }
        return score;
    }

    private void CalculateVillageHealth(int result)
    {
        int healthChange = 0;
        int badResultValue = 20;
        int veryBadResultValue = 15;
        int goodResultValue = 27;
 
        if(result > goodResultValue)            //if score is very high increase health
        {
            healthChange = 1;
        }
        else if(result < veryBadResultValue)    //if score is very low decrease health
        {
            healthChange = -2;
        }
        else if(result < badResultValue)        // if score is low decrease health
        {
            healthChange = -1;
        }
        else                                    // dont change health
        {
            healthChange = 0;
        }

        // Count survivers
        if(healthChange != -2) survivers++;

        // update village health
        // check if dead
        if(villageHealth+healthChange <= 0)
        {
            // Game Over
            villageHealth = 0;

            // Run Game Over
        }else if( villageHealth+healthChange >= maxVillageHealth)
        {
            villageHealth = maxVillageHealth;
        }
        else
        {
            villageHealth += healthChange;
        }

        VillageHealthChanged?.Invoke(this, new VillageHealthChangedEventArgs {
            villageHealthChange = healthChange
        });

        uiTracker.UpdateVillageBar(villageHealth);
    }
    
    void displayScoreBar(bool state)
    {
        if(score == null) referenceScore();
        score.toggleScoreDisplay(state);
    }

    void displayResultSuccess(int result)
    {
        if(score == null) referenceScore();

        // make sure scorebar is visible
        if(!score.isScoreBarDisplayed()) displayScoreBar(true);

        float resultPercent = result/30f;

        score.setScore(resultPercent);
    }

    private void referenceScore()
    {
        score = FindAnyObjectByType<ScoreBar>();
        //scoreBar = score.GetComponentInChildren<Slider>();
        //scoreText = score.GetComponentInChildren<TextMeshProUGUI>();
    }

    private void disableHelper()
    {
        displayHelper = false;
        helper.GetComponent<Helper>().displayHelperText(false);   
    }

    public void NoMoreClients()
    {
        LevelFinished?.Invoke(this, new LevelFinishedEventArgs{
            eMaxVillageHealth = maxVillageHealth,
            eVillageHealth = villageHealth,
            eMaxClients = maxClients,
            eSurvivers = survivers
        });
    }
}
