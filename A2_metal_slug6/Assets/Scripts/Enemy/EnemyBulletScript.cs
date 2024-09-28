using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletScript : MonoBehaviour
{
    [SerializeField] private float bulletSpeed = 20f;

    private Transform playerTransform;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Find player and check for null to avoid runtime errors
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;

            // Calculate the direction from bullet to player
            Vector2 direction = (playerTransform.position - transform.position).normalized;

            // Set bullet velocity towards player
            rb.velocity = direction * bulletSpeed;

            // Rotate bullet to face the player direction
            float rotationAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, rotationAngle);
        }
        else
        {
            Debug.LogWarning("Player object not found.");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject); // Destroy the bullet upon collision with the player
        }
    }
}
