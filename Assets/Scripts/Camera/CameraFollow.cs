using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraFollow : MonoBehaviour
{
    private static CameraFollow instance;
    private GameObject player;
    private Transform target;
    public float mouseSensitivity = 2.0f; // Mouse sensitivity for looking around
    public float upDownRange = 90.0f; // Maximum angle to look up or down
    public float distanceBehind = 3.0f; // Desired distance behind the player
    public float cameraHeight = 2.0f; // Desired height of the camera above the player
    private float verticalRotation = 0;
    private Vector3 offset; // Offset between camera and player
    
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        target = player.transform;
        
        Cursor.lockState = CursorLockMode.Locked; // Lock cursor to center of screen
        Cursor.visible = false; // Hide cursor
        
        // Calculate initial offset based on desired distance behind and camera height
        offset = new Vector3(0, cameraHeight, -distanceBehind);
    }

    private void Update()
    {
        // Rotate player horizontally based on mouse movement
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        player.transform.Rotate(Vector3.up, mouseX);
        
        // Rotate camera vertically based on mouse movement
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;
        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -upDownRange, upDownRange);
        transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);
    }

    private void FixedUpdate()
    {
        // Move the camera behind the player, applying the height offset
        Vector3 targetCamPos = target.position + offset;
        transform.position = targetCamPos;
    }
}
