using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatientLooks : MonoBehaviour
{

    [Header("Coding")]
    [SerializeField] Patient myPatient;
    [SerializeField] SpriteRenderer headSprite;
    [SerializeField] SpriteRenderer bodySprite;

    [SerializeField] List<Sprite> femaleHeads; 
    [SerializeField] List<Sprite> femaleBodies;

    // Start is called before the first frame update
    void Start()
    {
        myPatient.OnNewPatient += Patient_OnNewPatient;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void Patient_OnNewPatient(object sender, Patient.OnNewPatientEventArgs e)
    {
        // assuming female
        // get sprite
        Sprite head = femaleHeads[UnityEngine.Random.Range(0,femaleHeads.Count)];
        headSprite.sprite = head;
        Sprite body = femaleBodies[UnityEngine.Random.Range(0,femaleBodies.Count)];
        bodySprite.sprite = body;

    }
}
