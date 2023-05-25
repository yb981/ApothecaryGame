using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum patientPhase
{
    Enter,
    Talk,
    Wait,
    Leave
}

public class Patient : MonoBehaviour
{

    patientPhase state = patientPhase.Enter;
    [SerializeField] List<PatientSO> patientList = new List<PatientSO>();
    [SerializeField] GameObject goalPosition;
    [SerializeField] float moveFactor = 0.01f;
    [SerializeField] GameManager manager;
    //[SerializeField] TextMashProUGUI clientRequest;

    void Update()
    {
        switch(state)
        {
            case patientPhase.Enter:    enter();    break;
            case patientPhase.Talk:     talk();     break;
            case patientPhase.Wait:     wait();     break;
            case patientPhase.Leave:    leave();    break;
            default:                                break;
        }
    }

    void enter()
    {
        Transform goal = goalPosition.transform.GetChild(0);
        moveTo(goal.position);

        if(goal.position == transform.position)
        {
            Debug.Log("finished enter");
            manager.informPhaseCompleted();
            state = patientPhase.Leave;
        }
    }


    void talk()
    {
        Debug.Log("blabla");
        state = patientPhase.Wait;
    }

    void wait()
    {
        // Wait for Player Input
    }

    void leave()
    {
        Transform goal = goalPosition.transform.GetChild(1);
        moveTo(goal.position);
    }
    
    void moveTo(Vector3 goal) 
    {
        Vector3 currentPosition = transform.position;
        Vector3 deltaPos = new Vector3(0,0,0);
        deltaPos = goal-currentPosition;
        if(Mathf.Abs(deltaPos.x) < 0.1){
            currentPosition = goal;
        }else{
            currentPosition += deltaPos * moveFactor;
        }

        transform.position = currentPosition;
    }

    public void setPhase(patientPhase newPhase)
    {
        state = newPhase;
    }
}
