using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LoadingText : MonoBehaviour
{
    private TextMeshProUGUI myText;
    private string points = ".";
    private string loading = "Loading "; 
    private float timer;
    [SerializeField] private float animationSpeed = 1f;
    [SerializeField] private SceneHandler sceneHandler;

    private void Awake() 
    {
        myText = GetComponent<TextMeshProUGUI>();
        myText.text = loading;
    }

    private void Update() 
    {
        myText.text = loading;
        timer += Time.deltaTime * animationSpeed;
        int numberOfPoints = 3;
        int amount = (int) timer % numberOfPoints;
        for (int i = 0; i < amount+1; i++)
        {
            myText.text += points;
        }

        if(myText.text == "Loading ...")
        {
            sceneHandler.LoadNextScene();
            Debug.Log("loading next");
        }
    }
}
