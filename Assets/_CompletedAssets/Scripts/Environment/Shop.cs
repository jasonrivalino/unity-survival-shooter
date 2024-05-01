using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Shop : MonoBehaviour
{
    public void Interact()
    {
        SceneManager.LoadScene("Shop");
        Debug.Log("Interact shop");
    }
}
