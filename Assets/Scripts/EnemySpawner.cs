using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnInterval = 3.0f;
    public float spawnRadius = 10.0f;

    private void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (true) // Infinite loop for continuous spawning
        {
            Vector3 spawnPosition = Random.insideUnitCircle.normalized * spawnRadius;
            GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
            // Attach AI script to the spawned enemy here if not already done.

            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
