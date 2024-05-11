using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class FileHandler
{
    public static void SavePlayer(int data) 
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.fun";
        FileStream stream = new FileStream(path, FileMode.Create);

        // new

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static void LoadPlayer() {

    }

}