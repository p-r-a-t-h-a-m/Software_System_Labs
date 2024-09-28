using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombTrap : MonoBehaviour
{
    private Animator animator;
    private bool hasExploded = false;

    void Start()
    {
        // Get the Animator component attached to the bomb
        animator = GetComponent<Animator>();
    }

    // This method is called when another object enters the bomb's collider
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object that collided with the bomb has the Player tag and if the bomb hasn't exploded yet
        if (other.CompareTag("Player") && !hasExploded)
        {
            // Set the explode trigger in the Animator to play the explosion animation
            animator.SetTrigger("Explode");
            hasExploded = true;

            // Optionally, you can destroy the bomb object after the animation finishes
            Destroy(gameObject, 2f); // Adjust the time based on the length of the animation
        }
    }
}
