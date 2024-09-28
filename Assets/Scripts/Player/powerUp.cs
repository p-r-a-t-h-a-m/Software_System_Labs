using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class powerUp : MonoBehaviour
{
    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.CompareTag("Player")) 
        {
            if (gameObject.name == "powerUpBullet1")
            {
                
                player.GetComponent<FireBullet>().BulletShotType = 1;
                
            }
            if (gameObject.name == "powerUpBullet2")
            {

                player.GetComponent<FireBullet>().BulletShotType = 2;

            }
            Destroy(gameObject);
        }
        
    }
}
