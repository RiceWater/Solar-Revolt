using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TowerSlotOptionBehaviorScript : MonoBehaviour
{
    [SerializeField] private List<Transform> towerSlotOptions = new List<Transform>();
    [SerializeField] private List<Transform> towerPrefabs = new List<Transform>();
    private List<Color> origTowerSlotOptionColors = new List<Color>();
    private List<Color> origImageColors = new List<Color>();
    private List<Color> origCostTextColors = new List<Color>();
    private Color disabledColor = new Color(0.25f, 0.25f, 0.25f, 1);
    private Color disabledColorText = new Color(0.5f, 0.5f, 0.5f, 1);
    private GariumAndLivesScript gariumAndLivesScript;
    private void Start()
    {
        gariumAndLivesScript = GameObject.Find("Game Manager").GetComponent<GariumAndLivesScript>();
        for (int i = 0; i < towerSlotOptions.Count; i++)
        {
            origTowerSlotOptionColors.Add(towerSlotOptions[i].GetComponent<SpriteRenderer>().color);
            origImageColors.Add(towerSlotOptions[i].Find("Canvas").Find("Image").gameObject.GetComponent<Image>().color);
            origCostTextColors.Add(towerSlotOptions[0].Find("Canvas").Find("Text").gameObject.GetComponent<TMP_Text>().color);
        }
    }
    private void Update()
    {
        ColorChangeForTurretScript(0);
        ColorChangeForTurretScript(1);
        ColorChangeForTeslaScript();
    }
    private void ColorChangeForTurretScript(int index)
    {
        towerSlotOptions[index].Find("Canvas").Find("Text").gameObject.GetComponent<TMP_Text>().SetText(towerPrefabs[index].GetComponent<TurretScript>().GariumCost.ToString());
        if (gariumAndLivesScript.Garium < towerPrefabs[index].GetComponent<TurretScript>().GariumCost)
        {
            towerSlotOptions[index].GetComponent<SpriteRenderer>().color = disabledColor;
            towerSlotOptions[index].Find("Canvas").Find("Image").gameObject.GetComponent<Image>().color = disabledColor;
            towerSlotOptions[index].Find("Canvas").Find("Text").gameObject.GetComponent<TMP_Text>().color = disabledColorText;
        }
        else
        {
            towerSlotOptions[index].GetComponent<SpriteRenderer>().color = origTowerSlotOptionColors[index];
            towerSlotOptions[index].Find("Canvas").Find("Image").gameObject.GetComponent<Image>().color = origImageColors[index];
            towerSlotOptions[index].Find("Canvas").Find("Text").gameObject.GetComponent<TMP_Text>().color = origCostTextColors[index];
        }
    }

    private void ColorChangeForTeslaScript()
    {
        towerSlotOptions[2].Find("Canvas").Find("Text").gameObject.GetComponent<TMP_Text>().SetText(towerPrefabs[2].GetComponent<TeslaTowerScript>().GariumCost.ToString());
        if (gariumAndLivesScript.Garium < towerPrefabs[2].GetComponent<TeslaTowerScript>().GariumCost)
        {
            towerSlotOptions[2].GetComponent<SpriteRenderer>().color = disabledColor;
            towerSlotOptions[2].Find("Canvas").Find("Image").gameObject.GetComponent<Image>().color = disabledColor;
            towerSlotOptions[2].Find("Canvas").Find("Text").gameObject.GetComponent<TMP_Text>().color = disabledColorText;
        }
        else
        {
            towerSlotOptions[2].GetComponent<SpriteRenderer>().color = origTowerSlotOptionColors[2];
            towerSlotOptions[2].Find("Canvas").Find("Image").gameObject.GetComponent<Image>().color = origImageColors[2];
            towerSlotOptions[2].Find("Canvas").Find("Text").gameObject.GetComponent<TMP_Text>().color = origCostTextColors[2];
        }
    }
}
