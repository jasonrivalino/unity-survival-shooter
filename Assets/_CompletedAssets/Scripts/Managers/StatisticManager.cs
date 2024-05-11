using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace CompleteProject {
public class StatisticManager : MonoBehaviour
{
    public StatisticGame statisticGame;
    public Weapon weapons; // for getting hitCount and totalShootCount: Accuracy: weapon.GetHitCount() / weapon.GetTotalShootCount()
    public PlayerHealth playerHealth; // for checking if player is dead or not: playerHealth.currentHealth <= 0
    public PlayerMovement playerMovement; // for getting distanceTravelled: playerMovement.GetDistanceTravelled()
    public SceneStopwatch sceneStopwatch; // for getting playTime: sceneStopwatch.GetElapsedTime()
    private bool isgameOver = false;
    private float shootAccuracy;
    private float distanceTravelled;
    private float playTime;
    private int enemiesKilled;
    private int moneyCollected;
    private int totalScore;
    private int orbCollected;

    private void InitData()
    {
        shootAccuracy = PlayerPrefs.GetFloat(Statistics.ShootAccuracy, 0);
        distanceTravelled = PlayerPrefs.GetFloat(Statistics.DistanceTravelled, 0);
        playTime = PlayerPrefs.GetFloat(Statistics.PlayTime, 0);
        enemiesKilled = PlayerPrefs.GetInt(Statistics.EnemiesKilled, 0);
        moneyCollected = PlayerPrefs.GetInt(Statistics.MoneyCollected, 0);
        totalScore = PlayerPrefs.GetInt(Statistics.TotalScore, 0);
        orbCollected = PlayerPrefs.GetInt(Statistics.OrbCollected, 0);
        Debug.Log("StatisticManager::InitData() shootAccuracy: " + shootAccuracy);
    }

    public void UpdateData()
    {
        float accuracy = (float) weapons.GetHitCount() / weapons.GetTotalShootCount();
        if (float.IsNaN(accuracy))
        {
            accuracy = 0f;
        }

        shootAccuracy = (shootAccuracy + accuracy) / 2;
        distanceTravelled = distanceTravelled + playerMovement.GetDistanceTravelled();
        playTime = playTime + sceneStopwatch.GetElapsedTime();
        enemiesKilled = enemiesKilled + PlayerPrefs.GetInt("DeathCount", 0);
        moneyCollected = moneyCollected + MoneyManager.money;
        totalScore = totalScore + ScoreManager.score;
        orbCollected = orbCollected + PlayerPrefs.GetInt("numDamageOrbPicked", 0) + PlayerPrefs.GetInt("numSpeedOrbPicked", 0) + PlayerPrefs.GetInt("numHealthOrbPicked", 0);

        Debug.Log("StatisticManager::UpdateData()  EnemyHealth.deathCount: " + PlayerPrefs.GetInt("DeathCount", 0));

        PlayerPrefs.SetFloat(Statistics.ShootAccuracy, shootAccuracy);
        PlayerPrefs.SetFloat(Statistics.DistanceTravelled, distanceTravelled);
        PlayerPrefs.SetFloat(Statistics.PlayTime, playTime);
        PlayerPrefs.SetInt(Statistics.EnemiesKilled, enemiesKilled);
        PlayerPrefs.SetInt(Statistics.MoneyCollected, moneyCollected);
        PlayerPrefs.SetInt(Statistics.TotalScore, totalScore);
        PlayerPrefs.SetInt(Statistics.OrbCollected, orbCollected);

        PlayerPrefs.Save();
    }

    public void CalculateAllTimeStatistic()
    {
        float AllShootAccuracy = PlayerPrefs.GetFloat(Statistics.AllShootAccuracy, 0);
        float AllDistanceTravelled = PlayerPrefs.GetFloat(Statistics.AllDistanceTravelled, 0);
        float AllPlayTime = PlayerPrefs.GetFloat(Statistics.AllPlayTime, 0);
        int AllEnemiesKilled = PlayerPrefs.GetInt(Statistics.AllEnemiesKilled, 0);
        int AllHighMoneyCollected = PlayerPrefs.GetInt(Statistics.AllMoneyCollected, 0);
        int AllHighTotalScore = PlayerPrefs.GetInt(Statistics.AllHighTotalScore, 0);
        int AllOrbCollected = PlayerPrefs.GetInt(Statistics.AllOrbCollected, 0);

        
        AllShootAccuracy = (AllShootAccuracy + shootAccuracy) / 2;
        AllDistanceTravelled = AllDistanceTravelled + distanceTravelled;
        AllPlayTime = AllPlayTime + playTime;
        AllEnemiesKilled = AllEnemiesKilled + enemiesKilled;
        AllHighMoneyCollected = AllHighMoneyCollected + moneyCollected;
        AllHighTotalScore = AllHighTotalScore < totalScore ? totalScore : AllHighTotalScore;
        AllOrbCollected = AllOrbCollected + orbCollected;

        PlayerPrefs.SetFloat(Statistics.AllShootAccuracy, AllShootAccuracy);
        PlayerPrefs.SetFloat(Statistics.AllDistanceTravelled, AllDistanceTravelled);
        PlayerPrefs.SetFloat(Statistics.AllPlayTime, AllPlayTime);
        PlayerPrefs.SetInt(Statistics.AllEnemiesKilled, AllEnemiesKilled);
        PlayerPrefs.SetInt(Statistics.AllMoneyCollected, AllHighMoneyCollected);
        PlayerPrefs.SetInt(Statistics.AllHighTotalScore, AllHighTotalScore);
        PlayerPrefs.SetInt(Statistics.AllOrbCollected, AllOrbCollected);
        PlayerPrefs.Save();
        
        Debug.Log("StatisticManager::CalculateAllTimeStatistic() AllShootAccuracy: " + AllShootAccuracy);
        Debug.Log("StatisticManager::CalculateAllTimeStatistic() AllDistanceTravelled: " + AllDistanceTravelled);
        Debug.Log("StatisticManager::CalculateAllTimeStatistic() AllPlayTime: " + AllPlayTime);
        Debug.Log("StatisticManager::CalculateAllTimeStatistic() AllEnemiesKilled: " + AllEnemiesKilled);
        Debug.Log("StatisticManager::CalculateAllTimeStatistic() AllHighMoneyCollected: " + AllHighMoneyCollected);
        Debug.Log("StatisticManager::CalculateAllTimeStatistic() AllHighTotalScore: " + AllHighTotalScore);
    }

    public void showStatistic()
    {
        statisticGame.ActivateStatisticMenu();
    }

    private void Update() {
        if (playerHealth.currentHealth <= 0 && !isgameOver)
        {
            Debug.Log("StatisticManager::Update() Player is dead. Show statistic." + ToString());
            UpdateData();
            CalculateAllTimeStatistic();
            showStatistic();
            isgameOver = true;
        }
    }

        public override string ToString()
        {
            return "ShootAccuracy: " + shootAccuracy + "\n" +
                "DistanceTravelled: " + distanceTravelled + "\n" +
                "PlayTime: " + playTime + "\n" +
                "EnemiesKilled: " + enemiesKilled + "\n" +
                "MoneyCollected: " + moneyCollected + "\n" +
                "TotalScore: " + totalScore;
        }
    }

} // namespace CompleteProject