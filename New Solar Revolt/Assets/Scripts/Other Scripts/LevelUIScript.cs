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

    private void Update()
    {
        livesTextUI.SetText(GariumScript.Lives.ToString());
        gariumTextUI.SetText(GariumScript.Garium.ToString());
    }
}
