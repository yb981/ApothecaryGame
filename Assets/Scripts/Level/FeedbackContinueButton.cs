using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FeedbackContinueButton : MonoBehaviour
{
    private Button myButton;

    // Start is called before the first frame update
    void Start()
    {
        myButton = GetComponent<Button>();
    }

    private void Update()
    {
        bool isHelperActive = Helper.Instance.GetUserAdjustedValues();

        // Set Buttons activty as oppsite as helper active
        myButton.interactable = isHelperActive;
    } 
}
