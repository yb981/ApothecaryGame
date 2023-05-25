using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MixIngredients : MonoBehaviour
{

    [SerializeField] GameObject container;
    void Start()
    {

    }

    private void OnMouseDown() {
        // only if in acation phase
        if(GameManager.Instance.getGamePhase() == patientPhase.Wait) {
            mixContainerIngredients();
            GameManager.Instance.informPhaseCompleted();
        }else{
            Debug.Log("trying to mix when not in correct gamephase.");
            Debug.Log("phase: "+ GameManager.Instance.getGamePhase());
        }
    }

    private void mixContainerIngredients()
    {
        //Mix Ingredients
        Debug.Log("Mixing the ingredients together");
        container.GetComponent<ContainerCalculation>().mixInsertIngredients();
    }
}
