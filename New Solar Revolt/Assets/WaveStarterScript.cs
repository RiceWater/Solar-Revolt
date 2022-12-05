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
        transform.gameObject.SetActive(false);
        waveSpawner.StartWave();
    }
}
