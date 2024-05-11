// DataFileHandler.cs
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class DataFileHandler 
{
    public static void SavePlayer(GameData gameData, string dirPath, string fileName, string profileId)
    {
        if(profileId == null) 
        {
            Debug.LogWarning("DataFileHandler::SavePlayer() profileId is null");
            return;
        }

        string path = Path.Combine(dirPath, profileId, fileName);
        Debug.Log("DataFileHandler::SavePlayer() path: " + path);

        if (!Directory.Exists(Path.Combine(dirPath, profileId)))
        {
            Directory.CreateDirectory(Path.Combine(dirPath, profileId));
        }

        BinaryFormatter formatter = new BinaryFormatter();
        using (FileStream stream = new FileStream(path, FileMode.Create))
        {
            try
            {
                formatter.Serialize(stream, gameData);
            }
            catch (Exception e)
            {
                Debug.LogError($"DataFileHandler::SavePlayer() Error while saving player data: {e}");
            }
        }
    }

    public static GameData LoadPlayer(string dirPath, string fileName, string profileId)
    {
        string path = Path.Combine(dirPath, profileId, fileName);

        if (File.Exists(path)) 
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream stream = new FileStream(path, FileMode.Open))
            {
                try
                {
                    GameData gameData = formatter.Deserialize(stream) as GameData;
                    Debug.Log("DataFileHandler::LoadPlayer() gameData: " + gameData.ToString());
                    return gameData;
                }
                catch (Exception e)
                {
                    Debug.LogError($"DataFileHandler::LoadPlayer() Error while loading player data: {e}");
                    return null;
                }
            }
        }
        else 
        {
            Debug.LogWarning("DataFileHandler::LoadPlayer() File not found in " + path);
            return null;
        }

    }

    public static void DeletePlayer(string dirPath, string fileName, string profileId)
    {
        string path = Path.Combine(dirPath, profileId, fileName);

        if (File.Exists (path)) 
        {
            Debug.Log("DataFileHandler::DeletePlayer() Deleting file in " + path);
            File.Delete(path);
        }
        else 
        {
            Debug.LogWarning("DataFileHandler::DeletePlayer() File not found in " + path);
        }
    }

    public static Dictionary<string, GameData> LoadAllPlayers(string dirPath, string fileName)
    {
        Dictionary<string, GameData> players = new Dictionary<string, GameData>();

        IEnumerable<DirectoryInfo> directories = new DirectoryInfo(dirPath).EnumerateDirectories();

        foreach (DirectoryInfo directory in directories)
        {
            string profileId = directory.Name;
            
            string path = Path.Combine(dirPath, profileId, fileName);

            if (!File.Exists(path))
            {
                Debug.LogWarning("DataFileHandler::LoadAllPlayers() File not found in " + path);
                continue;
            }

            GameData gameData = LoadPlayer(dirPath, fileName, profileId);

            if (gameData != null)
            {
                players.Add(profileId, gameData);
            }
            else
            {
                Debug.LogWarning("DataFileHandler::LoadAllPlayers() gameData is null");
            }
        }

        return players;
    }
}