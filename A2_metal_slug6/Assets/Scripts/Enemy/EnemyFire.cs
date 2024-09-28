using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFire : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform fireSource;
    private GameObject player;
    private float timer;
    
    [SerializeField] private float fireInterval = 2f; // Time between shots
    [SerializeField] private float shootingRange = 20f; // Shooting range

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        float distance = Vector2.Distance(transform.position, player.transform.position);
        
        // Check if player is within range
        if (distance < shootingRange)
        {
            timer += Time.deltaTime;
            if (timer > fireInterval)
            {
                timer = 0;
                FireAtPlayer();
            }
        }
    }

    private void FireAtPlayer()
    {
        Vector2 direction = (player.transform.position - fireSource.position).normalized; // Direction to player
        GameObject firedBullet = Instantiate(bullet, fireSource.position, Quaternion.identity);
        
        // Set the bullet's direction (you should have a script on the bullet to handle movement)
        Rigidbody2D rb = firedBullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = direction * 10f; // Adjust the speed (10f) as necessary
        }
    }
}
