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
    public TMP_Text rabbitText;
    public TMP_Text mushroomText;
    public TMP_Text ghostText;
    public TMP_Text dogText;
    public TMP_Text cactusText;
    public TMP_Text bombText;
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
        if (PlayerPrefs.HasKey("Motherlode"))
        {
            moneyPanel.text = "infinite";
            moneyText.text = "infinite";
        }
        else
        {
            moneyPanel.text = money.ToString();
            moneyText.text = money.ToString();
        }
        rabbitText.text = PlayerPrefs.GetInt("rabbit").ToString();
        mushroomText.text = PlayerPrefs.GetInt("mushroom").ToString();
        ghostText.text = PlayerPrefs.GetInt("ghost").ToString();
        dogText.text = PlayerPrefs.GetInt("dog").ToString();
        cactusText.text = PlayerPrefs.GetInt("cactus").ToString();
        bombText.text = PlayerPrefs.GetInt("bomb").ToString();

    }

    public void PayPet(TMP_Text price)
    {
        int petPrice = Int32.Parse(price.text);
        if (PlayerPrefs.HasKey("Motherlode"))
        {
            moneyPanel.text = "infinite";
            CanPay = true;
            MoneyAudio.PlayOneShot(MoneySoundClip);
        }
        else
        {
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
