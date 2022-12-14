using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class TowerOptionsScript : MonoBehaviour
{
    [SerializeField] private GameObject towerRangeDisplayPF;
    private RaycastHit2D[] rc;
    private Transform towerRangeDisplayTransform;

    private char[] towerTargetOptions = { 'G', 'R', 'S', 'W' };
    private int towerTargetOptionsIndex;
    private GariumAndLivesScript gariumAndLivesScript;
    private void Start()
    {
        gariumAndLivesScript = GameObject.Find("Game Manager").GetComponent<GariumAndLivesScript>();
        Color spriteColor = towerRangeDisplayPF.GetComponent<SpriteRenderer>().material.color;
        towerRangeDisplayPF.GetComponent<SpriteRenderer>().material.color = new Color(spriteColor.r, spriteColor.g, spriteColor.b, 0.5f);
        towerRangeDisplayPF.SetActive(false);
        towerTargetOptionsIndex = 0;
    }

    private void Update()
    {
        Vector3 towerRadius = new Vector3(transform.GetComponent<CircleCollider2D>().radius, transform.GetComponent<CircleCollider2D>().radius, 1);
        towerRangeDisplayPF.transform.localScale = towerRadius * 2;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Physics2D.IgnoreCollision(transform.GetComponent<BoxCollider2D>(), collision);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Physics2D.IgnoreCollision(transform.GetComponent<BoxCollider2D>(), collision.collider);
    }

    private void OnMouseUpAsButton()
    {
        if(GameObject.Find("Game Manager").GetComponent<LevelUIScript>().IsGameOver || GameObject.Find("Game Manager").GetComponent<LevelUIScript>().CongratsOn || GameObject.Find("Game Manager").GetComponent<LevelUIScript>().IsPaused)
        {
            return;
        }
        rc = MouseRayCastScript.rc;
        if (rc.Length > 1)
        {
            for (int i = 0; i < rc.Length; i++)
            {
                if (rc[i].collider is BoxCollider2D && rc[i].transform.CompareTag("Tower"))
                {
                    Transform boxColliderTransform = rc[i].collider.transform;
                    //Traverse from the parent to the needed child object
                    for (int j = 0; j < boxColliderTransform.childCount; j++)
                    {
                        //Ensures that tower under mouse will be interacted and not some other tower 
                        //whose collider overlaps with the tower
                        if (boxColliderTransform.GetChild(j).name.Equals("Tower Range Display"))
                        {
                            towerRangeDisplayTransform = boxColliderTransform.GetChild(j);
                        }
                        //technically, the mouse still clicks the tower, which closes/opens the tower option of the unintended tower
                        else if (boxColliderTransform.GetChild(j).name.Equals("TowerOptions"))
                        {
                            
                            Transform towerOptionsTransform= boxColliderTransform.GetChild(j);
                            towerOptionsTransform.gameObject.SetActive(!towerOptionsTransform.gameObject.activeSelf);
                            towerRangeDisplayTransform.gameObject.SetActive(!towerRangeDisplayTransform.gameObject.activeSelf);
                        }
                    }
                }

                if (rc[i].collider.transform.CompareTag("Upgrade"))
                {
                    UpgradeTower(rc[i]);
                    CloseTowerOptionsAndRangeDisplay(rc[i]);
                }
                else if (rc[i].collider.transform.CompareTag("Sell"))
                {
                    SellTower(rc[i]);
                }
                else if (rc[i].collider.transform.CompareTag("Switch Priority")) {
                    SwitchPriority();
                    CloseTowerOptionsAndRangeDisplay(rc[i]);
                }
                
            }
        }
    } 

    private void UpgradeTower(RaycastHit2D rc)
    {
        CircleCollider2D towerRadius = rc.collider.transform.root.GetComponent<CircleCollider2D>();
        int upgradeCounter = 0;
        List<int> upgradeCosts = new List<int>();
        if (towerRadius.transform.GetComponent<TurretScript>() != null)
        {
            upgradeCosts = towerRadius.transform.GetComponent<TurretScript>().GetUpgradeCost();
            upgradeCounter = towerRadius.transform.GetComponent<TurretScript>().UpgradeCounter;
            if (upgradeCounter < upgradeCosts.Count && upgradeCosts[upgradeCounter] <= gariumAndLivesScript.Garium)
            {
                if (towerRadius.gameObject.name.Contains("BB-75"))
                {
                    //range (+10%)
                    towerRadius.radius = towerRadius.radius * 11 / 10;
                    //damage (+30%)
                    towerRadius.transform.GetComponent<TurretScript>().BulletDamage =
                        towerRadius.transform.GetComponent<TurretScript>().BulletDamage * 1.3f;
                    //firerate (+20%)
                    towerRadius.transform.GetComponent<TurretScript>().FireRate =
                        towerRadius.transform.GetComponent<TurretScript>().FireRate * 6 / 5;
                }
                else if (towerRadius.gameObject.name.Contains("RF-30"))
                {
                    //range (+10%)
                    towerRadius.radius = towerRadius.radius * 11 / 10;
                    //damage (+20%)
                    towerRadius.transform.GetComponent<TurretScript>().BulletDamage =
                        towerRadius.transform.GetComponent<TurretScript>().BulletDamage * 6 / 5;
                    //firerate (+40%)
                    towerRadius.transform.GetComponent<TurretScript>().FireRate =
                        towerRadius.transform.GetComponent<TurretScript>().FireRate * 7 / 5;
                }
                //counter and money
                gariumAndLivesScript.Garium -= upgradeCosts[upgradeCounter];
                towerRadius.transform.GetComponent<TurretScript>().UpgradeCounter++;
            }
        }
        else if (towerRadius.transform.GetComponent<TeslaTowerScript>() != null)
        {
            upgradeCosts = towerRadius.transform.GetComponent<TeslaTowerScript>().GetUpgradeCost();
            upgradeCounter = towerRadius.transform.GetComponent<TeslaTowerScript>().UpgradeCounter;
            if (upgradeCounter < upgradeCosts.Count && upgradeCosts[upgradeCounter] <= gariumAndLivesScript.Garium)
            {
                if (towerRadius.gameObject.name.Contains("XM-T50"))
                {
                    //range (+ 20%)
                    towerRadius.radius = towerRadius.radius * 6 / 5;
                    //damage (+20%)
                    towerRadius.transform.GetComponent<TeslaTowerScript>().BulletDamage =
                        towerRadius.transform.GetComponent<TeslaTowerScript>().BulletDamage * 6 / 5;
                    //firerate (+20%)
                    towerRadius.transform.GetComponent<TeslaTowerScript>().FireRate =
                        towerRadius.transform.GetComponent<TeslaTowerScript>().FireRate * 6 / 5;
                }
            }
            //counter and money
            gariumAndLivesScript.Garium -= upgradeCosts[upgradeCounter];
            towerRadius.transform.GetComponent<TeslaTowerScript>().UpgradeCounter++;
        }
    }

    private void SellTower(RaycastHit2D rc)
    {
        CircleCollider2D towerRadius = rc.collider.transform.root.GetComponent<CircleCollider2D>();
        if (towerRadius.transform.GetComponent<TurretScript>() != null)
        {
            List<int> upgradeCosts = new List<int>();
            upgradeCosts = towerRadius.transform.GetComponent<TurretScript>().GetUpgradeCost();
            int upgradeCounter = towerRadius.transform.GetComponent<TurretScript>().UpgradeCounter;
            int totalGariumSpent = towerRadius.transform.GetComponent<TurretScript>().GariumCost;
            for (int i = 0; i < upgradeCounter; i++)
            {
                totalGariumSpent += upgradeCosts[i];
            }

            gariumAndLivesScript.Garium += (totalGariumSpent  * 3 / 5);
        }
        else if (towerRadius.transform.GetComponent<TeslaTowerScript>() != null)
        {
            List<int> upgradeCosts = new List<int>();
            upgradeCosts = towerRadius.transform.GetComponent<TeslaTowerScript>().GetUpgradeCost();
            int upgradeCounter = towerRadius.transform.GetComponent<TeslaTowerScript>().UpgradeCounter;
            int totalGariumSpent = towerRadius.transform.GetComponent<TeslaTowerScript>().GariumCost;
            for (int i = 0; i < upgradeCounter; i++)
            {
                totalGariumSpent += upgradeCosts[i];
            }

            gariumAndLivesScript.Garium += (totalGariumSpent * 3 / 5);
        }
        Destroy(transform.root.gameObject);
    }

    private void SwitchPriority()
    {
        towerTargetOptionsIndex++;
        if(towerTargetOptionsIndex >= towerTargetOptions.Length)
        {
            towerTargetOptionsIndex = 0;
        }
        
        if(transform.GetComponent<TurretScript>() != null)
        {
            transform.GetComponent<TurretScript>().TargetPriority = towerTargetOptions[towerTargetOptionsIndex];
        }
        else if (transform.GetComponent<TeslaTowerScript>() != null)
        {
            transform.GetComponent<TeslaTowerScript>().TargetPriority = towerTargetOptions[towerTargetOptionsIndex];
        }
        
    }
    
    private void CloseTowerOptionsAndRangeDisplay(RaycastHit2D rc)
    {
        Transform towerOptionsTransform = rc.collider.transform.parent;
        Transform currentTowerRangeDisplay = rc.collider.transform.root.Find("Tower Range Display");
        if (towerOptionsTransform.parent.GetInstanceID().Equals(transform.GetInstanceID())
            && currentTowerRangeDisplay.parent.GetInstanceID().Equals(transform.GetInstanceID()))
        {
            towerOptionsTransform.gameObject.SetActive(!towerOptionsTransform.gameObject.activeSelf);
            currentTowerRangeDisplay.gameObject.SetActive(!currentTowerRangeDisplay.gameObject.activeSelf);
        }
    }

}
