using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeRifle : MonoBehaviour
{
    [SerializeField] private GameObject playerRifle;
    private FireBullet playerFire; // Changed to FireBullet
    private RifleRotation rifleRotation;

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerFire = player.GetComponent<FireBullet>(); // Changed to FireBullet
        rifleRotation = playerRifle.GetComponent<RifleRotation>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;

        switch (gameObject.name)
        {
            case "Rifle1PowerUp":
                ChangeRifleType(1);
                break;
            case "Rifle2PowerUp":
                ChangeRifleType(2);
                break;
        }

        Destroy(gameObject);
    }

    private void ChangeRifleType(int type)
    {
        Debug.Log($"Changed to rifle{type}");
        rifleRotation.currentGun = type;
        playerFire.currentBullet = type;
    }
}
