using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementScript : MonoBehaviour
{
    [SerializeField] private float speed;
    private Transform targetWayPoint;
    private int waypointIndex;
    private bool setStartingPoint;

    private float editableSpeed;
    private float stunDuration;

    private bool reverse; //for VIP
    private void Start()
    {
        editableSpeed = speed;
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
                targetWayPoint = WaypointsScript.waypoints[waypointIndex];
                setStartingPoint = true;
            }

        }

        //Code for making enemy move from starting waypoint to ending waypoint
        Vector3 dir = targetWayPoint.position - transform.position;
        transform.Translate(editableSpeed * Time.deltaTime * dir.normalized);
        if (setStartingPoint && !reverse)
        {
            if (Mathf.Abs(Vector3.Distance(transform.position, targetWayPoint.position)) < 0.25f)
            {
                SetToNextWaypoint();
            }
        }
        else if (reverse)
        {
            if (Mathf.Abs(Vector3.Distance(transform.position, targetWayPoint.position)) < 0.25f)
            {
                SetToPrevWaypoint();
            }
            
        }

        if(editableSpeed == 0)
        {
            stunDuration -= Time.deltaTime;
        }

        if(stunDuration <= 0)
        {
            editableSpeed = speed;
        }
    }

    //Code for going to the next waypoint
    private void SetToNextWaypoint()
    {
        if (Mathf.Abs(Vector3.Distance(transform.position, targetWayPoint.position)) >= 0.25f)
        {
            return;
        }

        waypointIndex++;
        if (waypointIndex < WaypointsScript.waypoints.Length)
        {
            targetWayPoint = WaypointsScript.waypoints[waypointIndex];
        }
        else
        {
            GariumAndLivesScript.Lives -= transform.GetComponent<EnemyAttributesScript>().LifeReduction;
            if (GariumAndLivesScript.Lives < 0)
            {
                GariumAndLivesScript.Lives = 0;
            }
            Destroy(gameObject);
        }

    }

    private void SetToPrevWaypoint()
    {
        if (Mathf.Abs(Vector3.Distance(transform.position, targetWayPoint.position)) >= 0.25f)
        {
            return;
        }

        if (waypointIndex > -1)
        {
            waypointIndex--;
            targetWayPoint = WaypointsScript.waypoints[waypointIndex];
        }
        else
        {
            GariumAndLivesScript.Lives = 0;
        }
    }
    public void SetTargetWaypoint(int index)
    {
        targetWayPoint = WaypointsScript.waypoints[index];
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            return;
        }
        else
        {
            Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), collision.gameObject.GetComponent<Collider2D>());
        }
    }

    public float Speed
    {
        get { return editableSpeed; }
        set { editableSpeed = value; }
    }

    public bool Reverse
    {
        get { return reverse; }
        set { reverse = value; }
    }

    public int WayPointIndex
    {
        get { return waypointIndex; }
        set { waypointIndex = value; }
    }
    public void Stun(float duration)
    {
        editableSpeed = 0;
        stunDuration = duration;
    }
}
