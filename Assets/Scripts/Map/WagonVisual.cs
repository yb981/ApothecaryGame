using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WagonVisual : MonoBehaviour
{
    [SerializeField] private Sprite standingWagon;
    [SerializeField] private Sprite movingWagon;

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
        spriteRenderer.sprite = standingWagon;
    }

    private void WagonVisual_OnMovementChanged(object sender, Player.OnMovementChangedEventArgs e)
    {
        UpdateSprite(e.movementState, e.destination);
    }

    private void UpdateSprite(Player.MovementState movementState, Transform destination)
    {
        if (movementState == Player.MovementState.moving)
        {
            TurnIntoMovingDirection(destination);
            SetSprite(movingWagon);
        }
        else
        {
            ResetSpriteDirection();
            SetSprite(standingWagon);
        }
    }

    private void TurnIntoMovingDirection(Transform destination)
    {
        if (destination.position.x < spriteRenderer.transform.position.x)
        {
            TurnSpriteLeft();
        }
    }

    private void ResetSpriteDirection()
    {
        Vector3 currentScale = spriteRenderer.transform.localScale;
        currentScale.x = Mathf.Abs(currentScale.x);
        spriteRenderer.transform.localScale = currentScale;
        
    }

    private void TurnSpriteLeft()
    {
        Vector3 currentScale = spriteRenderer.transform.localScale;
        currentScale.x *= -1;
        spriteRenderer.transform.localScale = currentScale;
    }

    private void SetSprite(Sprite sprite)
    {
        spriteRenderer.sprite = sprite;
    }
}
