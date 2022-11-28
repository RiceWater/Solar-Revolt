using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;

    public void PauseGame()
    {
        Debug.Log("GAME PAUSED");
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        Debug.Log("GAME RESUMED");
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void ReturnToMainMenu()
    {
        Debug.Log("BACK TO MAIN MENU");
        //change scene
    }
}
