using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRotation : MonoBehaviour
{
    private Transform playerTransform;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player")?.transform;
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (playerTransform == null)
        {
            Debug.LogWarning("Player object not found.");
        }
    }

    void Update()
    {
        if (playerTransform != null)
        {
            RotateTowardsPlayer();
            FlipSprite();
        }
    }

    // Rotates the enemy to face the player
    private void RotateTowardsPlayer()
    {
        Vector3 direction = playerTransform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Set the rotation on the z-axis to make the enemy face the player
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    // Flips the sprite depending on the player’s relative position (left or right)
    private void FlipSprite()
    {
        if (playerTransform.position.x < transform.position.x)
        {
            // If the player is on the left, flip the sprite
            spriteRenderer.flipX = true;
        }
        else
        {
            // If the player is on the right, reset the flip
            spriteRenderer.flipX = false;
        }
    }
}
