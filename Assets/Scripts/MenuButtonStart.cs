using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuButtonStart : MonoBehaviour
{
    private Button myButton;

    private void Awake() 
    {
        myButton = GetComponent<Button>();
    }

    void Start()
    {
        myButton.onClick.AddListener(() => {RunHandler.Instance.NextLevel(); });
    }
}
