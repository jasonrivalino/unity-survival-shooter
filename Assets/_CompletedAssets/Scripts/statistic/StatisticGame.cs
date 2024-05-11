using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatisticGame : MonoBehaviour
{
    [SerializeField] private Text shootAccuracy;
    [SerializeField] private Text distanceTravelled;
    [SerializeField] private Text playTime;
    [SerializeField] private Text enemiesKilled;
    [SerializeField] private Text moneyCollected;
    [SerializeField] private Text totalScore;
    [SerializeField] private Text orbCollected;

    private void SetData()
    {
        // shootAccuracy are save in float
        shootAccuracy.text = (PlayerPrefs.GetFloat(Statistics.ShootAccuracy, 0) * 100).ToString("F2") + "%";
        distanceTravelled.text = PlayerPrefs.GetFloat(Statistics.DistanceTravelled, 0).ToString("F2") + "m";
        float timeElapsed = PlayerPrefs.GetFloat(Statistics.PlayTime, 0);
        TimeSpan time = TimeSpan.FromSeconds(timeElapsed);
        playTime.text = string.Format("{0:D2}h:{1:D2}m:{2:D2}s:{3:D3}ms", 
            time.Hours, 
            time.Minutes,
            time.Seconds,
            time.Milliseconds);
        enemiesKilled.text = PlayerPrefs.GetInt(Statistics.EnemiesKilled, 0).ToString();
        moneyCollected.text = PlayerPrefs.GetInt(Statistics.MoneyCollected, 0).ToString();
        totalScore.text = PlayerPrefs.GetInt(Statistics.TotalScore, 0).ToString();
        orbCollected.text = PlayerPrefs.GetInt(Statistics.OrbCollected, 0).ToString();
    }

    public static void ResetData()
    {
        PlayerPrefs.SetFloat(Statistics.ShootAccuracy, 0);
        PlayerPrefs.SetFloat(Statistics.DistanceTravelled, 0);
        PlayerPrefs.SetFloat(Statistics.PlayTime, 0);
        PlayerPrefs.SetInt(Statistics.EnemiesKilled, 0);
        PlayerPrefs.SetInt(Statistics.MoneyCollected, 0);
        PlayerPrefs.SetInt(Statistics.TotalScore, 0);
        PlayerPrefs.SetInt(Statistics.OrbCollected, 0);
    }

    public void ActivateStatisticMenu()
    {
        SetData();
        gameObject.SetActive(true);
    }

    public void DeactivateStatisticMenu()
    {
        gameObject.SetActive(false);
    }
        
}
