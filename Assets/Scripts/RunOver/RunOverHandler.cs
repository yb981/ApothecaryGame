using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunOverHandler : MonoBehaviour
{
    
    void Start()
    {
        ResetRunHandler();    
    }

    private void ResetRunHandler()
    {
        RunHandler.Instance.ResetRun();
    }
}
