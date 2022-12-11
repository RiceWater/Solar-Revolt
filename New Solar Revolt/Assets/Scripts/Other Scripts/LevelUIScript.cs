using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUIScript : MonoBehaviour
{
    
    [SerializeField] private TextMeshProUGUI livesTextUI;
    [SerializeField] private TextMeshProUGUI gariumTextUI;
    [SerializeField] private GameObject gameOverMenu;
    [SerializeField] private TextMeshProUGUI wavesTextUI;
    [SerializeField] private WaveSpawnerScript waveSpawnerScript;
    private bool isGameOver;

    private void Start()
    {
        isGameOver = false;
        gameOverMenu.SetActive(false);
    }
    private void Update()
    {
        livesTextUI.SetText(GariumAndLivesScript.Lives.ToString());
        gariumTextUI.SetText(GariumAndLivesScript.Garium.ToString());

        int waveCounter = waveSpawnerScript.Waves.Count - waveSpawnerScript.WavesRemaining;
        wavesTextUI.SetText(waveCounter + "/" + waveSpawnerScript.Waves.Count); 

        if(GariumAndLivesScript.Lives <= 0 && !isGameOver)
        {
            gameOverMenu.SetActive(true);
            Time.timeScale = 0f;
            isGameOver = true;
        }
    }

}
