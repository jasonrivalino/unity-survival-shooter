using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Shop : MonoBehaviour
{
    Canvas canvas;

    void Start()
    {
        GameObject shopCanvas = GameObject.Find("ShopCanvas");
        canvas = shopCanvas.GetComponent<Canvas>();
    }
    public void Interact()
    {
        canvas.enabled = true;
        Debug.Log("Interact shop");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            canvas.enabled = false;
        }
    }
}
