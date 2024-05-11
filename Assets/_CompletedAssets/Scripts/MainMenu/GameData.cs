using System.Collections.Generic;

[System.Serializable]
public class GameData
{
    // [Header("Only Slot Data")]
    public string slotName;
    public string sceneName;
    public string playerName;
    public string saveTime;

    // [Header("Used Game Data")]
    public int money;
    public int score;
    public float playTime;
    public float accuracy;
    public float distanceTravelled;
    public int enemiesKilled;
    public int orbCollected;

    public Dictionary<string, int> pets = new Dictionary<string, int>();

    public GameData(string slotName, string sceneName, string playerName, string saveTime, int money, int score, float playTime, float accuracy, float distanceTravelled, int enemiesKilled, int orbCollected, Dictionary<string, int> pets)
    {
        this.slotName = slotName;
        this.sceneName = sceneName;
        this.playerName = playerName;
        this.saveTime = saveTime;
        this.money = money;
        this.score = score;
        this.playTime = playTime;
        this.accuracy = accuracy;
        this.distanceTravelled = distanceTravelled;
        this.enemiesKilled = enemiesKilled;
        this.orbCollected = orbCollected;
        this.pets = pets;
    }

    
    override public string ToString()
    {
        return "GameData:\n" + 
            "slotName: " + slotName + "\n" +
            "sceneName: " + sceneName + "\n" +
            "playerName: " + playerName + "\n" +
            "saveTime: " + saveTime + "\n" +
            "money: " + money + "\n" +
            "score: " + score + "\n" +
            "playTime: " + playTime + "\n" +
            "accuracy: " + accuracy + "\n" +
            "distanceTravelled: " + distanceTravelled + "\n" +
            "enemiesKilled: " + enemiesKilled + "\n" +
            "pets: " + pets;
    }
}
