using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public enum patientPhase
{
    Enter,
    Talk,
    Wait,
    Leave,
    Score
}

public enum PatientTalkPhase
{
    Talk,
    Stats,
    Nothing,
}

public class Patient : MonoBehaviour
{
    [Header("ClientsUI")]
    [SerializeField] Canvas clientCanvas;
    [SerializeField] List<PatientSO> patientList = new List<PatientSO>();
    [SerializeField] GameObject goalPosition;
    [SerializeField] float moveFactor = 0.01f;


    [Header("SicknessUI")]
    [SerializeField] Slider caughBar;
    [SerializeField] Slider bleedBar;
    [SerializeField] Slider feverBar;
    [SerializeField] TextMeshProUGUI TMPv1;
    [SerializeField] TextMeshProUGUI TMPv2;
    [SerializeField] TextMeshProUGUI TMPv3;

    public event EventHandler<OnPatientTalkPhaseChangedEventArgs> OnPatientTalkPhaseChanged;
    public class OnPatientTalkPhaseChangedEventArgs : EventArgs
    {
        public PatientTalkPhase talkPhase;
        public PatientSO currentPatient;
    }

    private patientPhase state = patientPhase.Enter;
    private PatientTalkPhase talkState = PatientTalkPhase.Nothing;
    private PatientSO patientDetails;
    private PatientSO currentClient;
    private bool gotNewClient = false;
    private bool firstTalk = true;
    private bool firstLeave = true;
    private int totalNumberOfPatients = -1;
    private int currentPatientCount = 0;

    // Client Stats
    string currentClientName;
    string currentClientRequest;
    int[] currentClientSicknessValues = new int[3];
    Sprite currentClientSprite;
    
    private void Awake() 
    {
        // Save total patient numbers
        totalNumberOfPatients = patientList.Count;
    }

    void Start()
    {
        // Name Values
        TMPv1.text = GameConstants.VALUE1;
        TMPv2.text = GameConstants.VALUE2;
        TMPv3.text = GameConstants.VALUE3;

        // Disable text first
        //displayClientStats(false);

        // disable interaction on sliders
        caughBar.enabled = false;
        bleedBar.enabled = false;
        feverBar.enabled = false;

        // Subscribe to Manager
        GameManager.Instance.LevelFinished += Patient_LevelFinished;
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
        // if game over
        if(!getNewClient()) GameManager.Instance.NoMoreClients();

        Transform goal = goalPosition.transform.GetChild(0);
        moveTo(goal.position);

        if(goal.position == transform.position)
        {
            GameManager.Instance.informPhaseCompleted();
        }
    }

    void talk()
    {
        if(firstTalk)
        {
            talkState = PatientTalkPhase.Talk;
            OnPatientTalkPhaseChanged?.Invoke(this, new OnPatientTalkPhaseChangedEventArgs{ talkPhase = talkState, currentPatient = currentClient });    
            firstTalk = false;
        }
        
        if(Input.GetMouseButtonDown(0) && talkState == PatientTalkPhase.Talk)
        {
            talkState = PatientTalkPhase.Stats;
            OnPatientTalkPhaseChanged?.Invoke(this, new OnPatientTalkPhaseChangedEventArgs{ talkPhase = talkState, currentPatient = currentClient });

            GameManager.Instance.informPhaseCompleted();
        }

        //TMPrequestText.enabled = true;
        //displayClientStats(true);
        
    }

    void wait()
    {
        // Wait for Player Input
    }

    void leave()
    {
        // Stop talking
        if(firstLeave)
        {
            talkState = PatientTalkPhase.Nothing;
            OnPatientTalkPhaseChanged?.Invoke(this, new OnPatientTalkPhaseChangedEventArgs{ talkPhase = talkState});
            firstLeave = false;
        }

        Transform goal = goalPosition.transform.GetChild(1);
        if(moveTo(goal.position))   
        {
            //GameManager.Instance.informPhaseCompleted();
        }

    }

    void Score()
    {
        // Reset Flag for new Cycle
        gotNewClient = false;
        firstTalk = true;
        firstLeave = true;
    }

    private void Patient_LevelFinished(object sender, GameManager.LevelFinishedEventArgs e)
    {
        gameObject.SetActive(false);
    }
    
    private bool moveTo(Vector3 goal) 
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

    bool getNewClient()
    {

        // Do this method only once per enter
        if(gotNewClient) return true;

        if(patientList.Count == 0) return false;

        // get random ID
        int randomId = UnityEngine.Random.Range(0,patientList.Count);


        currentClient = patientList[randomId];
        currentClientName = currentClient.getNPCname();
        currentClientRequest = currentClient.getRequestText();
        currentClientSicknessValues = currentClient.getSicknessValues();

        //TMPrequestText.text = currentClientName + ": "+currentClientRequest;

        // Remove current client from list, so he only appears once
        // can later add flag for clients to re-apear
        if(patientList.Contains(currentClient)){ patientList.Remove(currentClient);}
        // Maybe add this to a later state. But would need to introduce new state only for delete#

        // Increase count to know how many visited
        currentPatientCount++;

        // Do not Enter again
        gotNewClient = true;
        return true;
    }

    // Getter
    public int[] getPatientSicknessValues()
    {
        return currentClientSicknessValues;
    }

    public int GetTotalNumberOfPatients()
    {
        return totalNumberOfPatients;
    }

    public int GetCurrentPatientNumber()
    {
        return currentPatientCount;
    }
}
