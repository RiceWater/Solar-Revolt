using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseRayCastScript : MonoBehaviour
{
    [SerializeField] private Camera cam;

    public static RaycastHit2D[] rc;


    private void Update()
    {
        Vector2 origin = new Vector2(cam.ScreenToWorldPoint(Input.mousePosition).x, cam.ScreenToWorldPoint(Input.mousePosition).y);
        rc = Physics2D.RaycastAll(origin, Vector2.zero, 0);
      
        
    }
}
