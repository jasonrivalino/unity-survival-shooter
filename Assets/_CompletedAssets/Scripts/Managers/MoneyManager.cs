using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MoneyManager : MonoBehaviour
{
    public static int money;
    public TMP_Text moneyPanel;
    public TMP_Text moneyText;
    public GameObject errorCanvas;
    Canvas canvas;
    // Start is called before the first frame update
    void Start()
    {
        canvas = errorCanvas.GetComponent<Canvas>();
        if (SceneManager.GetActiveScene().name == "Town1")
        {
            money = 0;
            money += 100;
        }
        else if (SceneManager.GetActiveScene().name == "Town2")
        {
            money += 300;
        }
        else if (SceneManager.GetActiveScene().name == "Town3")
        {
            money += 500;
        }
    }

    // Update is called once per frame
    void Update()
    {
        moneyPanel.text = money.ToString();
        moneyText.text = money.ToString();
    }

    public void PayPet(TMP_Text moneyText)
    {
        int petPrice = Int32.Parse(moneyText.text);
        if (money < petPrice)
        {
            canvas.enabled = true;
        }
        else
        {
            money -= petPrice;
        }
    }

    public void CloseErrorCanvas()
    {
        canvas.enabled = false;
    }
}
