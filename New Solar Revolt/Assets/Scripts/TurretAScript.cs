using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAScript : MonoBehaviour
{
    private Transform target;  

    //For direction;
    public Transform directionObject;

    //Bullet-related
    public float fireRate = 1f;
    private float fireCountdown = 0f;

    //Bullet Prefabs-related
    public GameObject pfBulletA;
    public Transform spawnLocationBulletA;

    private List<GameObject> enemiesInRange = new List<GameObject>();


    private void Start()
    {
        InvokeRepeating("TargetEnemyNearGoal", 0f, 1f);
    }

    private void Update()
    {
        if(target == null)
        {
            return;
        }

        RotateTowardsTarget();

        //Fires the projectile
        if(fireCountdown <= 0)
        {
            FireProjectile();
            fireCountdown = 1f / fireRate;
        }
        fireCountdown -= Time.deltaTime;


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            enemiesInRange.Add(collision.gameObject);
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            enemiesInRange.Remove(collision.gameObject);
        }
    }

    private void RotateTowardsTarget()
    {
        var offset = 0f;
        Vector2 dir = target.position - transform.position;
        dir.Normalize();
        float angle = Mathf.Rad2Deg * Mathf.Atan2(dir.y, dir.x);
        directionObject.transform.rotation = Quaternion.Euler(Vector3.forward * (angle + offset));
    }

    private void FireProjectile()
    {
        GameObject bullet = Instantiate(pfBulletA, spawnLocationBulletA.position, spawnLocationBulletA.rotation);
        bullet.GetComponent<BulletAScript>().SetTarget(target.gameObject);   
    }


    //Targets the enemy closest to the endpoint 
    private void TargetEnemyNearGoal()
    {
        float highestDistance = -1f;
        if(enemiesInRange.Count == 0)
        {
            target = null;
            return;
        }

        foreach (GameObject enemy in enemiesInRange)
        {
            float currEnemyDistance = enemy.GetComponent<EnemyDistanceTraveledScript>().getDistanceTraveled();
            if (currEnemyDistance > highestDistance)
            {
                target = enemy.transform;
                highestDistance = currEnemyDistance;
            }
        }
    }

    //Targets healthiest enemy
    private void TargetHealthiestEnemy()
    {

    }

    
}
