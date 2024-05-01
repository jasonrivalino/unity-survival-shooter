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
    public GameObject errorUI;
    public TextMeshProUGUI hintName;
    public TextMeshProUGUI errorName;
    public String visualObjectAction;
    // Start is called before the first frame update
    void Start()
    {
        visualUI.SetActive(false);
        errorUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isInRange)
        {
            isInRange = true;
            Debug.Log("Player is in range");
            if (Input.GetKeyDown(interactKey))
            {

                interactAction.Invoke();
            }
        }
        else
        {
            Debug.Log("Player is not in range");

            if (Input.GetKeyDown(interactKey))
            {
                StartCoroutine(ShowErrorMessage());
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

    IEnumerator ShowErrorMessage()
    {
        yield return new WaitForSeconds((float)0.5);
        errorUI.SetActive(true);
        errorName.text = "Tidak bisa interaksi dengan apa-apa!";
        yield return new WaitForSeconds(2);
        errorUI.SetActive(false);
    }




}
