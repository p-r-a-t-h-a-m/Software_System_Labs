using System.Collections;
using UnityEngine;

public class SpawnerEnemy : MonoBehaviour
{
    [SerializeField] private GameObject[] enemiesReference;  // Array of enemy prefabs
    [SerializeField] private Transform rightPosition;        // Position where enemies will spawn
    [SerializeField] private int numberOfEnemies = 5;        // Number of enemies to spawn
    [SerializeField] private float minSpawnDelay = 1f;       // Minimum time between spawns
    [SerializeField] private float maxSpawnDelay = 5f;       // Maximum time between spawns
    [SerializeField] private float spawnDistance = 25f;      // Distance from player to start spawning

    private GameObject player;
    private bool isSpawned = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        float playerPosX = player.transform.position.x;
        float spawnerPosX = transform.position.x;

        // Start spawning if player is close enough and hasn't passed the spawner
        if (!isSpawned && (spawnerPosX - playerPosX < spawnDistance) && (playerPosX < spawnerPosX))
        {
            isSpawned = true;
            StartCoroutine(SpawnEnemies());
        }
    }

    private IEnumerator SpawnEnemies()
    {
        for (int i = 0; i < numberOfEnemies; i++)
        {
            // Random delay between spawns
            yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));

            // Spawn random enemy
            int randIndex = Random.Range(0, enemiesReference.Length);
            GameObject spawnedEnemy = Instantiate(enemiesReference[randIndex], rightPosition.position, Quaternion.identity);

            // Set random speed and scale
            spawnedEnemy.GetComponent<EnemyRunLeft>().speed = Random.Range(4, 10);
            spawnedEnemy.transform.localScale = new Vector3(spawnedEnemy.transform.localScale.x, spawnedEnemy.transform.localScale.y, spawnedEnemy.transform.localScale.z);
        }

        // Optional: Disable the spawner after all enemies have been spawned
        gameObject.SetActive(false);
    }
}
