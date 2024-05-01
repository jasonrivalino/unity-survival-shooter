using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    public GameObject errorUI;
    public TextMeshProUGUI errorName;
    public bool isInRange;
    public KeyCode interactKey;

    // Start is called before the first frame update
    void Start()
    {
        errorUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isInRange)
        {
            if (Input.GetKeyDown(interactKey))
            {
                StartCoroutine(ShowErrorMessage());
            }
        }

    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Interactor"))
        {
            isInRange = true;
        }
    }



    private void OnTriggerExit(Collider collider)
    {
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
