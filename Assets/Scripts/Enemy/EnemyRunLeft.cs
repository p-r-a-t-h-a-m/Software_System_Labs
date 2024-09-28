using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRunLeft : MonoBehaviour
{


    [HideInInspector] public float speed;

    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        rb.velocity = new Vector2(-speed, rb.velocity.y);
    }
}
