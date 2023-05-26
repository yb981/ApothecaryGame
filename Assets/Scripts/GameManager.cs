using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    static GameManager  instance;
    public static GameManager Instance { get { return instance;} }
    patientPhase gamePhase = patientPhase.Enter;
    Patient pat;
    Slider scoreBar;
    

    bool readyToCombine = false;
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
            DontDestroyOnLoad(this.gameObject);
        }
    }

    void Start()
    {
        scoreBar = GameObject.Find("Score").GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void setGamePhaseNext()
    {
        
        // Setting next Gamephase for Client
        switch(gamePhase)
        {
            case patientPhase.Enter:    gamePhase = patientPhase.Talk;    break;
            case patientPhase.Talk:     gamePhase = patientPhase.Wait;     break;
            case patientPhase.Wait:     gamePhase = patientPhase.Leave;     break;
            case patientPhase.Leave:    gamePhase = patientPhase.Enter;    break;
            default:                                break;
        }
        pat = GameObject.FindObjectOfType<Patient>();
        pat.setPhase(gamePhase);
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
        int[] resultPotion = new int[3];
        int[] valueSickness = new int[3];
        
        resultPotion = containerScript.mixInsertIngredients();
        valueSickness = pat.getPatientSicknessValues();

        // Calculate Result
        int result = resultFormula(valueSickness, resultPotion);

        // Display Result
        displayResultSuccess(result);

        // Reset Container Values
        containerScript.setValues(0,0,0);
    }

    private int resultFormula(int[] sickness, int[] input)
    {

        Debug.Log("calculating score value");
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

            Debug.Log("adding: "+diff[i]);

            // add up
            score += diff[i];
        }

        return score;
    }
    
    void displayResultSuccess(int result)
    {
        if(scoreBar == null) scoreBar = GameObject.Find("Score").GetComponent<Slider>();
        float resultPercent = result/30f;
        scoreBar.value = resultPercent;
         Debug.Log("Score in numbers is: "+ result);
        Debug.Log("Score in % is: "+ resultPercent);
    }
}
