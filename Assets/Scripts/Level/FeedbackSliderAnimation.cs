using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedbackSliderAnimation : MonoBehaviour
{

    [SerializeField] Animator myAnimator;
    private Helper helper;

    // Start is called before the first frame update
    void Awake()
    {
        // Activate Helper animation
        myAnimator?.SetBool("helperIsOn", true);

    }

    private void Start() 
    {
        // Activate Helper animation
        helper = Helper.Instance;
    }

    private void Update() 
    {
        // Activate Helper animation
        if(Helper.Instance.IsHelperEnabled()) myAnimator?.SetBool("helperIsOn", true);
    }

}
