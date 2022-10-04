using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDistanceTraveledScript : MonoBehaviour
{
    //Basically uses the speed * time = distance formula
    //To know how far the enemy traveled
    private float speed;
    public float distanceTraveled;
    private float timeSinceSpawn;
    void Start()
    {
        speed = gameObject.GetComponent<EnemyMovementScript>().getSpeed();
        distanceTraveled = 0;
    }

    void Update()
    {
        timeSinceSpawn += Time.deltaTime;
        distanceTraveled = timeSinceSpawn * speed;
    }

    public float getDistanceTraveled()
    {
        return distanceTraveled;
    }
}
