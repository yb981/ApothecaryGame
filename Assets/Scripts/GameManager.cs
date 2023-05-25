using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    patientPhase gamePhase = patientPhase.Enter;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void setGamePhase(patientPhase phase)
    {
        /*Patients patient = GameObject.Find("Patient1");
        switch(phase)
        {
            case patientPhase.Enter:    patient.setPhase();    break;
            case patientPhase.Talk:     talk();     break;
            case patientPhase.Wait:     wait();     break;
            case patientPhase.Leave:    leave();    break;
            default:                                break;
        }*/
    }

    public void informPhaseCompleted()
    {
        setGamePhase(gamePhase++);
    }

    public patientPhase getGamePhase()
    {
        return gamePhase;
    }

    void getNewClient()
    {

    }

    void displayClientText()
    {

    }

    void displayClientStats()
    {

    }

    void getClientOut()
    {

    }
}
