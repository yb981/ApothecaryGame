using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientPiece : MonoBehaviour
{

    GameObject container;
    private IngredientSO ingredientSO;
    bool isOverContainer = false;
    Helper helper;

    void Start()
    {
        container = GameObject.FindGameObjectWithTag("Container");
        helper = GameObject.Find("Helper").GetComponent<Helper>();
        helper.inoformIsHolding(true);
    }

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
                container.GetComponent<ContainerCalculation>().addIngredient(ingredientSO);
            }

            
            // delete object
            helper.inoformIsHolding(false);
            Destroy(this.gameObject);
        }
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
