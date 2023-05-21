using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Patient",fileName = "new Patient")]
public class PatientSO : ScriptableObject
{
    [SerializeField] string introductionText = "blabla";
    [SerializeField] string NPCname = "Peter";
    [SerializeField] Sprite looks;
    [SerializeField] int[] sickness;
}
