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

    private bool entering = false;
    private bool leaving = false;
    private float alpha = 1;
    private float alphaAmount = 0f;
    private float fadeOutFactor = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        myPatient.OnNewPatient += Patient_OnNewPatient;
        myPatient.OnEnter += Patient_OnEnter;
        myPatient.OnLeave += Patient_OnLeave;
    }

    private void Update() 
    {
        InAndOutFade();
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

        // set the reference in on object
        PatientBodySO newLooksSO = new PatientBodySO();
        newLooksSO.SetNewSprites(headSprite.sprite,bodySprite.sprite);
        myPatient.SetPatientSprite(newLooksSO);

    }
    
    private void Patient_OnEnter()
    {
        entering = true;
        alphaAmount = 0;
    }
    
    private void Patient_OnLeave()
    {
        alphaAmount = 0;
        leaving = true;
    }

    private void InAndOutFade()
    {
        if(entering)
        {
            if(FadeSpriteFromTo(0,1)) entering = false;
        }
        else if(leaving)
        {
            if(FadeSpriteFromTo(1,0)) leaving = false;
        }
    }

    private bool FadeSpriteFromTo(float from, float to)
    {
        alphaAmount += Time.deltaTime;
        alpha = Mathf.Lerp(from,to,alphaAmount/fadeOutFactor);
        headSprite.color = new Color(1,1,1,alpha);
        bodySprite.color = new Color(1,1,1,alpha);

        if(alpha == to) return true;
        return false;
    }
}
