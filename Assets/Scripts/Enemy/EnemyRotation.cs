using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRotation : MonoBehaviour
{
    private GameObject player;
    private SpriteRenderer sr;

    private Vector3 playerPos;
    Vector3 rotate;
    float rotZ;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        sr = GetComponent<SpriteRenderer>();
    }


    void Update()
    {
        followPlayer();
    }

    private void followPlayer()
    {
        playerPos = player.transform.position;
        rotate = playerPos - transform.position;
        rotZ = Mathf.Atan2(rotate.y, rotate.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, rotZ);
    }

}
