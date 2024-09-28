using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    private Transform playerTransform; // Cache player's Transform to optimize

    // Cache the player's Transform at the start
    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
        else
        {
            Debug.LogWarning("Player object not found. AutoDestroy will not work.");
        }
    }

    // Check distance each frame, destroy the object if beyond 50 units
    void Update()
    {
        if (playerTransform != null)
        {
            float distance = Vector2.Distance(transform.position, playerTransform.position);

            if (distance > 50f)
            {
                Destroy(gameObject);
            }
        }
    }
}
