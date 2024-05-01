using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public GameObject shop;
    Canvas canvas;
    public GameObject interactHint;

    // Start is called before the first frame update
    void Start()
    {
        GameObject shopCanvas = GameObject.Find("ShopCanvas");
        canvas = shopCanvas.GetComponent<Canvas>();
        shop = GameObject.Find("shop");
        StartCoroutine(DisappearShop());
    }

    IEnumerator DisappearShop()
    {
        yield return new WaitForSeconds(30);
        shop.SetActive(false);
        canvas.enabled = false;
        interactHint.SetActive(false);
    }
}
