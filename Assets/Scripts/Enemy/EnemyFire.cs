using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFire : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform fireSource;
    private GameObject player;

    private float timer;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    void Update()
    {
        float distance = Vector2.Distance(transform.position, player.transform.position);
        
        if (distance < 20f)
        {
            timer += Time.deltaTime;
            if (timer > 2f)
            {
                timer = 0;
                fire();
            }

        }
    }

    private void fire()
    {
        Instantiate(bullet, fireSource.position, Quaternion.identity);
    }
}
