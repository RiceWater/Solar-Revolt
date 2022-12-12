using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttributesScript : MonoBehaviour
{
    //Values are serialized so one script can be applied to different enemies
    [SerializeField] private int enemyGarium;
    [SerializeField] private float enemyHealth;
    [SerializeField] private int lifeReduction;

    [SerializeField] private bool hasAnalgesicBlood;
    private bool isImmortal;

    private GariumAndLivesScript gariumAndLivesScript;

    [SerializeField] private HealthBarScript healthBar;
    private float enemyMaxHealth;
    private void Start()
    {
        gariumAndLivesScript = GameObject.Find("Game Manager").GetComponent<GariumAndLivesScript>();
        enemyMaxHealth = enemyHealth;
        healthBar.SetHealthBar(enemyHealth, enemyMaxHealth);
    }
    private void Update()
    {
        if (isImmortal)
        {
            ImmortalState();
        }
        IncreasePlayerGarium();
    }

    public int EnemyGarium
    {
        get { return enemyGarium; }
        set { enemyGarium = value; }
    }

    public float EnemyHealth
    {
        get { return enemyHealth; }
        set { enemyHealth = value; }
    }

    public int LifeReduction
    {
        get { return lifeReduction; }
        set { lifeReduction = value; }
    }
    
    private void ImmortalState()
    {
        enemyHealth = 1;
    }


    private void IncreasePlayerGarium()
    {
        if (enemyHealth < 1)
        {
            gariumAndLivesScript.Garium += enemyGarium;
            Destroy(gameObject);
        }
    }

    public void TakeDamage(float damageAmount)
    {
        
        if(hasAnalgesicBlood && enemyHealth - damageAmount < 1)
        {
            hasAnalgesicBlood = false;
            enemyHealth = 1;
            isImmortal = true;
            Invoke("RemoveImmortality", 3f);    //call function after 3 seconds
        }
        else
        {
            enemyHealth -= damageAmount;
        }

        healthBar.SetHealthBar(enemyHealth, enemyMaxHealth);
        if(enemyHealth <= enemyMaxHealth / 2 && transform.name.Contains("VIP") && !transform.GetComponent<EnemyMovementScript>().Reverse)
        {
            transform.GetComponent<EnemyMovementScript>().WayPointIndex = transform.GetComponent<EnemyMovementScript>().WayPointIndex - 1;
            transform.GetComponent<EnemyMovementScript>().Speed = transform.GetComponent<EnemyMovementScript>().Speed * 1.5f;
            transform.GetComponent<EnemyMovementScript>().Reverse = true;
            transform.GetComponent<EnemyMovementScript>().SetTargetWaypoint(transform.GetComponent<EnemyMovementScript>().WayPointIndex);
        }
    }

    private void RemoveImmortality()
    {
        isImmortal = false;
    }
}
