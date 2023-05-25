using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

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
    PatientSO patientDetails;
    [SerializeField] List<PatientSO> patientList = new List<PatientSO>();
    [SerializeField] GameObject goalPosition;
    [SerializeField] float moveFactor = 0.01f;
    [SerializeField] TextMeshPro TMPrequestText;

    [Header("SicknessUI")]
    [SerializeField] Slider caughBar;
    [SerializeField] Slider bleedBar;
    [SerializeField] Slider feverBar;


    string currentClientName;
    string currentClientRequest;
    int[] currentClientSicknessValues = new int[3];
    Sprite currentClientSprite;
    
    void Start()
    {
        // Disable text first
        TMPrequestText.enabled = false;
        displayClientStats(false);
        
        // get first client
        PatientSO currentClient = patientList[0];
        currentClientName = currentClient.getNPCname();
        currentClientRequest = currentClient.getRequestText();
        currentClientSicknessValues = currentClient.getSicknessValues();

        TMPrequestText.text = currentClientName + ": "+currentClientRequest;
    }

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
            GameManager.Instance.informPhaseCompleted();
        }
    }


    void talk()
    {
        Debug.Log("blabla");
        TMPrequestText.enabled = true;
        displayClientStats(true);
        GameManager.Instance.informPhaseCompleted();
    }

    void wait()
    {
        // Wait for Player Input
        Debug.Log(GameManager.Instance.getGamePhase());
    }

    void leave()
    {
        // Stop talking
        TMPrequestText.enabled = false;
        displayClientStats(false);

        Transform goal = goalPosition.transform.GetChild(1);
        if(moveTo(goal.position))   
        {
            GameManager.Instance.informPhaseCompleted();
            getNewClient();
        }

    }
    
    bool moveTo(Vector3 goal) 
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

        return goal == currentPosition;
    }

    public void setPhase(patientPhase newPhase)
    {
        state = newPhase;
    }

    void displayClientStats(bool state)
    {
        caughBar.enabled = state;
        bleedBar.enabled = state;
        feverBar.enabled = state;
        setClientStatsToUI();
    }

    void setClientStatsToUI()
    {
        caughBar.value = currentClientSicknessValues[0];
        bleedBar.value = currentClientSicknessValues[1];
        feverBar.value = currentClientSicknessValues[2];
    }

    void getNewClient()
    {
        // get random ID
        int randomId = Random.Range(0,patientList.Count);


        PatientSO currentClient = patientList[randomId];
        currentClientName = currentClient.getNPCname();
        currentClientRequest = currentClient.getRequestText();
        currentClientSicknessValues = currentClient.getSicknessValues();

        TMPrequestText.text = currentClientName + ": "+currentClientRequest;

        // Remove current client from list, so he only appears once
        // can later add flag for clients to re-apear
        if(patientList.Contains(currentClient)){ patientList.Remove(currentClient);}
    }
}
