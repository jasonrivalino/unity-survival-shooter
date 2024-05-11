// SaveManager.cs
// MelakukanSavePlayer
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace CompleteProject
{
    public class SaveLoad : MonoBehaviour
    {
        public string dirPath = Application.dataPath + "/SaveGame";

        public string fileName = "PData.dat";
        
        private void Awake()
        {

#if UNITY_EDITOR
            dirPath = Application.dataPath + "/SaveGame";
#else
            dirPath = Application.persistentDataPath;
#endif

        }

        
        public void SavePlayer(GameData gameData, string profileId)
        {
            DataFileHandler.SavePlayer(gameData, dirPath, fileName, profileId);
        }

        public GameData LoadPlayer(string profileId)
        {
            GameData gameData = DataFileHandler.LoadPlayer(dirPath, fileName, profileId);   
            return gameData;
        }

        public void DeletePlayer(string profileId)
        {
            DataFileHandler.DeletePlayer(dirPath, fileName, profileId);
        }


    }
}
