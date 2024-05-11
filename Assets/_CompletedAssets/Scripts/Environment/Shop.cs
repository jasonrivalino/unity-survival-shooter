using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Shop : MonoBehaviour
{
    Canvas canvas;
    GameObject shop;
    void Start()
    {
        GameObject shopCanvas = GameObject.Find("ShopCanvas");
        shop = GameObject.Find("shop");
        canvas = shopCanvas.GetComponent<Canvas>();
    }
    public void Interact()
    {
        canvas.enabled = true;
        Time.timeScale = 0;
        Debug.Log("Interact shop");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        // if (Input.GetKeyDown(KeyCode.Escape) || (shop != null && !shop.activeSelf))
        {
            canvas.enabled = false;
            Time.timeScale = 1;
        }
    }
}
