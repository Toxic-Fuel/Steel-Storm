using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    public GameObject enemyPrefab;

    [SerializeField]
    private float spawnRange = 5f;

    [SerializeField]
    private int numberOfEnemiesToSpawn = 5;

    private void Start()
    {
        SpawnEnemiesInHitbox();
    }

    private void SpawnEnemiesInHitbox()
    {
        for (int i = 0; i < numberOfEnemiesToSpawn; i++)
        {
            Vector3 spawnPosition = new Vector3(
                Random.Range(-spawnRange, spawnRange),
                0,
                Random.Range(-spawnRange, spawnRange)
            );

            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        }
    }
}
