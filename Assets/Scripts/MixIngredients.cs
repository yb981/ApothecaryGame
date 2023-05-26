using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MixIngredients : MonoBehaviour
{

    [SerializeField] GameObject container;
    ContainerCalculation containerScript;
    void Start()
    {
        containerScript = GetComponent<ContainerCalculation>();
    }

    private void OnMouseDown() {
        // only if in acation phase
        if(GameManager.Instance.getGamePhase() == patientPhase.Wait) {
            GameManager.Instance.informPhaseCompleted();
        }else{
            Debug.Log("trying to mix when not in correct gamephase.");
            Debug.Log("phase: "+ GameManager.Instance.getGamePhase());
        }
    }
}
