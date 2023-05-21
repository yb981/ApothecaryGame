using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum patientPhase
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

    }

    void talk()
    {

    }

    void wait()
    {

    }

    void leave()
    {

    }
}
