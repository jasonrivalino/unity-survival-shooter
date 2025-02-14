using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toilet : MonoBehaviour
{
    [SerializeField] private Canvas saveCanvas;

    public void Interact()
    {
        saveCanvas.enabled = true;
        Time.timeScale = 0;
        Debug.Log("Interact save");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        // if (Input.GetKeyDown(KeyCode.Escape) || (shop != null && !shop.activeSelf))
        {
            saveCanvas.enabled = false;
            Time.timeScale = 1;
        }
    }
}
