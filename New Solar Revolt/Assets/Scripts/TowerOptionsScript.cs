using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class TowerOptionsScript : MonoBehaviour
{
    private RaycastHit2D[] rc;

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
                    for(int j = 0; j < boxColliderTransform.childCount; j++)
                    {
                        //Ensures that tower under mouse will be interacted and not some other tower 
                        //whose collider overlaps with the tower
                        if(boxColliderTransform.GetChild(j).name.Equals("TowerOptions"))
                        {
                            Transform towerOptionsTransform = boxColliderTransform.GetChild(j);
                            towerOptionsTransform.gameObject.SetActive(!towerOptionsTransform.gameObject.activeSelf);
                        }
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
