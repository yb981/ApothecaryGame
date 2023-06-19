using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Patient Data SO",fileName ="NewPatientDataSO")]
public class PatientDataSO : ScriptableObject
{
    [SerializeField] private List<string> indtroductionTexts;
    [SerializeField] private List<string> maleNames;
    [SerializeField] private List<string> femaleNames;

    public string GetOneMaleName()
    {
        return maleNames[Random.Range(0,maleNames.Count)];
    }

    public string GetOneFemaleName()
    {
       return femaleNames[Random.Range(0,femaleNames.Count)];
    }

    public string GetOneIntroductionsTexts()
    {
        return indtroductionTexts[Random.Range(0,indtroductionTexts.Count)];
    }
}
