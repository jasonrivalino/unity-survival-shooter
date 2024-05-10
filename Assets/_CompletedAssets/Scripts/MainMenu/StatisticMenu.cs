using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatisticMenu : MonoBehaviour
{
    [SerializeField] private Text shootAccuracy;
    [SerializeField] private Text distanceTravelled;
    [SerializeField] private Text playTime;
    [SerializeField] private Text enemiesKilled;
    [SerializeField] private Text moneyCollected;
    [SerializeField] private Text totalScore;

    private void Start()
    {
        SetData();
    }

    private void SetData()
    {
        shootAccuracy.text = PlayerPrefs.GetFloat("ShootAccuracy", 0).ToString("F2") + "%";
        distanceTravelled.text = PlayerPrefs.GetFloat("DistanceTravelled", 0).ToString("F2") + "m";
        playTime.text = PlayerPrefs.GetFloat("PlayTime", 0).ToString("F2") + "s";
        enemiesKilled.text = PlayerPrefs.GetInt("EnemiesKilled", 0).ToString();
        moneyCollected.text = PlayerPrefs.GetInt("MoneyCollected", 0).ToString();
        totalScore.text = PlayerPrefs.GetInt("TotalScore", 0).ToString();
    }

    public void ResetData()
    {
        PlayerPrefs.SetFloat("ShootAccuracy", 0);
        PlayerPrefs.SetFloat("DistanceTravelled", 0);
        PlayerPrefs.SetFloat("PlayTime", 0);
        PlayerPrefs.SetInt("EnemiesKilled", 0);
        PlayerPrefs.SetInt("MoneyCollected", 0);
        PlayerPrefs.SetInt("TotalScore", 0);
        SetData();
    }
        
}
