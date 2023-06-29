using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonBackUI : MonoBehaviour
{
    private Button button;

    private void Awake() 
    {
        button = GetComponent<Button>();
    }

    void Start()
    {
        button.onClick.AddListener(ReturnToMenu);
    }

    private void ReturnToMenu()
    {
        StartCoroutine(LoadDelay());
    }

    private IEnumerator LoadDelay()
    {
        yield return new WaitForEndOfFrame();
        Loader.NextSceneWithoutLoadingScreen(GameConstants.SCENE_MENU);
    }
}
