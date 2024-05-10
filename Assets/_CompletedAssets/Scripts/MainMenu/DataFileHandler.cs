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

        if (!Directory.Exists(Path.GetDirectoryName(path)))
        {
            Directory.CreateDirectory(Path.GetDirectoryName(path));
        }

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, gameData);
        stream.Close();
    }

    public static GameData LoadPlayer(string dirPath, string fileName, string profileId)
    {
        string path = Path.Combine(dirPath, profileId, fileName);
        Debug.Log("DataFileHandler::LoadPlayer() path: " + path);

        if (File.Exists (path)) 
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            GameData gameData = formatter.Deserialize(stream) as GameData;
            stream.Close();
        
            return gameData;
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
        Debug.Log("DataFileHandler::DeletePlayer() path: " + path);

        if (File.Exists (path)) 
        {
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