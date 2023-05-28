using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    Vector3 mousePositionOffset;
    [SerializeField] GameObject snapTarget;
    CreateDublicateOnClick clickScript;

    private void Start()
    {
        clickScript = gameObject.GetComponent<CreateDublicateOnClick>();

        
        if(Input.GetMouseButton(0))
        {
            if(clickScript.isSPawner()) return;
            Debug.Log("trying to do the instant mouseDown");
            OnMouseDown();
            OnMouseDrag();
        }
    }

    private Vector3 GetMouseWorldPosition()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    public void OnMouseDown() 
    {
        if(clickScript == null) clickScript = gameObject.GetComponent<CreateDublicateOnClick>();
        if(clickScript.isSPawner()) return;
        mousePositionOffset = gameObject.transform.position - GetMouseWorldPosition();
    }

    public void OnMouseDrag() 
    {
        if(clickScript == null) clickScript = gameObject.GetComponent<CreateDublicateOnClick>();
        if(clickScript.isSPawner()) return;
        gameObject.transform.position = GetMouseWorldPosition() + mousePositionOffset;
    }

    private void OnMouseUpAsButton() 
    {
        if(clickScript == null) clickScript = gameObject.GetComponent<CreateDublicateOnClick>();
        if(clickScript.isSPawner()) return;

        if(snapTarget != null){
            Collider2D otherCollider = snapTarget.GetComponent<Collider2D>();
            if(otherCollider.bounds.Contains(GetMouseWorldPosition() + mousePositionOffset))
            {
                // Adding Ingredient to Container, if fails delete Object
                //if(!addToContainer()) Destroy(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    void addValuesToContainer(int c, int b, int f) 
    {
        GameObject cont = GameObject.Find("Container");
        cont.GetComponent<ContainerCalculation>().setValues(c,b,f);
    }

    /*   bool addToContainer()
    {
        // Adding Ingredient to Container List, and check if it was successful
        bool successfullyAddedIng = snapTarget.GetComponent<ContainerCalculation>().addIngredient(gameObject);
        if(successfullyAddedIng){
            transform.position = snapTarget.transform.position;
            return true;
        }
        return false;
    }*/
}
