using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class EnemyLife : MonoBehaviour
{
    [SerializeField] private float health;
    private Animator anim;
    private bool isDead = false;  // Track if the enemy is dead

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Function to apply damage to the enemy
    internal void damage(float intensity)
    {
        if (isDead) return; // Prevent damage if already dead

        // Check if the "hurt" animation exists in the Animator's current controller
        if (anim.HasState(0, Animator.StringToHash("kraken_hurt")))
        {
            anim.SetTrigger("hurt");
        }

        health -= intensity;

        if (health <= 0 && !isDead)
        {
            isDead = true;
            anim.SetTrigger("isEnemyDead");
            GetComponent<EnemyFire>().enabled = false;  // Disable shooting
            //destroyObject();
        }
    }

    // Function to handle the enemy destruction
    private void destroyObject()
    {
        
        Destroy(gameObject);  // Destroy the enemy object after the delay
    }
}
