using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player; // Will reference the player game object's position
    private Vector3 offset; // Will set the offset position from the player to the camera
    
    void Start()
    {
        // Calculate and store the offset value by getting the distance between the player's position and camera's position.
        offset = transform.position - player.transform.position;
    }
    void Update()
    {
        // Sets the camera to where the player is plus the offset set above
        transform.position = player.transform.position + offset;
    }
    
}