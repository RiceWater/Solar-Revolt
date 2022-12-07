using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TowerOptionBehaviorScript : MonoBehaviour
{
    [SerializeField] private List<Transform> towerOptions = new List<Transform>();  //upgrade, change, sell
    private List<Color> origTowerOptionColors = new List<Color>();
    private List<Color> origImageColors = new List<Color>();
    private List<Color> origCostTextColors = new List<Color>();

    private void Start()
    {
        for (int i = 0; i < towerOptions.Count; i++)
        {
            origTowerOptionColors.Add(towerOptions[i].GetComponent<SpriteRenderer>().color);
            origImageColors.Add(towerOptions[i].Find("Canvas").Find("Image").gameObject.GetComponent<Image>().color);
            origCostTextColors.Add(towerOptions[i].Find("Canvas").Find("Text").gameObject.GetComponent<TMP_Text>().color);
        }
    }

    private void Update()
    {
        ColorChangeUpgrade();
        TextChangeSell();
        TextChangeTarget();
    }

    private void ColorChangeUpgrade()
    {
        List<int> upgradeCosts = transform.parent.GetComponent<TurretScript>().GetUpgradeCost();
        int upgradeCounter = transform.parent.GetComponent<TurretScript>().UpgradeCounter;
        

        if (upgradeCounter >= upgradeCosts.Count)
        {
            towerOptions[0].Find("Canvas").Find("Text").gameObject.GetComponent<TMP_Text>().SetText("Max");
            towerOptions[0].GetComponent<SpriteRenderer>().color = origTowerOptionColors[0];
            towerOptions[0].Find("Canvas").Find("Image").gameObject.GetComponent<Image>().color = origImageColors[0];
            towerOptions[0].Find("Canvas").Find("Text").gameObject.GetComponent<TMP_Text>().color = origCostTextColors[0];
            return;
        }

        towerOptions[0].Find("Canvas").Find("Text").gameObject.GetComponent<TMP_Text>().SetText(upgradeCosts[upgradeCounter].ToString());
        if (GariumAndLivesScript.Garium < upgradeCosts[upgradeCounter])
        {
            towerOptions[0].GetComponent<SpriteRenderer>().color = new Color(0.25f, 0.25f, 0.25f, 1);
            towerOptions[0].Find("Canvas").Find("Image").gameObject.GetComponent<Image>().color = new Color(0.25f, 0.25f, 0.25f, 1);
            towerOptions[0].Find("Canvas").Find("Text").gameObject.GetComponent<TMP_Text>().color = new Color(0.5f, 0.5f, 0.5f, 1);
        }
        else
        {
            towerOptions[0].GetComponent<SpriteRenderer>().color = origTowerOptionColors[0];
            towerOptions[0].Find("Canvas").Find("Image").gameObject.GetComponent<Image>().color = origImageColors[0];
            towerOptions[0].Find("Canvas").Find("Text").gameObject.GetComponent<TMP_Text>().color = origCostTextColors[0];
        }
    }

    private void TextChangeSell()
    {
        List<int> upgradeCosts = transform.parent.GetComponent<TurretScript>().GetUpgradeCost();
        int upgradeCounter = transform.parent.GetComponent<TurretScript>().UpgradeCounter;
        int totalGariumSpent = transform.parent.GetComponent<TurretScript>().GariumCost;
        
        for (int i = 0; i < upgradeCounter; i++)
        {
            totalGariumSpent += upgradeCosts[i];
        }
        totalGariumSpent = totalGariumSpent * 3 / 5;
        towerOptions[1].Find("Canvas").Find("Text").gameObject.GetComponent<TMP_Text>().SetText(totalGariumSpent.ToString());
    }

    private void TextChangeTarget()
    {
        //targetType = G : first to goal, S : strongest, W : weakest, R : first in range
        char targetPriority = transform.parent.GetComponent<TurretScript>().TargetPriority;
        towerOptions[2].Find("Canvas").Find("Text").gameObject.GetComponent<TMP_Text>().SetText(DefineTargetPriority(targetPriority));

    }

    private string DefineTargetPriority(char priority)
    {
        switch (priority)
        {
            case 'G':
                return "Close to Base";
            case 'S':
                return "Strongest";
            case 'W':
                return "Weakest";
            case 'R':
                return "In Range";
            default:
                return "Close to Base";
        }
    }

}
