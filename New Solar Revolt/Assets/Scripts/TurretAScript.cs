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

    [SerializeField] private List<GameObject> enemiesInRange = new List<GameObject>();

    //targetType = G : first to goal, S : strongest, W : weakest, R : first in range
    [SerializeField] private char targetPriority;

    private void Start()
    {
        //InvokeRepeating("TargetEnemyNearGoal", 0f, 1f);
        targetPriority = 'G';
    }

    private void Update()
    {
        SetTargetPriority();
        if (target == null)
        {
            return;
        }

        RotateTowardsTarget();

        //Fires the projectile
        if(fireCountdown <= 0 && target != null)
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

    private void SetTargetPriority()
    {
        switch (targetPriority)
        {
            case 'G':
                TargetEnemyNearGoal();
                break;
            case 'R':
                TargetEnemyFirst();
                break;
            case 'S':
                TargetEnemyMostHP();
                break;
            case 'W':
                TargetEnemyLeastHP();
                break;
            default:
                TargetEnemyNearGoal();
                break;
        }
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

        //checks if enemy is dead before targeting another enemy
        if(target != null)
        {
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

    //Targets enemy that came into range first
    private void TargetEnemyFirst()
    {
        if(enemiesInRange.Count < 1)
        {
            return;
        }

        //checks if enemy is dead before targeting another enemy
        if (target != null)
        {
            return;
        }

        target = enemiesInRange[0].transform;
    }

    //Targets the enemy with the highest health 
    private void TargetEnemyMostHP()
    {
        if(enemiesInRange.Count < 1)
        {
            return;
        }

        //checks if enemy is dead before targeting another enemy
        if (target != null)
        {
            return;
        }

        int maxHealth = -10000;
        foreach (GameObject enemy in enemiesInRange)
        {
            int currEnemyHealth = enemy.GetComponent<EnemyAttributesScript>().health;
            if (currEnemyHealth > maxHealth)
            {
                target = enemy.transform;
                maxHealth = currEnemyHealth;
            }
        }
    }

    //Targets the enemy with the lowest health
    private void TargetEnemyLeastHP()
    {
        if (enemiesInRange.Count < 1)
        {
            return;
        }

        //checks if enemy is dead before targeting another enemy
        if (target != null)
        {
            return;
        }

        int minHealth = 10000;
        foreach (GameObject enemy in enemiesInRange)
        {
            int currEnemyHealth = enemy.GetComponent<EnemyAttributesScript>().health;
            if (currEnemyHealth < minHealth)
            {
                target = enemy.transform;
                minHealth = currEnemyHealth;
            }
        }
    }
    
}
