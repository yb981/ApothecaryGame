using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientPiece : MonoBehaviour
{

    [SerializeField] GameObject Container;
    private IngredientSO ingredientSO;
    bool isOverContainer = false;


    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0f;
            transform.position = mousePosition;
        }else{
            
            // check if over container
            if(isOverContainer)
            {
                // if over container add to container
                Debug.Log("adding exactly 1 time, by calling addIng now");
                Container.GetComponent<ContainerCalculation>().addIngredient(ingredientSO);
            }

            
            // delete object
            Destroy(this.gameObject);
        }

        Debug.Log(Container.GetComponent<ContainerCalculation>().getListSize());
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("Container"))
        {
            isOverContainer = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if(other.CompareTag("Container"))
        {
            isOverContainer = false;
        }
    }

    public void setIngredientSO(IngredientSO ing)
    {
        ingredientSO = ing;
    }
}
