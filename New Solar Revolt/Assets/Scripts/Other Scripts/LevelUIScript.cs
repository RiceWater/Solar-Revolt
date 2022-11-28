using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUIScript : MonoBehaviour
{
    
    [SerializeField] private GameObject livesImgUI;
    [SerializeField] private TextMeshProUGUI livesTextUI;
    [SerializeField] private GameObject gariumImgUI;
    [SerializeField] private TextMeshProUGUI gariumTextUI;

    private bool isGameOver;

    private void Start()
    {
        isGameOver = false;
    }
    private void Update()
    {
        livesTextUI.SetText(GariumAndLivesScript.Lives.ToString());
        gariumTextUI.SetText(GariumAndLivesScript.Garium.ToString());
        
        if(GariumAndLivesScript.Lives <= 0 && !isGameOver)
        {
            Debug.Log("GAME OVER");
            isGameOver = true;
        }
    }

}
