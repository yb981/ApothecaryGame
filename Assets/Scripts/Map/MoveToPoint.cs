using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToPoint : MonoBehaviour
{

    [SerializeField] float speed;
    [SerializeField] Transform objectToMove;
    [SerializeField] Transform goalObject;
    private Player player;
    private Vector3 startPosition;
    private bool moving = true;
    private float amount = 0f;

    private void Start() 
    {
        player = GetComponent<Player>();
        startPosition = objectToMove.position;    
        player.OnMovementChanged += Player_OnMovementChanged;

    }

    void Update()
    {
        if(moving)
        {
            amount += speed * Time.deltaTime;
            float yMove = Mathf.Lerp(startPosition.y,goalObject.position.y, amount);
            float xMove = Mathf.Lerp(startPosition.x,goalObject.position.x, amount);
            Vector3 moveVector = new Vector3(xMove,yMove,0);

            // Move Object
            objectToMove.position = moveVector;
            if(amount >= 1f) {
                // Reset the values
                moving = false;
                amount = 0f;
                startPosition = goalObject.transform.position;
            }
            
        }
    }

    private void Player_OnMovementChanged(object sender, Player.OnMovementChangedEventArgs e)
    {
        goalObject = e.destination;
        if(e.movementState == Player.MovementState.moving)
        {
            moving = true;
        }else{
            moving = false;
        }
    }
}
