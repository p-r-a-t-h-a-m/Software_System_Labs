using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeRifle : MonoBehaviour
{
    [SerializeField] private GameObject playerRifle;
    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("collided to rifle1");
        if (collision.gameObject.CompareTag("Player"))
        {
            if (gameObject.name == "Rifle1PowerUp")
            {
                Debug.Log("changed to rifle1");
                playerRifle.GetComponent<RifleRotation>().currentGun = 1;
                player.GetComponent<FireBullet>().currentBullet = 1;

            }
            if (gameObject.name == "Rifle2PowerUp")
            {
                playerRifle.GetComponent<RifleRotation>().currentGun = 2;
                player.GetComponent<FireBullet>().currentBullet = 2;
            }
            Destroy(gameObject);
        }

    }
}
