using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementScript : MonoBehaviour
{
    [SerializeField] private int speed;
    private Transform target;
    private int waypointIndex;
    private bool setStartingPoint;

    private void Start()
    {
        waypointIndex = 1;
        setStartingPoint = false;
    }

    private void Update()
    {
        if (WaypointsScript.initialized)
        {
            if (!setStartingPoint)
            {
                transform.position = WaypointsScript.waypoints[0].transform.position;
                target = WaypointsScript.waypoints[waypointIndex];
                setStartingPoint = true;
            }
            
        }

        //Code for making enemy move from starting waypoint to ending waypoint
        if (setStartingPoint)
        {
            Vector3 dir = target.position - transform.position;
            transform.Translate(speed * Time.deltaTime * dir.normalized);

            if (Vector3.Distance(transform.position, target.position) < 0.25f)
            {
                SetNextWaypoint();
            }
        }
    }

    //Code for going to the next waypoint
    private void SetNextWaypoint()
    {
        waypointIndex++;
        if (waypointIndex < WaypointsScript.waypoints.Length)
        {
            target = WaypointsScript.waypoints[waypointIndex];
        }
        else
        {
            Destroy(gameObject);
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), collision.gameObject.GetComponent<Collider2D>());
        }
    }

    public float getSpeed()
    {
        return speed;
    }
}
