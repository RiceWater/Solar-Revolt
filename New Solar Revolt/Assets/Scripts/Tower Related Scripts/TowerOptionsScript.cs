using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class TowerOptionsScript : MonoBehaviour
{
    [SerializeField] private GameObject towerRangeDisplayPF;
    private RaycastHit2D[] rc;
    private Transform towerRangeDisplayTransform;

    private void Start()
    {
        Color spriteColor = towerRangeDisplayPF.GetComponent<SpriteRenderer>().material.color;
        towerRangeDisplayPF.GetComponent<SpriteRenderer>().material.color = new Color(spriteColor.r, spriteColor.g, spriteColor.b, 0.5f);
        towerRangeDisplayPF.SetActive(false);
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
                
            }
        }
    } 

    private void UpgradeTower(RaycastHit2D rc)
    {
        CircleCollider2D towerRadius = rc.collider.transform.root.GetComponent<CircleCollider2D>();
        int gariumCost = towerRadius.transform.GetComponent<TurretAScript>().GariumCost;
        if (GariumScript.Garium < gariumCost) { return; }

        towerRadius.radius += 5;
        GariumScript.Garium -= gariumCost;
        towerRadius.transform.GetComponent<TurretAScript>().GariumCost += 30;

        //add damage (need Wood's Code)
        //increase firerate(?)
    }

    private void SellTower(RaycastHit2D rc)
    {
        CircleCollider2D towerRadius = rc.collider.transform.root.GetComponent<CircleCollider2D>();
        GariumScript.Garium += (towerRadius.transform.GetComponent<TurretAScript>().GariumCost / 3);
        Destroy(gameObject);
    }

    //Retrieve the transform/objects again to avoid closing the wrong gameobjects
    //Currently the best solution to fix the closing tower menu bug
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
