using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelLockScript : MonoBehaviour
{
    public static bool[] unlockedLevels = { true, false, false, false };

    private Image imageComponent;
    private Button buttonComponent;

    private void Start()
    {
        imageComponent = transform.gameObject.GetComponent<Image>();
        buttonComponent = transform.gameObject.GetComponent<Button>();
    }
    private void Update()
    {
        string levelName = transform.gameObject.name;
        int lastChar = levelName[levelName.Length - 1] - '0';
        if (unlockedLevels[lastChar - 1])
        {
            imageComponent.color = Color.white;
            buttonComponent.enabled = true;
        }
        else
        {
            imageComponent.color = Color.red;
            buttonComponent.enabled = false;
        }
    }

}
