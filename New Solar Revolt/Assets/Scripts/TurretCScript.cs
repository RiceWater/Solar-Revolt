using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretCScript : MonoBehaviour
{
    [SerializeField] private int gariumCost;
    private Transform target;  

    //For direction;
    public Transform directionObject;

    //Bullet-related
    public float fireRate;
    private float fireCountdown;

    //Bullet Prefabs-related
    public GameObject pfBulletC;
    public Transform spawnLocationBulletC;

    [SerializeField] private List<GameObject> enemiesInRange = new List<GameObject>();

    //targetType = G : first to goal, S : strongest, W : weakest, R : first in range
    [SerializeField] private char targetPriority;

    private void Start()
    {
        //InvokeRepeating("TargetEnemyNearGoal", 0f, 1f);
        targetPriority = 'G';
        fireRate = 1f;
        fireCountdown = 0f;
        gariumCost = 20;
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

    public int GariumCost
    {
        get { return gariumCost; }
        set { gariumCost = value; }
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
        GameObject bullet = Instantiate(pfBulletC, spawnLocationBulletC.position, spawnLocationBulletC.rotation);
        bullet.GetComponent<BulletCScript>().SetTarget(target.gameObject);   
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

        float maxHealth = -10000;
        foreach (GameObject enemy in enemiesInRange)
        {
            float currEnemyHealth = enemy.GetComponent<EnemyAttributesScript>().EnemyHealth;
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

        float minHealth = 10000;
        foreach (GameObject enemy in enemiesInRange)
        {
            float currEnemyHealth = enemy.GetComponent<EnemyAttributesScript>().EnemyHealth;
            if (currEnemyHealth < minHealth)
            {
                target = enemy.transform;
                minHealth = currEnemyHealth;
            }
        }
    }
    
}
