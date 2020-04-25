using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Holds lists of waypoints as children, each child is a list and contains a line of waypoints
/// </summary>
public class WayPointGroups : MonoBehaviour
{
    public static Transform[][] wayPointArrays;

    void Awake ()
    {
        Transform[] points;
        wayPointArrays = new Transform[transform.childCount][];
        for (int i = 0; i < wayPointArrays.Length; i++)
        {
            Transform firstChild = transform.GetChild(i);
            
            points = new Transform[firstChild.childCount];
            for (int j = 0; j < points.Length; j++)
            {
                points[j] = firstChild.GetChild(j);
            }

            wayPointArrays[i] = points;
        }
    
    }
}
