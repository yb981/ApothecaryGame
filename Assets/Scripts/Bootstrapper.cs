using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bootstrapper : MonoBehaviour
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void LoadMainHandler()
    {
        GameObject runhandler = GameObject.Instantiate(Resources.Load("RunHandler")) as GameObject;

        GameObject soundHandler = GameObject.Instantiate(Resources.Load("AudioHandler")) as GameObject;
    }
}
