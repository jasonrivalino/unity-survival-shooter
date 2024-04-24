using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraFollow : MonoBehaviour
{
    private static CameraFollow instance;
    private GameObject player;
    private Transform target;
    public float smoothing = 5f;
    Vector3 offset;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        target = player.transform;
    }


    private void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            offset = new Vector3(6.54f, 6.71f, -10.22f);
        } else if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            offset = new Vector3(1.00f, 15.00f, -22.00f);
        }
    }
    

    private void FixedUpdate()
    {
        Vector3 targetCamPos = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
    }
    
}