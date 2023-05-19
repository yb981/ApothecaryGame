using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    Vector3 mousePositionOffset;
    [SerializeField] GameObject snapTarget;


    private Vector3 GetMouseWorldPosition()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    public void OnMouseDown() 
    {
        mousePositionOffset = gameObject.transform.position - GetMouseWorldPosition();
    }

    public void OnMouseDrag() 
    {
        gameObject.transform.position = GetMouseWorldPosition() + mousePositionOffset;
    }

    private void OnMouseUpAsButton() 
    {

        if(snapTarget != null){
            Collider2D otherCollider = snapTarget.GetComponent<Collider2D>();
            if(otherCollider.bounds.Contains(GetMouseWorldPosition() + mousePositionOffset))
            {
                transform.position = snapTarget.transform.position;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    void addValuesToContainer(int c, int b, int f) 
    {
        //GameObject.FindObjectOfType<Container>().setValues(c,b,f);
        //GameObject.FindObjectOfType<Container>().setValues(c,b,f);
        GameObject cont = GameObject.Find("Container");
        cont.GetComponent<ContainerCalculation>().setValues(c,b,f);
        
    }
}
