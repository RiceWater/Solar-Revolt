using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GariumScript : MonoBehaviour
{
    [SerializeField] private static int garium;
    private float second;

    void Start()
    {
        garium = 100;
        second = 0;
    }

    
    void Update()
    {
        Debug.LogWarning(garium);
        second += Time.deltaTime;
        IncrementGarium();
        ValueProofGarium();
    }

    public static int Garium
    {
        get { return garium; }
        set { garium = value; }
    }

    private void IncrementGarium()
    {
        if (second > 0.5)
        {
            garium++;
            second = 0;
        }
        
    }

    private void ValueProofGarium()
    {
        if(garium < 0)
        {
            garium = 0;
        }
    }
}
