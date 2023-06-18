using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingUI : MonoBehaviour
{
    
    [SerializeField] RectTransform myRectTransform;
    [SerializeField] float floatTimeInSeconds = 2f;
    [SerializeField] float amplitude = 0.5f;
    private float y = 0f;
    private float timer = 0.5f;
    private float goalUp;
    private float goalDown;
    private float amount;
    private bool invert = true;

    private void Start() 
    {
        goalUp = myRectTransform.position.y + amplitude;
        goalDown = myRectTransform.position.y - amplitude;
    }

    void Update()
    {
        if(!invert){
            // move up 0-1
            timer += Time.deltaTime;
            amount = timer/floatTimeInSeconds;
            if(amount >= 1) invert = true;
        }else{
            // move down 1-0
            timer -= Time.deltaTime;
            amount = timer/floatTimeInSeconds;
            if(amount <= 0) invert = false;
        }
        
        y = Mathf.Lerp(goalDown,goalUp,amount);

        // add to position
        myRectTransform.position = new Vector3(myRectTransform.position.x,y,myRectTransform.position.z);

    }
}
