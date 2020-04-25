using UnityEngine;

/// <summary>
/// Holds a list that contains a line of waypoints as children (NOTE: THIS CODE IS NOT USED, INSTEAD WE USE CLASS
/// "WayPointGroups" as a replacement of three scripts)
/// </summary>
public class Waypoints2 : MonoBehaviour{
    
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
