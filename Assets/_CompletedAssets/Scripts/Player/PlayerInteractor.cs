using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInteractor : MonoBehaviour
{
    public bool isInRange;
    public KeyCode interactKey;
    public UnityEvent interactAction;
    public GameObject visualUI;
    public TextMeshProUGUI hintName;
    public String visualObjectAction;
    // Start is called before the first frame update
    void Start()
    {
        visualUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isInRange)
        {
            isInRange = true;
            // Debug.Log("Player is in range");
            if (Input.GetKeyDown(interactKey))
            {

                interactAction.Invoke();
            }
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            visualUI.SetActive(true);
            hintName.text = "Tekan E untuk " + visualObjectAction + "!";
            isInRange = true;
        }
    }



    private void OnTriggerExit(Collider collider)
    {
        visualUI.SetActive(false);
        isInRange = false;
    }






}
