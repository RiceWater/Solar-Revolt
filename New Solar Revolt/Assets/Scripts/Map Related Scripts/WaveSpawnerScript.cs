using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawnerScript : MonoBehaviour
{
    public Transform[] enemyPrefab;
    public int totalWaves = 3;
    public Transform spawnPoint;
    private float timeBetweenWaves = 20f;
    public float countdown = 2f;

    private void Update()
    {
        if(countdown < 0)
        {
            SpawnWave();
            countdown = timeBetweenWaves;
        }
        countdown -= Time.deltaTime;
    }

    private void SpawnWave()
    {
        if(totalWaves > 0)
        {
            totalWaves--;
            StartCoroutine(TestSpawn());
        }
    }

    private void SpawnEnemy()
    {
        Instantiate(enemyPrefab[0], spawnPoint.position, spawnPoint.rotation);
    }

    IEnumerator TestSpawn()
    {
        int numberOfEnemies = Random.Range(1, 10);
        for (int i = 0; i < numberOfEnemies; i++)
        {
            yield return new WaitForSeconds(1);
            SpawnEnemy();
        }
        
    }
}
