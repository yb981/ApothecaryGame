using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Helper : MonoBehaviour
{

    [SerializeField] TextMeshPro dropTMP;
    [SerializeField] TextMeshPro dropTMParrow;
    [SerializeField] TextMeshPro pickTMP;
    [SerializeField] TextMeshPro pickTMParrow;
    Animator myAnimator;

    void Start()
    {
        displayHelperText(true);
        myAnimator = GetComponent<Animator>();
    }

    public void inoformIsHolding(bool state)
    {
        myAnimator.SetBool("isHolding", state);
    }

    public void displayHelperText(bool state)
    {
        dropTMP.gameObject.SetActive(state);
        dropTMParrow.gameObject.SetActive(state);
        pickTMP.gameObject.SetActive(state);
        pickTMParrow.gameObject.SetActive(state);
    }
}
