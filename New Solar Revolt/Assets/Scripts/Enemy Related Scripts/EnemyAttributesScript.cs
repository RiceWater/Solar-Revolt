using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttributesScript : MonoBehaviour
{
    //Values are serialized so one script can be applied to 3 different enemies
    //SeralizedField makes you input values in the inspector
    [SerializeField] private int enemyGarium;
    [SerializeField] private float enemyHealth;


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

    private void IncreasePlayerGarium()
    {
        if (enemyHealth < 1)
        {
            GariumScript.Garium += enemyGarium;
            Destroy(gameObject);
        }
    }
    public void TakeDamage(float damageAmount)
    {
        enemyHealth -= damageAmount;
    }
}
