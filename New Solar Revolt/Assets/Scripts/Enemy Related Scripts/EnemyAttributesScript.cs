using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttributesScript : MonoBehaviour
{
    //Values are serialized so one script can be applied to 3 different enemies
    //SeralizedField makes you input values in the inspector
    [SerializeField] private int enemyGarium;
    [SerializeField] private float enemyHealth;
    [SerializeField] private int lifeReduction;


    private void Update()
    {
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
    private void IncreasePlayerGarium()
    {
        if (enemyHealth < 1)
        {
            GariumAndLivesScript.Garium += enemyGarium;
            Destroy(gameObject);
        }
    }
    public void TakeDamage(float damageAmount)
    {
        enemyHealth -= damageAmount;
    }
}
