using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUIScript : MonoBehaviour
{
    
    [SerializeField] private TextMeshProUGUI livesTextUI;
    [SerializeField] private TextMeshProUGUI gariumTextUI;
    [SerializeField] private GameObject gameOverMenu;
    [SerializeField] private GameObject congratulationsMenu;
    [SerializeField] private TextMeshProUGUI wavesTextUI;
    [SerializeField] private WaveSpawnerScript waveSpawnerScript;
    private GariumAndLivesScript gariumAndLivesScript;
    private bool isGameOver;
    private bool congratsOn;

    private void Start()
    {
        gariumAndLivesScript = transform.GetComponent<GariumAndLivesScript>();
        isGameOver = false;
        gameOverMenu.SetActive(false);
    }
    private void Update()
    {
        livesTextUI.SetText(gariumAndLivesScript.Lives.ToString());
        gariumTextUI.SetText(gariumAndLivesScript.Garium.ToString());

        int waveCounter = waveSpawnerScript.Waves.Count - waveSpawnerScript.WavesRemaining;
        wavesTextUI.SetText(waveCounter + "/" + waveSpawnerScript.Waves.Count); 

        if(gariumAndLivesScript.Lives <= 0 && !isGameOver)
        {
            gameOverMenu.SetActive(true);
            Time.timeScale = 0f;
            isGameOver = true;
        }

        if (waveSpawnerScript.GameWon && !congratsOn)
        {
            congratulationsMenu.SetActive(true);
            Time.timeScale = 0f;
            congratsOn = true;

        }
    }

}
