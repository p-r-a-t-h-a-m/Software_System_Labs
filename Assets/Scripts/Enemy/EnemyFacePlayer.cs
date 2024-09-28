using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFacePlayer : MonoBehaviour
{
    private GameObject player;
    private SpriteRenderer sr;
    private BoxCollider2D bc;

    private float enemyPosX;
    private float playerPosX;
    private float boxColliderOffsetX = 0.39f;

    void Start()
    {
        bc = GetComponent<BoxCollider2D>();
        sr = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player");
    }


    void Update()
    {
        playerPosX = player.transform.position.x;
        enemyPosX = transform.position.x;
        if (enemyPosX > playerPosX)
        {
            bc.offset = new Vector2(-boxColliderOffsetX, bc.offset.y);
            sr.flipX = true;
        }
        else
        {
            bc.offset = new Vector2(boxColliderOffsetX, bc.offset.y);
            sr.flipX = false;
        }

    }
}
