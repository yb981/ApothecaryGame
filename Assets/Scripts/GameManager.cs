using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static GameManager  instance;
    public static GameManager Instance { get { return instance;} }
    patientPhase gamePhase = patientPhase.Enter;
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

    // Update is called once per frame
    void Update()
    {
        
    }

    void setGamePhaseNext()
    {
        
        //Debug.Log("setting next gamephase, from "+ gamePhase);
        switch(gamePhase)
        {
            case patientPhase.Enter:    gamePhase = patientPhase.Talk;    break;
            case patientPhase.Talk:     gamePhase = patientPhase.Wait;     break;
            case patientPhase.Wait:     gamePhase = patientPhase.Leave;     break;
            case patientPhase.Leave:    
                getNewClient();
                gamePhase = patientPhase.Enter;    break;
            default:                                break;
        }
        Patient pat = GameObject.FindObjectOfType<Patient>();
        pat.setPhase(gamePhase);
        Debug.Log("now "+ gamePhase);
    }

    public void informPhaseCompleted()
    {
        setGamePhaseNext();
    }

    public patientPhase getGamePhase()
    {
        return gamePhase;
    }

    void getNewClient()
    {
        // Getting new client
        Debug.Log("new client!");
    }

    void displayClientStats()
    {

    }
}
