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

    private void Start()
    {
        for(int i = 0; i < towerSlotOptions.Count; i++)
        {
            origTowerSlotOptionColors.Add(towerSlotOptions[i].GetComponent<SpriteRenderer>().color);
            origImageColors.Add(towerSlotOptions[i].Find("Canvas").Find("Image").gameObject.GetComponent<Image>().color);
        }

        origCostTextColors.Add(towerSlotOptions[0].Find("Canvas").Find("Tower A Cost").gameObject.GetComponent<TMP_Text>().color);
        origCostTextColors.Add(towerSlotOptions[1].Find("Canvas").Find("Tower B Cost").gameObject.GetComponent<TMP_Text>().color);
        origCostTextColors.Add(towerSlotOptions[2].Find("Canvas").Find("Tower C Cost").gameObject.GetComponent<TMP_Text>().color);
    }
    private void Update()
    {
        ColorChangeTowerA();
        ColorChangeTowerB();
        ColorChangeTowerC();
    }

    private void ColorChangeTowerA()
    {
        
        towerSlotOptions[0].Find("Canvas").Find("Tower A Cost").gameObject.GetComponent<TMP_Text>().SetText(towerPrefabs[0].GetComponent<TurretScript>().GariumCost.ToString());
        if (GariumAndLivesScript.Garium < towerPrefabs[0].GetComponent<TurretScript>().GariumCost)
        {
            towerSlotOptions[0].GetComponent<SpriteRenderer>().color = new Color(0.25f, 0.25f, 0.25f, 1);
            towerSlotOptions[0].Find("Canvas").Find("Image").gameObject.GetComponent<Image>().color = new Color(0.25f, 0.25f, 0.25f, 1);
            towerSlotOptions[0].Find("Canvas").Find("Tower A Cost").gameObject.GetComponent<TMP_Text>().color = new Color(0.5f, 0.5f, 0.5f, 1);
        }
        else
        {
            towerSlotOptions[0].GetComponent<SpriteRenderer>().color = origTowerSlotOptionColors[0];
            towerSlotOptions[0].Find("Canvas").Find("Image").gameObject.GetComponent<Image>().color = origImageColors[0];
            towerSlotOptions[0].Find("Canvas").Find("Tower A Cost").gameObject.GetComponent<TMP_Text>().color = origCostTextColors[0];
        }
    }

    private void ColorChangeTowerB()
    {
        towerSlotOptions[1].Find("Canvas").Find("Tower B Cost").gameObject.GetComponent<TMP_Text>().SetText(towerPrefabs[1].GetComponent<TurretScript>().GariumCost.ToString());
        if (GariumAndLivesScript.Garium < towerPrefabs[1].GetComponent<TurretScript>().GariumCost)
        {
            towerSlotOptions[1].GetComponent<SpriteRenderer>().color = new Color(0.25f, 0.25f, 0.25f, 1);
            towerSlotOptions[1].Find("Canvas").Find("Image").gameObject.GetComponent<Image>().color = new Color(0.25f, 0.25f, 0.25f, 1);
            towerSlotOptions[1].Find("Canvas").Find("Tower B Cost").gameObject.GetComponent<TMP_Text>().color = new Color(0.5f, 0.5f, 0.5f, 1);
        }
        else
        {
            towerSlotOptions[1].GetComponent<SpriteRenderer>().color = origTowerSlotOptionColors[1];
            towerSlotOptions[1].Find("Canvas").Find("Image").gameObject.GetComponent<Image>().color = origImageColors[1];
            towerSlotOptions[1].Find("Canvas").Find("Tower B Cost").gameObject.GetComponent<TMP_Text>().color = origCostTextColors[1];
        }
    }

    private void ColorChangeTowerC()
    {
        towerSlotOptions[2].Find("Canvas").Find("Tower C Cost").gameObject.GetComponent<TMP_Text>().SetText(towerPrefabs[2].GetComponent<TeslaTowerScript>().GariumCost.ToString());
        if (GariumAndLivesScript.Garium < towerPrefabs[2].GetComponent<TeslaTowerScript>().GariumCost)
        {
            towerSlotOptions[2].GetComponent<SpriteRenderer>().color = new Color(0.25f, 0.25f, 0.25f, 1);
            towerSlotOptions[2].Find("Canvas").Find("Image").gameObject.GetComponent<Image>().color = new Color(0.25f, 0.25f, 0.25f, 1);
            towerSlotOptions[2].Find("Canvas").Find("Tower C Cost").gameObject.GetComponent<TMP_Text>().color = new Color(0.5f, 0.5f, 0.5f, 1);
        }
        else
        {
            towerSlotOptions[2].GetComponent<SpriteRenderer>().color = origTowerSlotOptionColors[2];
            towerSlotOptions[2].Find("Canvas").Find("Image").gameObject.GetComponent<Image>().color = origImageColors[2];
            towerSlotOptions[2].Find("Canvas").Find("Tower C Cost").gameObject.GetComponent<TMP_Text>().color = origCostTextColors[2];
        }
    }
}
