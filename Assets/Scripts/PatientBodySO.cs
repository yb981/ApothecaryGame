using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatientBodySO : ScriptableObject
{
    private Sprite head;
    private Sprite body;

    public void SetNewSprites(Sprite newHead, Sprite newBody)
    {
        head = newHead;
        body = newBody;
    }

    public Sprite GetBody()
    {
        return body;
    }

    public Sprite GetHead()
    {
        return head;
    }

}
