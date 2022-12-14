using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;

    private void Update()
    {
        if (pauseMenu.activeSelf)
        {
            GameObject.Find("Game Manager").GetComponent<LevelUIScript>().IsPaused = true;
        }
        else
        {
            GameObject.Find("Game Manager").GetComponent<LevelUIScript>().IsPaused = false;
        }
    }
    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }
}
