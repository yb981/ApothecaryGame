using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCalculation : MonoBehaviour
{
    
    int totalCaugh = 0;
    int totalBlood = 0;
    int totalFever = 0;
    int maxIngredients = 3;
    List<GameObject> filledIngredients = new List<GameObject>();
    
    // Setter Getter
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

    public bool addIngredient(GameObject ingredient)
    {
        Debug.Log("Trying to add ingredient");
        if(filledIngredients.Count < 3)
        {
            filledIngredients.Add(ingredient);
            Debug.Log("Succesfully added");
            calculateIngredientValues();
            return true;
        }else{
            Debug.Log("could not add, already full");
            return false;
        }
    }

    private void calculateIngredientValues()
    {
        for(int i = 0; i < filledIngredients.Count; i++)
        {
            int[] tmp = new int[3];
            tmp = filledIngredients[i].GetComponent<CreateDublicateOnClick>().getIngredientValues();
            addValues(tmp[0], tmp[1], tmp[2]);
            Debug.Log("calulated ingredient: "+i);
        }
    }
}
