using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeslaTowerScript : MonoBehaviour
{
    //For towers
    [Header("Tower Settings")]
    //For direction;
    //[SerializeField] private Transform directionObject;
    [SerializeField] private int gariumCost;
    [SerializeField] private float fireRate;
    [SerializeField] private List<int> upgradeCost = new List<int>();
    [SerializeField] private Animator towerAnimator;
    [SerializeField] private GameObject shockAreaGameObject;
    private int upgradeCounter;
    private float fireCountdown;
    private float bulletDamage;
    private Transform target;
    private float time = 0;
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

        //Fires the projectile
        if (fireCountdown <= 0 && target != null)
        {
            shockAreaGameObject.SetActive(true);
            shockAreaGameObject.GetComponent<Transform>().localScale = new Vector3(transform.GetComponent<CircleCollider2D>().radius/2, transform.GetComponent<CircleCollider2D>().radius / 2,
                shockAreaGameObject.GetComponent<Transform>().localScale.z);
            shockAreaGameObject.GetComponent<SpriteRenderer>().color = new Color(shockAreaGameObject.GetComponent<SpriteRenderer>().color.r, shockAreaGameObject.GetComponent<SpriteRenderer>().color.g
               , shockAreaGameObject.GetComponent<SpriteRenderer>().color.b, 0.4f);
            time = 0;
            FireProjectile();
            fireCountdown = 1f / fireRate;
        }

        if (shockAreaGameObject.activeSelf)
        {
            shockAreaGameObject.GetComponent<SpriteRenderer>().color = new Color(shockAreaGameObject.GetComponent<SpriteRenderer>().color.r, shockAreaGameObject.GetComponent<SpriteRenderer>().color.g
               , shockAreaGameObject.GetComponent<SpriteRenderer>().color.b, Mathf.Lerp(0.4f, 0, time));
            time += Time.deltaTime * 2f;
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

    public List<int> GetUpgradeCost()
    {
        return upgradeCost;
    }

    private void FireProjectile()
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
        towerAnimator.SetBool("isShooting", true);
        Invoke("StopShooting", 0.5f);
        bullet.GetComponent<BulletScript>().Damage = bulletDamage;
        bullet.GetComponent<BulletScript>().SplashRange = transform.gameObject.GetComponent<CircleCollider2D>().radius;
        bullet.GetComponent<BulletScript>().SetTarget(transform.gameObject);
    }

    private void StopShooting()
    {
        towerAnimator.SetBool("isShooting", false);
        shockAreaGameObject.SetActive(false);
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
        if (enemiesInRange.Count == 0)
        {
            target = null;
            return;
        }

        //checks if enemy is dead before targeting another enemy
        if (target != null && enemiesInRange.Contains(target.gameObject))
        {
            return;
        }

        foreach (GameObject enemy in enemiesInRange)
        {
            if (enemy == null)
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
        if (enemiesInRange.Count < 1)
        {
            return;
        }

        //checks if enemy is dead before targeting another enemy
        if (target != null && enemiesInRange.Contains(target.gameObject))
        {
            return;
        }
        foreach (GameObject enemy in enemiesInRange)
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
        if (enemiesInRange.Count < 1)
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
