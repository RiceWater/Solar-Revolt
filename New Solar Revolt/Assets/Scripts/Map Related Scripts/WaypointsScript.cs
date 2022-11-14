using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointsScript : MonoBehaviour
{
    public static Transform[] waypoints;
    public static bool initialized;
    private void Start()
    {
        waypoints = new Transform[transform.childCount];

        for (int i = 0; i < waypoints.Length; i++)
        {
            waypoints[i] = transform.GetChild(i);
        }
        initialized = true;
    }
}
