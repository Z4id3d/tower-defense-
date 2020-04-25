using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Holds all spawn points as children, adds them to a public accessible list
/// </summary>
public class SpawnPoints : MonoBehaviour
{
    public static Transform[] points;

    void Awake ()
    {
        points = new Transform[transform.childCount];
        for (int i = 0; i < points.Length; i++)
        {
            points[i] = transform.GetChild(i);
        }
    
    }
}
