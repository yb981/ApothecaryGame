using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class SpriteAtlasImage : MonoBehaviour
{

    [SerializeField] SpriteAtlas mySpriteAtlas;
    [SerializeField] string spriteName;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = mySpriteAtlas.GetSprite(spriteName);
    }

}
