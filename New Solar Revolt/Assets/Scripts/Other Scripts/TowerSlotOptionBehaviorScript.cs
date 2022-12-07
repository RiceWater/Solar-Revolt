using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TowerSlotOptionBehaviorScript : MonoBehaviour
{
    [SerializeField] private List<Transform> towerOptions = new List<Transform>();
    [SerializeField] private List<Transform> towerPrefabs = new List<Transform>();
    private List<Color> origTowerOptionColors = new List<Color>();
    private List<Color> origImageColors = new List<Color>();
    private List<Color> origCostTextColors = new List<Color>();

    private void Start()
    {
        for(int i = 0; i < towerOptions.Count; i++)
        {
            origTowerOptionColors.Add(towerOptions[i].GetComponent<SpriteRenderer>().color);
            origImageColors.Add(towerOptions[i].Find("Canvas").Find("Image").gameObject.GetComponent<Image>().color);
        }

        origCostTextColors.Add(towerOptions[0].Find("Canvas").Find("Tower A Cost").gameObject.GetComponent<TMP_Text>().color);
        origCostTextColors.Add(towerOptions[1].Find("Canvas").Find("Tower B Cost").gameObject.GetComponent<TMP_Text>().color);
        origCostTextColors.Add(towerOptions[2].Find("Canvas").Find("Tower C Cost").gameObject.GetComponent<TMP_Text>().color);
    }
    private void Update()
    {
        ColorChangeTowerA();
        ColorChangeTowerB();
        ColorChangeTowerC();
    }

    private void ColorChangeTowerA()
    {
        if (GariumAndLivesScript.Garium < towerPrefabs[0].GetComponent<TurretScript>().GariumCost)
        {
            towerOptions[0].GetComponent<SpriteRenderer>().color = new Color(0.25f, 0.25f, 0.25f, 1);
            towerOptions[0].Find("Canvas").Find("Image").gameObject.GetComponent<Image>().color = new Color(0.25f, 0.25f, 0.25f, 1);
            towerOptions[0].Find("Canvas").Find("Tower A Cost").gameObject.GetComponent<TMP_Text>().color = new Color(0.5f, 0.5f, 0.5f, 1);
        }
        else
        {
            towerOptions[0].GetComponent<SpriteRenderer>().color = origTowerOptionColors[0];
            towerOptions[0].Find("Canvas").Find("Image").gameObject.GetComponent<Image>().color = origImageColors[0];
            towerOptions[0].Find("Canvas").Find("Tower A Cost").gameObject.GetComponent<TMP_Text>().color = origCostTextColors[0];
        }
    }

    private void ColorChangeTowerB()
    {
        if (GariumAndLivesScript.Garium < towerPrefabs[1].GetComponent<TurretScript>().GariumCost)
        {
            towerOptions[1].GetComponent<SpriteRenderer>().color = new Color(0.25f, 0.25f, 0.25f, 1);
            towerOptions[1].Find("Canvas").Find("Image").gameObject.GetComponent<Image>().color = new Color(0.25f, 0.25f, 0.25f, 1);
            towerOptions[1].Find("Canvas").Find("Tower B Cost").gameObject.GetComponent<TMP_Text>().color = new Color(0.5f, 0.5f, 0.5f, 1);
        }
        else
        {
            towerOptions[1].GetComponent<SpriteRenderer>().color = origTowerOptionColors[1];
            towerOptions[1].Find("Canvas").Find("Image").gameObject.GetComponent<Image>().color = origImageColors[1];
            towerOptions[1].Find("Canvas").Find("Tower B Cost").gameObject.GetComponent<TMP_Text>().color = origCostTextColors[1];
        }
    }

    private void ColorChangeTowerC()
    {
        if (GariumAndLivesScript.Garium < towerPrefabs[2].GetComponent<TeslaTowerScript>().GariumCost)
        {
            towerOptions[2].GetComponent<SpriteRenderer>().color = new Color(0.25f, 0.25f, 0.25f, 1);
            towerOptions[2].Find("Canvas").Find("Image").gameObject.GetComponent<Image>().color = new Color(0.25f, 0.25f, 0.25f, 1);
            towerOptions[2].Find("Canvas").Find("Tower C Cost").gameObject.GetComponent<TMP_Text>().color = new Color(0.5f, 0.5f, 0.5f, 1);
        }
        else
        {
            towerOptions[2].GetComponent<SpriteRenderer>().color = origTowerOptionColors[2];
            towerOptions[2].Find("Canvas").Find("Image").gameObject.GetComponent<Image>().color = origImageColors[2];
            towerOptions[2].Find("Canvas").Find("Tower C Cost").gameObject.GetComponent<TMP_Text>().color = origCostTextColors[2];
        }
    }
}
