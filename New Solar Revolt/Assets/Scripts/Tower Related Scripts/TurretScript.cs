using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretScript : MonoBehaviour
{
    //For towers
    [Header("Tower Settings")]
    //For direction;
    [SerializeField] private Transform directionObject;
    [SerializeField] private Transform bulletSpawnLocation;
    [SerializeField] private int gariumCost;
    [SerializeField] private float fireRate;
    [SerializeField] private List<int> upgradeCost = new List<int>();
    private int upgradeCounter;
    private float fireCountdown;
    private float bulletDamage;
    private Transform target;

    //For prefabs
    [Header("Required Prefabs")]
    [SerializeField] private GameObject bulletPrefab;
    
    [SerializeField] private List<GameObject> enemiesInRange = new List<GameObject>();

    //targetType = G : first to goal, S : strongest, W : weakest, R : first in range
    private char targetPriority;

    private void Start()
    {
        targetPriority = 'G';
        fireCountdown = 0f;
        bulletDamage = bulletPrefab.GetComponent<BulletScript>().Damage;
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

    public float BulletDamage
    {
        get { return bulletDamage; }
        set { bulletDamage = value; }
    }

    public float FireRate
    {
        get { return fireRate; }
        set { fireRate = value; }
    }

    public int GariumCost
    {
        get { return gariumCost; }
        set { gariumCost = value; }
    }

    public char TargetPriority
    {
        get { return targetPriority; }
        set { targetPriority = value; }
    }

    public int UpgradeCounter
    {
        get { return upgradeCounter; }
        set { upgradeCounter = value; }
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

    public  List<int> GetUpgradeCost()
    {
        return upgradeCost;
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
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnLocation.position, bulletSpawnLocation.rotation);
        bullet.GetComponent<BulletScript>().Damage = bulletDamage;
        bullet.GetComponent<BulletScript>().SetTarget(target.gameObject);   
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
        if(target != null && enemiesInRange.Contains(target.gameObject))
        {
            return;
        }

        foreach (GameObject enemy in enemiesInRange)
        {
            if(enemy == null)
            {
                continue;
            }
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
        if (target != null && enemiesInRange.Contains(target.gameObject))
        {
            return;
        }
        foreach(GameObject enemy in enemiesInRange)
        {
            if (enemy == null)
            {
                continue;
            }
            else
            {
                target = enemy.transform;
                break;
            }
        }
        
    }

    //Targets the enemy with the highest health 
    private void TargetEnemyMostHP()
    {
        if(enemiesInRange.Count < 1)
        {
            return;
        }

        //checks if enemy is dead before targeting another enemy
        if (target != null && enemiesInRange.Contains(target.gameObject))
        {
            return;
        }

        float maxHealth = -10000;
        foreach (GameObject enemy in enemiesInRange)
        {
            if (enemy == null)
            {
                continue;
            }
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
        if (target != null && enemiesInRange.Contains(target.gameObject))
        {
            return;
        }

        float minHealth = 10000;
        foreach (GameObject enemy in enemiesInRange)
        {
            if (enemy == null)
            {
                continue;
            }
            float currEnemyHealth = enemy.GetComponent<EnemyAttributesScript>().EnemyHealth;
            if (currEnemyHealth < minHealth)
            {
                target = enemy.transform;
                minHealth = currEnemyHealth;
            }
        }
    }
    
}
