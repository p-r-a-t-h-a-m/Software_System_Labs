using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBullet : MonoBehaviour
{
    [SerializeField] private Transform fireSource;       // Source of bullet fire
    [SerializeField] private GameObject[] bullet;        // Different types of bullets
    [SerializeField] private float fireRate = 0.1f;      // Fire rate for continuous fire

    internal int currentBullet = 0;                      // Index of the current bullet type
    internal int BulletShotType = 0;                     // 0: single, 1: spread, 2: burst
    private float ShotOffset = 0.5f;                     // Offset for spread/burst shots
    private float nextFireTime = 0f;                     // Controls when to fire next

    void Update()
    {
        // Fire single or spread bullets on left mouse click ("Fire1")
        if (Input.GetButtonDown("Fire1"))
        {
            FireBulletPattern();
        }

        // Fire continuous bullets on right mouse hold ("Fire2")
        if (Input.GetButton("Fire2") && Time.time >= nextFireTime)
        {
            FireBulletPattern();
            nextFireTime = Time.time + fireRate;         // Ensure delay for continuous firing
        }
    }

    // Fire the appropriate bullet pattern based on the BulletShotType
    private void FireBulletPattern()
    {
        // Always fire one bullet at the fireSource position
        Instantiate(bullet[currentBullet], fireSource.position, fireSource.rotation);

        // Spread shot (3 bullets)
        if (BulletShotType == 1)
        {
            FireSpread();
        }

        // Burst mode (3 bullets, spread horizontally)
        if (BulletShotType == 2)
        {
            FireBurst();
        }
    }

    // Spread shot (additional bullets above and below the source)
    private void FireSpread()
    {
        Instantiate(bullet[currentBullet], new Vector2(fireSource.position.x, fireSource.position.y + ShotOffset), fireSource.rotation);
        Instantiate(bullet[currentBullet], new Vector2(fireSource.position.x, fireSource.position.y - ShotOffset), fireSource.rotation);
    }

    // Burst shot (additional bullets horizontally offset from the source)
    private void FireBurst()
    {
        for (int i = 1; i <= 2; i++)
        {
            float offsetX = i * ShotOffset;
            Instantiate(bullet[currentBullet], new Vector2(fireSource.position.x + offsetX, fireSource.position.y), fireSource.rotation);
        }
    }
}
