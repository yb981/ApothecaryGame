using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToPoint : MonoBehaviour
{
    public event Action OnDestinationReached;

    [SerializeField] float speed;
    [SerializeField] Transform objectToMove;
    [SerializeField] Transform goalObject;
    private Player player;
    private Vector3 startPosition;
    private bool moving = false;
    private float amount = 0f;

    private void Start()
    {
        player = GetComponent<Player>();
        startPosition = objectToMove.position;
        player.OnMovementChanged += Player_OnMovementChanged;

    }

    void Update()
    {
        if (moving)
        {
            Move();
        }
    }

    private void Player_OnMovementChanged(object sender, Player.OnMovementChangedEventArgs e)
    {
        SetDestinationAndStartPositions(e.destination);
        if (e.movementState == Player.MovementState.moving)
        {
            moving = true;
        }
        else
        {
            moving = false;
        }
    }

    private void SetDestinationAndStartPositions(Transform destination)
    {
        goalObject = destination;
        startPosition = objectToMove.position;

    }

    private void Move()
    {
        amount += speed * Time.deltaTime;
        float yMove = Mathf.Lerp(startPosition.y, goalObject.position.y, amount);
        float xMove = Mathf.Lerp(startPosition.x, goalObject.position.x, amount);
        Vector3 moveVector = new Vector3(xMove, yMove, 0);

        // Move Object
        objectToMove.position = moveVector;
        if (amount >= 1f)
        {
            // Reset the values
            moving = false;
            amount = 0f;
            startPosition = goalObject.transform.position;
            OnDestinationReached?.Invoke();
        }
    }
}
