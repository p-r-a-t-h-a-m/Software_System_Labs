using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float bulletSpeed = 25f;
    [SerializeField] private float damageIntensity = 30f;
    [SerializeField] private float lifeTime = 5f; // Lifetime of the bullet before destruction

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * bulletSpeed;

        // Destroy the bullet after a set time to prevent it from flying indefinitely
        Destroy(gameObject, lifeTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the object hit has the EnemyLife component
        EnemyLife enemy = collision.GetComponent<EnemyLife>();

        // If the object is tagged as "Enemy" and has the EnemyLife component, apply damage
        if (collision.gameObject.CompareTag("Enemy") && enemy != null)
        {
            enemy.damage(damageIntensity);
            Destroy(gameObject);
        }
}
}
