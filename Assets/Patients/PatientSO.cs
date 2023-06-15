using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Patient",fileName = "new Patient")]
public class PatientSO : ScriptableObject
{   

    public enum Gender
    {
        male,
        female
    }

    [TextArea(1,3)]
    [SerializeField] string introductionText = "Enter Request by Client";
    [SerializeField] string NPCname = "Enter NPC Name";
    [SerializeField] public Gender gender;
    [SerializeField] Sprite looks;
    [SerializeField] int[] sickness = new int[3];

    // Getters
    public string getRequestText()
    {
        return introductionText;
    }

    public string getNPCname()
    {
        return NPCname;
    }

    public int[] getSicknessValues()
    {
        return sickness;
    }
}
