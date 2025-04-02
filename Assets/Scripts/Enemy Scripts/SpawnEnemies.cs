using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    public GameObject enemyPrefab;
    public BoxCollider spawnArea;

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
            Vector3 spawnPosition = GetRandomPositionWithinHitbox();
            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        }
    }

    private Vector3 GetRandomPositionWithinHitbox()
    {
        Vector3 center = spawnArea.bounds.center;
        Vector3 size = spawnArea.bounds.extents;

        float randomX = Random.Range(center.x - size.x, center.x + size.x);
        float randomY = center.y;
        float randomZ = Random.Range(center.z - size.z, center.z + size.z);

        return new Vector3(randomX, randomY, randomZ);
    }
}
