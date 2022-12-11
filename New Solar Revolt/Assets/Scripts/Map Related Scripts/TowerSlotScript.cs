using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSlotScript : MonoBehaviour
{
    [SerializeField] private GameObject[] towerPrefabs;
    private RaycastHit2D[] rc;
    private GameObject currTower;
    private Transform towerSlotTransform;
    private GariumAndLivesScript gariumAndLivesScript;

    private void Start()
    {
        gariumAndLivesScript = GameObject.Find("Game Manager").GetComponent<GariumAndLivesScript>();
        currTower = null;
    }

    private void Update()
    {
        if (!currTower) //to prevent uninteractivity of mouse and tower slot
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -2);
        }
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
        if (rc.Length >= 1)
        {
            for (int i = 0; i < rc.Length; i++)
            {
                if (rc[i].collider is BoxCollider2D && rc[i].transform.CompareTag("Tower Slot") && currTower == null)
                {
                    Transform boxColliderTransform = rc[i].collider.transform;
                    //Traverse from the parent to the needed child object
                    for (int j = 0; j < boxColliderTransform.childCount; j++)
                    {
                        //Ensures that tower under mouse will be interacted and not some other tower 
                        //whose collider overlaps with the tower
                        if (boxColliderTransform.GetChild(j).CompareTag("Slot Options") && currTower == null)
                        {
                            //Transform
                            towerSlotTransform = boxColliderTransform.GetChild(j);
                            if (towerSlotTransform.transform.parent.GetInstanceID().Equals(transform.GetInstanceID())) 
                            { 
                                towerSlotTransform.gameObject.SetActive(!towerSlotTransform.gameObject.activeSelf);
                            }
                            
                        }
                    }
                }
                if (rc[i].collider.transform.CompareTag("Slot Option"))
                {
                    string towerName = rc[i].collider.transform.name;
                    SpawnTower(towerName);
                    towerSlotTransform = rc[i].collider.transform.parent;
                    towerSlotTransform.gameObject.SetActive(!towerSlotTransform.gameObject.activeSelf);
                }
            }
        }

    }

    private void SpawnTower(string towerOptionName)
    {
        int towerCost;
        switch (towerOptionName)
        {
            case "Tower A":
                towerCost = towerPrefabs[0].GetComponent<TurretScript>().GariumCost;
                if (gariumAndLivesScript.Garium < towerCost)
                {
                    return;
                }
                gariumAndLivesScript.Garium -= towerCost;
                currTower = Instantiate(towerPrefabs[0]);
                currTower.transform.position = new Vector3(transform.position.x, transform.position.y, -1);
                transform.position = new Vector3(transform.position.x, transform.position.y, 0);
                break;
            case "Tower B":
                towerCost = towerPrefabs[1].GetComponent<TurretScript>().GariumCost;
                if (gariumAndLivesScript.Garium < towerCost)
                {
                    return;
                }
                gariumAndLivesScript.Garium -= towerCost;
                currTower = Instantiate(towerPrefabs[1]);
                currTower.transform.position = new Vector3(transform.position.x, transform.position.y, -1);
                transform.position = new Vector3(transform.position.x, transform.position.y, 0);

                break;
            case "Tower C":
                towerCost = towerPrefabs[2].GetComponent<TeslaTowerScript>().GariumCost;
                if (gariumAndLivesScript.Garium < towerCost)
                {
                    return;
                }
                gariumAndLivesScript.Garium -= towerCost;
                currTower = Instantiate(towerPrefabs[2]);
                currTower.transform.position = new Vector3(transform.position.x, transform.position.y, -1);
                transform.position = new Vector3(transform.position.x, transform.position.y, 0);
                break;
            default:
                break;
        }
        
    }
}
