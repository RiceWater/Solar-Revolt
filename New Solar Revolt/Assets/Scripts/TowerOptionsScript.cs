using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class TowerOptionsScript : MonoBehaviour
{
    [SerializeField] private GameObject towerOptions;
    private RaycastHit2D[] rc;
    private GameObject selectedTower;

    private void OnMouseUpAsButton()
    {
        rc = MouseRayCastScript.rc;
        if (rc.Length > 1)
        {
            Debug.Log(rc.Length);
            for (int i = 0; i < rc.Length; i++)
            {
                if (rc[i].collider is BoxCollider2D && rc[i].transform.CompareTag("Tower"))
                {
                    if(rc[i].collider.transform.root.gameObject == transform.gameObject)
                    {
                        Debug.Log("RIGHT PARENT");
                        towerOptions.SetActive(!towerOptions.activeSelf);
                        selectedTower = towerOptions.transform.root.gameObject;
                        Debug.Log(selectedTower.name);
                    }
                    
                }

                if (rc[i].collider.transform.CompareTag("Upgrade"))
                {
                    Debug.Log("UPGRAADE");
                    CircleCollider2D towerRadius = rc[i].collider.transform.root.GetComponent<CircleCollider2D>();
                    towerRadius.radius += 5;
                    GariumScript.Garium -= 5;
                    //add damage
                    //increase firerate(?)
                    //decrease garium
                }
                else if (rc[i].collider.transform.CompareTag("Sell")){
                    Debug.Log("SELLL");
                    
                    //Destroy Object and add GARIUUUUM
                }
            }
        }

    }

    
    
}
