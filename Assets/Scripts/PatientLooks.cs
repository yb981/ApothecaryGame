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
    [SerializeField] List<Sprite> maleHeads; 
    [SerializeField] List<Sprite> maleBodies;

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
        // check gender
        if(e.currentPatient.gender == PatientSO.Gender.male)
        {
            Sprite head = maleHeads[UnityEngine.Random.Range(0,maleHeads.Count)];
            headSprite.sprite = head;
            Sprite body = maleBodies[UnityEngine.Random.Range(0,maleBodies.Count)];
            bodySprite.sprite = body;
        }else{
            // get sprite
            Sprite head = femaleHeads[UnityEngine.Random.Range(0,femaleHeads.Count)];
            headSprite.sprite = head;
            Sprite body = femaleBodies[UnityEngine.Random.Range(0,femaleBodies.Count)];
            bodySprite.sprite = body;
        }

    }
}
