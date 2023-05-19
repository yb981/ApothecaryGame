using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCalculation : MonoBehaviour
{
    
    int totalCaugh = 0;
    int totalBlood = 0;
    int totalFever = 0;
    
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
}
