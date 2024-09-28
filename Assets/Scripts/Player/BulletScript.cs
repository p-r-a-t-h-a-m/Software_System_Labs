using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float bulletSpeed = 25f;
    [SerializeField] private float damageIntensity = 30f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * bulletSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyLife enemy = collision.GetComponent<EnemyLife>();
        
        //Debug.Log(collision.gameObject.name);
        if (collision.gameObject.CompareTag("Enemy") )
        {            
            enemy.damage(damageIntensity);
            Destroy(gameObject);
        }
        
    }

}
