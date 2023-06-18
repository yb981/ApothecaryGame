using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientPiece : MonoBehaviour
{

    GameObject container;
    private IngredientSO ingredientSO;
    bool isOverContainer = false;
    Helper helper;
    int[] assumedValues;

    void Start()
    {
        container = GameObject.FindGameObjectWithTag("Container");
        helper = GameObject.Find("Helper").GetComponent<Helper>();
        helper.InformIsHolding(true);
    }

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
                container.GetComponent<ContainerCalculation>().addIngredient(ingredientSO, assumedValues);
            }

            
            // delete object
            helper.InformIsHolding(false);
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

    public void setAssumedValues(int[] values)
    {
        
        assumedValues = new int[values.Length];
        assumedValues = values;
    }
}
