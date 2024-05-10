// SaveManager.cs
// MelakukanSavePlayer
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace CompleteProject
{
    public class SaveManager : MonoBehaviour
    {
        public static SaveManager instance;

        public string dirPath {get; private set;}

        public string fileName {get; private set;} = "PData.dat";
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


        public void SavePlayer(GameData gameData, string profileId)
        {
            DataFileHandler.SavePlayer(gameData, dirPath, fileName, profileId);
        }

        public GameData LoadPlayer(string profileId)
        {
            /*
            Data yang harus diload di sini:
            - level / scene yang nanti dimainkan v
            - nama player v
            - uang player v
            - score player v
            - waktu save 
            - waktu bermain
            - slot name
            */
            GameData gameData = DataFileHandler.LoadPlayer(dirPath, fileName, profileId);   
            
            PlayerPrefs.SetString("playerName", gameData.playerName);
            
            MoneyManager.money = gameData.money;
            ScoreManager.score = gameData.score;

            PlayerPrefs.SetString("startTime", gameData.saveTime);
            PlayerPrefs.SetString("playedTime", gameData.playTime);
            
            Debug.Log("SaveManager::LoadPlayer() " + gameData.playerName + " loaded");
            return gameData;
        }

        public void DeletePlayer(string profileId)
        {
            DataFileHandler.DeletePlayer(dirPath, fileName, profileId);
        }
    }
}
