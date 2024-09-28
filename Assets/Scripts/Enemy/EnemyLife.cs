using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class EnemyLife : MonoBehaviour
{
    [SerializeField] private float health;
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    internal void damage(float intensity)
    {
        anim.SetTrigger("hurt");

        health -= intensity;

        if (health <= 0)
        {
            anim.SetTrigger("isEnemyDead");
        }
    }

    private void destroyObject()
    {
        // This function is called by event in enemy_die animation

        Destroy(gameObject);
    }
}
