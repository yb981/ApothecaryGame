using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateDublicateOnClick : MonoBehaviour
{
    //[SerializeField] GameObject copy;
    private void OnMouseDown() 
    {
        GameObject dragCopy = Instantiate(gameObject);
        //IngredientDragable copyRaw = new IngredientDragable(1,1,1);
        //GameObject copy = new GameObject("IngredientDragable");
        ///copy.AddComponent<Collider2D>();
        //copy.AddComponent<DragAndDrop>();
        //copy.AddComponent<SpriteRenderer>.sprite = 
        dragCopy.GetComponent<DragAndDrop>().enabled = true;
    }
}
