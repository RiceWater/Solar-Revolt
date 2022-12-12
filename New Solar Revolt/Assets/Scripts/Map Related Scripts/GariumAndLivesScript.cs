using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//to rename file for better suit
public class GariumAndLivesScript : MonoBehaviour
{
    private int lives;
    private int garium;
    private float second;

    void Start()
    {
        garium = 150; 
        lives = 20;
        second = 0;
    }


    void Update()
    {
        second += Time.deltaTime;
        IncrementGarium();
    }

    public  int Garium
    {
        get { return garium; }
        set { garium = value; }
    }

    public int Lives
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
