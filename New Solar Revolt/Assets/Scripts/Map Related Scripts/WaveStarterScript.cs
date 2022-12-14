using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveStarterScript : MonoBehaviour
{
    [SerializeField] private WaveSpawnerScript waveSpawner;
    private void Start()
    {
        transform.gameObject.SetActive(true);
    }

    private void OnMouseUpAsButton()
    {
        if (GameObject.Find("Game Manager").GetComponent<LevelUIScript>().IsGameOver || GameObject.Find("Game Manager").GetComponent<LevelUIScript>().CongratsOn || GameObject.Find("Game Manager").GetComponent<LevelUIScript>().IsPaused)
        {
            return;
        }
        transform.gameObject.SetActive(false);
        waveSpawner.StartWave();
    }
}
