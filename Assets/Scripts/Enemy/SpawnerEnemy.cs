using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerEnemy : MonoBehaviour
{
    [SerializeField] private GameObject[] enemiesReference;
    [SerializeField] private Transform rightPosition;
    [SerializeField] private int numberOfEnemies = 5;

    private GameObject spawnedEnemy;
    private GameObject player;

    private int randIndex;
    private float playerPosX;
    private float spawnerPosX;
    private bool isSpawned = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        playerPosX = player.transform.position.x;
        spawnerPosX = gameObject.transform.position.x;

        if (!isSpawned && (spawnerPosX - playerPosX < 25f) && (playerPosX < spawnerPosX))
        {
            isSpawned = true;
            StartCoroutine(SpawnEnemy());
        }
    }
    IEnumerator SpawnEnemy()
    {
        
        while (numberOfEnemies-- > 0)
        {
            yield return new WaitForSeconds(Random.Range(1, 5));
            randIndex = Random.Range(0, enemiesReference.Length);
           
            spawnedEnemy = Instantiate(enemiesReference[randIndex]);

            spawnedEnemy.transform.position = rightPosition.position;
            spawnedEnemy.GetComponent<EnemyRunLeft>().speed = Random.Range(4, 10);
            spawnedEnemy.transform.localScale = new Vector3(spawnedEnemy.transform.localScale.x, spawnedEnemy.transform.localScale.y, spawnedEnemy.transform.localScale.z);

        }

    }
}
