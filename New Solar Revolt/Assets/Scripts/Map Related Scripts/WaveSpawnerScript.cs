using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawnerScript : MonoBehaviour
{
    public Transform[] enemyPrefab;
    
    public Transform spawnPoint;
    private float timeBetweenWaves = 25f;
    public float countdown = 2f;

    //Size is the number of waves, string is enemies in each wave
    public List<string> waves = new List<string>();
    private int wavesRemaining = 0;
    
    private List<int> waveEnemies = new List<int>();
    //var is used to convert one string into int at a time
    private int stringcounter = 0;

    [SerializeField] private GameObject waveStarter;
    private void Start()
    {
        wavesRemaining = waves.Count;
        
    }
    private void Update()
    {
        if(countdown < 0)
        {
            GetEnemiesInWave();
            SpawnWave();
            countdown = 500;
        }
        countdown -= Time.deltaTime;
    }

    public int WaveEnemiesCount
    {
        get { return waveEnemies.Count; }
    }

    public int WavesRemaining
    {
        get { return wavesRemaining; }
    }


    public List<string> Waves
    {
        get { return waves; }
    }

    private void GetEnemiesInWave()
    {
        if (stringcounter < waves.Count)
        {
            waveEnemies.Clear();
            for (int i = 0; i < waves[stringcounter].Length; i++)
            {
                waveEnemies.Add(waves[stringcounter][i] - '0');
            }
            stringcounter++;
        }
    }

    private void SpawnWave()
    {
        if(wavesRemaining > 0)
        {
            wavesRemaining--;
            StartCoroutine(SpawnEnemy());
        }
    }

    IEnumerator SpawnEnemy()
    {
        waveStarter.SetActive(false);
        for(int i = 0; i < waveEnemies.Count; i++)
        {
            Instantiate(enemyPrefab[waveEnemies[i]], spawnPoint.position, spawnPoint.rotation);
            yield return new WaitForSeconds(1);
        }
        waveEnemies.Clear();
        countdown = timeBetweenWaves;
        yield return new WaitForSeconds(4);
        waveStarter.SetActive(stringcounter < waves.Count);
    }

    public void StartWave()
    {
        GariumAndLivesScript.Garium += (int)Mathf.Ceil(countdown * 5 / 3);
        countdown = 0f;
    }
}
