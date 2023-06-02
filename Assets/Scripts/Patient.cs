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
    Leave,
    Score
}

public class Patient : MonoBehaviour
{

    patientPhase state = patientPhase.Enter;
    PatientSO patientDetails;
    bool gotNewClient = false;

    public string[] sicknessValueNames = {"Caugh","Blood","Fever"};


    [Header("ClientsUI")]
    [SerializeField] Canvas clientCanvas;
    [SerializeField] List<PatientSO> patientList = new List<PatientSO>();
    [SerializeField] TextMeshProUGUI TMPrequestText;
    [SerializeField] GameObject goalPosition;
    [SerializeField] float moveFactor = 0.01f;


    [Header("SicknessUI")]

    [SerializeField] Slider caughBar;
    [SerializeField] Slider bleedBar;
    [SerializeField] Slider feverBar;
    [SerializeField] TextMeshProUGUI TMPv1;
    [SerializeField] TextMeshProUGUI TMPv2;
    [SerializeField] TextMeshProUGUI TMPv3;


    // Client Stats
    string currentClientName;
    string currentClientRequest;
    int[] currentClientSicknessValues = new int[3];
    Sprite currentClientSprite;
    
    
    void Start()
    {
        // Name Values
        TMPv1.text = sicknessValueNames[0];
        TMPv2.text = sicknessValueNames[1];
        TMPv3.text = sicknessValueNames[2];


        // Disable text first
        TMPrequestText.enabled = false;
        displayClientStats(false);
        
        // get first client
        //PatientSO currentClient = patientList[0];
        //currentClientName = currentClient.getNPCname();
        //currentClientRequest = currentClient.getRequestText();
        //currentClientSicknessValues = currentClient.getSicknessValues();

        //TMPrequestText.text = currentClientName + ": "+currentClientRequest;

        // disable interaction on sliders
        caughBar.enabled = false;
        bleedBar.enabled = false;
        feverBar.enabled = false;
    }

    void Update()
    {
        switch(state)
        {
            case patientPhase.Enter:    enter();    break;
            case patientPhase.Talk:     talk();     break;
            case patientPhase.Wait:     wait();     break;
            case patientPhase.Leave:    leave();    break;
            case patientPhase.Score:    Score();    break;
            default:                                break;
        }
    }

    void enter()
    {
        getNewClient();
        Transform goal = goalPosition.transform.GetChild(0);
        moveTo(goal.position);

        if(goal.position == transform.position)
        {
            GameManager.Instance.informPhaseCompleted();
        }
    }


    void talk()
    {
        TMPrequestText.enabled = true;
        displayClientStats(true);
        GameManager.Instance.informPhaseCompleted();
    }

    void wait()
    {
        // Wait for Player Input
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
        }

    }

    void Score()
    {
        // Reset Flag for new Cycle
        gotNewClient = false;
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
        clientCanvas.enabled = state;
        caughBar.gameObject.SetActive(state);
        bleedBar.gameObject.SetActive(state);
        feverBar.gameObject.SetActive(state);
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
        // Do this method only once per enter
        if(gotNewClient) return;

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
        // Maybe add this to a later state. But would need to introduce new state only for delete#

        // Do not Enter again
        gotNewClient = true;
    }

    // Getter
    public int[] getPatientSicknessValues()
    {
        return currentClientSicknessValues;
    }
}
