using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionsScript : MonoBehaviour
{
    public Animator animator;

    private int levelToLoad;

    public void FadeToLevel(int levelIndex)
    {
        Time.timeScale = 1f;
        levelToLoad = levelIndex;
        animator.SetTrigger("FadeOut");
    }

    public void onFadeComplete()
    {
        
        SceneManager.LoadScene(levelToLoad);
    }
}
