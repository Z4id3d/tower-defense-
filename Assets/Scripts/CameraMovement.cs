using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Responsible for camera movements, both the cinematic and the user driven movements such as moving the camera with
/// the mouse
/// </summary>
public class CameraMovement : MonoBehaviour
{

    public static CameraMovement instance;
    void Awake ()
    {
        if (instance == null)
        {
            instance = this;

        }
    }
    
    public float cameraMovementSpeed;
    
    public GameObject leftMapEnd;
    public GameObject rightMapEnd;

    private Vector3 dragOrigin;
    
    
    void Update()
    {
        controlCameraMovement();
    }
    
    /// <summary>
    /// Allows the player to control the movement of the camera using the left mouse button
    /// </summary>
    private void controlCameraMovement()
    {

        if (Input.GetMouseButtonDown(0))
        {
            dragOrigin = Input.mousePosition;
            return;
        }
 
        if (!Input.GetMouseButton(0)) return;
 
        Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - dragOrigin);


        float xMovement = -pos.x * cameraMovementSpeed;
        float yMovement = -pos.y * cameraMovementSpeed;
        Vector3 move = new Vector3(xMovement, 0, yMovement);
        transform.Translate(move, Space.World);
        
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, leftMapEnd.transform.position.x, rightMapEnd.transform.position.x),
            transform.position.y,
            Mathf.Clamp(transform.position.z, leftMapEnd.transform.position.z, rightMapEnd.transform.position.z));

    }


    /// <summary>
    /// Move the camera to the zombie spawn location
    /// </summary>
    public void moveCameraToZombieSpawn()
    {
        transform.position =  Vector3.Lerp(transform.position, new Vector3(-25f, transform.position.y,-159f), 5f);
    }
}
