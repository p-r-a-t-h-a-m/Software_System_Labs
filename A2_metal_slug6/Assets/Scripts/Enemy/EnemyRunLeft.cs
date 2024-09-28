using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRunLeft : MonoBehaviour
{
    [HideInInspector] public float speed; // Speed of the enemy moving left
    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Use FixedUpdate for consistent physics updates
    void FixedUpdate()
    {
        MoveLeft();
    }

    // Moves the enemy left by setting the velocity
    private void MoveLeft()
    {
        rb.velocity = new Vector2(-speed, rb.velocity.y);
    }
}
