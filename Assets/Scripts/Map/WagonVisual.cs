using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WagonVisual : MonoBehaviour
{
    private Player player;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = GetComponentInParent<Player>();
    }

    private void Start()
    {
        player.OnMovementChanged += WagonVisual_OnMovementChanged;
    }

    private void WagonVisual_OnMovementChanged(object sender, Player.OnMovementChangedEventArgs e)
    {
        if (e.movementState == Player.MovementState.moving)
        {
            TurnIntoMovingDirection(e.destination);
        }
        else
        {
            ResetSprite();
        }
    }

    private void TurnIntoMovingDirection(Transform destination)
    {
        if (destination.position.x < spriteRenderer.transform.position.x)
        {
            TurnSpriteLeft();
        }
    }

    private void ResetSprite()
    {
        Vector3 currentScale = spriteRenderer.transform.localScale;
        currentScale.x = Mathf.Abs(currentScale.x);
        spriteRenderer.transform.localScale = currentScale;
    }

    private void TurnSpriteLeft()
    {
        // Turn Left
        Vector3 currentScale = spriteRenderer.transform.localScale;
        currentScale.x *= -1;
        spriteRenderer.transform.localScale = currentScale;

    }
}
