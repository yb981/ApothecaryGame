using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCalculation : MonoBehaviour
{
    
    int totalCaugh = 0;
    int totalBlood = 0;
    int totalFever = 0;
    int maxIngredients = 3;
    List<IngredientSO> filledIngredients = new List<IngredientSO>();
    int ingredCountOld = 0;
    
private void Start() {
    Debug.Log("ingredientscount: "+ filledIngredients.Count);
}

private void Update() {
    if(ingredCountOld != filledIngredients.Count)
    {
        Debug.Log("CHANGED NOWWWWWWWWW");
        ingredCountOld = filledIngredients.Count;
    }

}

    // Set & Get
    public void addValues(int c, int b, int f) 
    {
        totalCaugh += c;
        totalBlood += b;
        totalFever += f;
    }

    public void setValues(int c, int b, int f) 
    {
        totalCaugh = c;
        totalBlood = b;
        totalFever = f;
    }

    public int[] getValues()
    {
        return new int[] {totalCaugh, totalBlood, totalFever};
    }

    public bool addIngredient(IngredientSO ingredient)
    {
        Debug.Log("Trying to add ingredient");
        Debug.Log("ingredientscount: "+ filledIngredients.Count);
        for (int i = 0; i < filledIngredients.Count; i++)
        {
            Debug.Log(i+": "+filledIngredients[i]);
        }
        if(filledIngredients.Count < 3)
        {
            filledIngredients.Add(createNewSOObject(ingredient));
            Debug.Log("Succesfully added");
            calculateIngredientValues();
            return true;
        }else{
            Debug.Log("could not add, already full");
            return false;
        }
    }

    private IngredientSO createNewSOObject(IngredientSO old)
    {

        IngredientSO ingredientSO = old;
        return ingredientSO;
    }

    private void calculateIngredientValues()
    {
        for(int i = 0; i < filledIngredients.Count; i++)
        {
            int[] tmp = new int[3];
            tmp = filledIngredients[i].getValues();
            addValues(tmp[0], tmp[1], tmp[2]);
            Debug.Log("calulated ingredient: "+i);
        }
    }

    public int[] mixInsertIngredients()
    {
        calculateIngredientValues();

        // create one potion or something

        // Display

        clearContainer();

        return new int[] {totalCaugh, totalBlood, totalFever};
    }

    private void clearContainer()
    {
        // Clear List
        filledIngredients.Clear();
    }

    public void inPutCombine()
    {
        
        if(GameManager.Instance.getGamePhase() == patientPhase.Wait) {

            if(filledIngredients.Count == 3)
            {
                GameManager.Instance.informPhaseCompleted();
            }else{
                // Tell player to put more ingredients
                Debug.Log("still missing ingredients to combine");
            }
        }else{
            Debug.Log("trying to mix when not in correct gamephase.");
            Debug.Log("phase: "+ GameManager.Instance.getGamePhase());
        }
    }

    public void inPutClear()
    {
        clearContainer();
    }

    public int getListSize()
    {
        return filledIngredients.Count;
    }
}