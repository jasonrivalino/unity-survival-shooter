using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public GameObject shop;
    // Start is called before the first frame update
    void Start()
    {
        // StartCoroutine(DisappearShop());
    }

    IEnumerator DisappearShop()
    {
        yield return new WaitForSeconds(25);
        shop.SetActive(false);
    }
}
