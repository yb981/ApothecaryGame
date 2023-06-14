using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunHandler : MonoBehaviour
{

    public static RunHandler Instance { get {return instance;}}
    private static RunHandler instance;

    void Awake()
    {
        if(instance != null)
        {
            Destroy(this.gameObject);
        }else{
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }
}
