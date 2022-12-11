using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//to rename file for better suit
public class GariumAndLivesScript : MonoBehaviour
{
    private static int lives;
    [SerializeField] private static int garium;
    private float second;

    void Start()
    {
        garium = 50;
        lives = 20;
        second = 0;
    }


    void Update()
    {
        second += Time.deltaTime;
        IncrementGarium();
    }

    public static int Garium
    {
        get { return garium; }
        set { garium = value; }
    }

    public static int Lives
    {
        get { return lives; }
        set { lives = value; }
    }


    private void IncrementGarium()
    {
        if (second > 0.7)
        {
            garium++;
            second = 0;
        }
        
    }

}
