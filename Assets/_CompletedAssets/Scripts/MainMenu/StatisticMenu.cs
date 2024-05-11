using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatisticMenu : MonoBehaviour
{
    [SerializeField] private Text AllshootAccuracy;
    [SerializeField] private Text AlldistanceTravelled;
    [SerializeField] private Text AllplayTime;
    [SerializeField] private Text AllenemiesKilled;
    [SerializeField] private Text AllmoneyCollected;
    [SerializeField] private Text HighestTotalScore;
    [SerializeField] private Text TotalOrbCollected;

    private void Start()
    {
        SetData();
    }

    private void SetData()
    {
        AllshootAccuracy.text = (PlayerPrefs.GetFloat(Statistics.AllShootAccuracy, 0) * 100).ToString("F3") + "%";
        AlldistanceTravelled.text = PlayerPrefs.GetFloat(Statistics.AllDistanceTravelled, 0).ToString("F2") + "m";
        float timeElapsed = PlayerPrefs.GetFloat(Statistics.AllPlayTime, 0);
        TimeSpan time = TimeSpan.FromSeconds(timeElapsed);
        AllplayTime.text = string.Format("{0:D2}h:{1:D2}m:{2:D2}s:{3:D3}ms", 
            time.Hours, 
            time.Minutes,
            time.Seconds,
            time.Milliseconds);
        AllenemiesKilled.text = PlayerPrefs.GetInt(Statistics.AllEnemiesKilled, 0).ToString();
        AllmoneyCollected.text = PlayerPrefs.GetInt(Statistics.AllMoneyCollected, 0).ToString();
        HighestTotalScore.text = PlayerPrefs.GetInt(Statistics.AllHighTotalScore, 0).ToString();
        TotalOrbCollected.text = PlayerPrefs.GetInt(Statistics.AllOrbCollected, 0).ToString();
    }

    public static void ResetAllTimeStatData()
    {
        PlayerPrefs.SetFloat(Statistics.AllShootAccuracy, 0);
        PlayerPrefs.SetFloat(Statistics.AllDistanceTravelled, 0);
        PlayerPrefs.SetFloat(Statistics.AllPlayTime, 0);
        PlayerPrefs.SetInt(Statistics.AllEnemiesKilled, 0);
        PlayerPrefs.SetInt(Statistics.AllMoneyCollected, 0);
        PlayerPrefs.SetInt(Statistics.AllHighTotalScore, 0);
        PlayerPrefs.SetInt(Statistics.AllOrbCollected, 0);
    }
        
}
