using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    private bool isPaused;

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
            isGameOver = true;
            Time.timeScale = 0f;
            
        }
        
        if (waveSpawnerScript.GameWon && !congratsOn)
        {
            congratsOn = true;
            Invoke("ShowCongratsMenu", 1.5f);

            //For unlocking next level
            string sceneName = SceneManager.GetActiveScene().name;
            int level = sceneName[sceneName.Length - 1] - '0';

            if (level < LevelLockScript.unlockedLevels.Length)
            {
                LevelLockScript.unlockedLevels[level] = true;
            }
            
        }
        
    }

    public bool IsPaused
    {
        get { return isPaused; }
        set { isPaused = value; }
    }
    public bool CongratsOn
    {
        get { return congratsOn; }
    }

    public bool IsGameOver
    {
        get { return isGameOver; }
    }

    private void ShowCongratsMenu()
    {
        congratulationsMenu.SetActive(true);
        Time.timeScale = 0f;
    }

}
