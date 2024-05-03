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
    // Start is called before the first frame update
    void Start()
    {
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

    public void PayPet()
    {
        money -= 100;
    }
}
