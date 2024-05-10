using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public string slotName;
    public string playerName;
    public int money;
    public int score;
    public string sceneName;
    public string saveTime;
    public string playTime;
    // public string[] pets;

    public GameData(string slotName, string playerName, int money, int score, string sceneName, string saveTime, string playTime)
    {
        this.slotName = slotName;
        this.playerName = playerName;
        this.money = money;
        this.score = score;
        this.sceneName = sceneName;
        this.saveTime = saveTime;
        this.playTime = playTime;
        // this.pets = pets;
    }

    public string toString()
    {
        return "Slot Name: " + slotName + "\n" +
               "Player Name: " + playerName + "\n" +
               "Money: " + money + "\n" +
               "Score: " + score + "\n" +
               "Scene Name: " + sceneName + "\n" +
               "Save Time: " + saveTime + "\n" +
               "Play Time: " + playTime + "\n";
            //    "Pets: " + pets;
    }
}
