using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GariumScript : MonoBehaviour
{
    [SerializeField] private static int garium;
    void Start()
    {
        garium = 0;
    }

    
    void Update()
    {

    }

    public static int Garium
    {
        get { return garium; }  //read
        set { garium = value; } //write
    }
}
