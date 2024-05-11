// LoadManager.cs
// MelakukanSavePlayer
using System.Collections.Generic;
using UnityEngine;

namespace CompleteProject
{
public class LoadManager : SaveLoad
{
    public static LoadManager instance;

    private void Awake()
    {
        if (instance == null)
        {
#if UNITY_EDITOR
            dirPath = Application.dataPath + "/SaveGame";
#else
            dirPath = Application.persistentDataPath;
#endif
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public new GameData LoadPlayer(string profileId)
    {
        
        GameData gameData = base.LoadPlayer(profileId);
        
        /* 
            // [Header("Only Slot Data")]
            public string slotName;
            public string sceneName;
            public string playerName;
            public string saveTime;

            // [Header("Used Game Data")]
            public int money;
            public int score;
            public string playTime;
            public float accuracy;
            public float distanceTravelled;
            public int enemiesKilled;

            public Dictionary<string, int> pets = new Dictionary<string, int>();
            */

        MoneyManager.money = gameData.money;
        ScoreManager.score = gameData.score;

        PlayerPrefs.SetString("PlayerName", gameData.playerName);
        PlayerPrefs.SetInt(Statistics.MoneyCollected, gameData.money);
        PlayerPrefs.SetInt(Statistics.TotalScore, gameData.score);
        PlayerPrefs.SetFloat(Statistics.PlayTime, gameData.playTime);
        PlayerPrefs.SetFloat(Statistics.ShootAccuracy, gameData.accuracy);
        PlayerPrefs.SetFloat(Statistics.DistanceTravelled, gameData.distanceTravelled);
        PlayerPrefs.SetInt(Statistics.EnemiesKilled, gameData.enemiesKilled);

        foreach (KeyValuePair<string, int> pet in gameData.pets)
        {
            PlayerPrefs.SetInt(pet.Key, pet.Value);
        }
        
        return gameData;
    }
}

} // namespace CompleteProject
