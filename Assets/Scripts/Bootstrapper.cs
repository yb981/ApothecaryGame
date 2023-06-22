using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bootstrapper : MonoBehaviour
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void LoadMainHandler()
    {
        Debug.Log(Resources.Load("RunHandler"));
        GameObject runhandler = GameObject.Instantiate(Resources.Load("RunHandler")) as GameObject;
        GameObject.DontDestroyOnLoad(runhandler);
    }
}
