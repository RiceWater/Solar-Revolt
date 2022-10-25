using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSlotScript : MonoBehaviour
{
    private RaycastHit2D[] rc;

    private void OnMouseUpAsButton()
    {
        rc = MouseRayCastScript.rc;
        Debug.Log(rc.Length);
        if (rc.Length > 1)
        {
            for (int i = 0; i < rc.Length; i++)
            {
                if (rc[i].collider is BoxCollider2D && rc[i].transform.CompareTag("Tower Slot"))
                {
                    Transform boxColliderTransform = rc[i].collider.transform;
                    //Traverse from the parent to the needed child object
                    for (int j = 0; j < boxColliderTransform.childCount; j++)
                    {
                        //Ensures that tower under mouse will be interacted and not some other tower 
                        //whose collider overlaps with the tower
                        if (boxColliderTransform.GetChild(j).name.Equals("Slot Options"))
                        {    
                            Transform towerSlotTransform = boxColliderTransform.GetChild(j);
                            towerSlotTransform.gameObject.SetActive(!towerSlotTransform.gameObject.activeSelf);
                        }
                    }
                }
                if (rc[i].collider.transform.CompareTag("Test"))
                {
                    string towerName = rc[i].collider.transform.name;
                    switch (towerName)
                    {
                        case "Tower A":
                            Debug.Log("A");
                            break;
                        case "Tower B":
                            Debug.Log("B");
                            break;
                        case "Tower C":
                            Debug.Log("C");
                            break;
                        case "Tower D":
                            Debug.Log("D");
                            break;
                        default:
                            Debug.Log("Out of choices");
                            break;
                    }
                }
            }
        }

    }
}
