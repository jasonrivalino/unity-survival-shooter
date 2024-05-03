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
    public AudioClip MoneySoundClip;
    AudioSource MoneyAudio;
    bool CanPay = true;
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

    void Awake()
    {
        MoneyAudio = this.gameObject.AddComponent<AudioSource>();
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
            CanPay = false;
        }
        else
        {
            canvas.enabled = false;
            CanPay = true;
            MoneyAudio.PlayOneShot(MoneySoundClip);
            money -= petPrice;
        }
    }

    public void AddPet(string petName)
    {
        if (CanPay)
        {
            if (PlayerPrefs.HasKey(petName))
            {
                PlayerPrefs.SetInt(petName, PlayerPrefs.GetInt(petName) + 1);
                Debug.Log(petName + PlayerPrefs.GetInt(petName));
            }
            else
            {
                PlayerPrefs.SetInt(petName, 1);
                Debug.Log(petName + PlayerPrefs.GetInt(petName));
            }
        }
    }

    public void CloseErrorCanvas()
    {
        canvas.enabled = false;
    }
}
