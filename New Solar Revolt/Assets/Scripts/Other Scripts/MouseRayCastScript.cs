using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseRayCastScript : MonoBehaviour
{
    [SerializeField] private Camera camera;

    public static RaycastHit2D[] rc;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 origin = new Vector2(camera.ScreenToWorldPoint(Input.mousePosition).x, camera.ScreenToWorldPoint(Input.mousePosition).y);
        rc = Physics2D.RaycastAll(origin, Vector2.zero, 0);
      
        
    }
}
