using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnClickFeedback : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnMouseUp() 
    {
        GetComponentInParent<PlayerFeedback>().PlayerWantsToContinue();    
    }
}
