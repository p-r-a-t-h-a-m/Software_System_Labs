using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFacePlayer : MonoBehaviour
{
    private GameObject player;
    private SpriteRenderer sr;
    private BoxCollider2D bc;

    private float boxColliderOffsetX = 0.39f;
    private bool isFacingLeft;

    void Start()
    {
        bc = GetComponent<BoxCollider2D>();
        sr = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player");
        isFacingLeft = false; // By default, assuming the enemy faces right
    }

    void Update()
    {
        FacePlayer();
    }

    private void FacePlayer()
    {
        float playerPosX = player.transform.position.x;
        float enemyPosX = transform.position.x;

        // Check if the enemy needs to flip based on player's position
        if (enemyPosX > playerPosX && !isFacingLeft)
        {
            FlipEnemy(true);  // Face left
        }
        else if (enemyPosX < playerPosX && isFacingLeft)
        {
            FlipEnemy(false); // Face right
        }
    }

    private void FlipEnemy(bool faceLeft)
    {
        isFacingLeft = faceLeft;
        sr.flipX = faceLeft;
        // Adjust the collider offset accordingly
        if (faceLeft)
        {
            bc.offset = new Vector2(-boxColliderOffsetX, bc.offset.y);
        }
        else
        {
            bc.offset = new Vector2(boxColliderOffsetX, bc.offset.y);
        }
    }
}
