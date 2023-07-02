using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializeClients : MonoBehaviour
{
    [SerializeField] private Patient patient;
    //[SerializeField] private InitializeIngredients initializeIngredients;
    [SerializeField] private PatientDataSO patientDataSO;

    [Header("Direct Testing in Scene Number")]
    [SerializeField] private int TestnumberOfClients = 4;

    private List<PatientSO> patientList;
    private int maxClients;
    private int maxSicknessValue;

    void Start()
    {
        if(RunHandler.Instance != null)
        {
            LevelSettingsSO currentLevel = RunHandler.Instance.GetCurrentLevel();
            maxClients = currentLevel.AmountOfPatients;
        }else{
            // Default Number
            maxClients = TestnumberOfClients;
        }
        
        
        StartInitilizing();
    }

    private List<PatientSO> InitializePatients()
    {
        List<PatientSO> newPatients = new List<PatientSO>();

        // Get All Ingredients
        List<IngredientSO> ingredients = new List<IngredientSO>(RunHandler.Instance.GetCurrentEndlessIngredients());

        // Create all patients
        for (int z = 0; z < maxClients; z++)
        {
            // New Patient
            PatientSO newPatient = ScriptableObject.CreateInstance<PatientSO>();

            // Gender
            PatientSO.Gender[] allValues = (PatientSO.Gender[])Enum.GetValues(typeof(PatientSO.Gender));
            PatientSO.Gender newGender = allValues[UnityEngine.Random.Range(0,allValues.Length)];
            newPatient.SetGender(newGender);

            // Text
            string newIntroductionText = patientDataSO.GetOneIntroductionsTexts();
            newPatient.SetIntroductionText(newIntroductionText);

            // Name
            string newName = "";
            switch (newGender)
            {
                case PatientSO.Gender.male:         newName = patientDataSO.GetOneMaleName(); break;
                case PatientSO.Gender.female:         newName = patientDataSO.GetOneFemaleName(); break;
                default: break;
            }
            newPatient.SetName(newName);

            // Looks
            // At this point still randomized in Visuals

            // Stats
            int maxValues = 3;
            int[] newSicknessValues = new int[maxValues];
            
            int ingredientAmount = 3;
            // add up values of 3 ingredients
            for (int i = 0; i < ingredientAmount; i++)
            {
                // Get Random Ingredient
                IngredientSO randomIngredient = ingredients[UnityEngine.Random.Range(0,ingredients.Count)];
                int[] ingValues = randomIngredient.getValues();

                // For each Stat add up
                for (int j = 0; j < newSicknessValues.Length; j++)
                {
                    
                    newSicknessValues[j] += ingValues[j];
                }    
                
                Mathf.Clamp(newSicknessValues[i],0,maxSicknessValue);
            }
            // TODO: make sure stats are unique

            newPatient.SetSicknessValues(newSicknessValues);

            newPatients.Add(newPatient);
        }

        return newPatients;
    }

    public void StartInitilizing()
    {
        patientList = InitializePatients();
        patient.SetPatientsList(patientList);
        patient.setPhase(patientPhase.Enter);
        patient.SetTotalNumberOfPatients(patientList.Count);
    }
}
