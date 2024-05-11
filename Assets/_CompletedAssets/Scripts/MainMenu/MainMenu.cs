using UnityEngine;


public class MainMenu : MonoBehaviour
{
    public void QuitGame()
    {
#if !UNITY_EDITOR
        Application.Quit();
#else
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    public static void ResetGameStatData()
    {
        PlayerPrefs.SetFloat(Statistics.ShootAccuracy, 0);
        PlayerPrefs.SetFloat(Statistics.DistanceTravelled, 0);
        PlayerPrefs.SetFloat(Statistics.PlayTime, 0);
        PlayerPrefs.SetInt(Statistics.EnemiesKilled, 0);
        PlayerPrefs.SetInt(Statistics.MoneyCollected, 0);
        PlayerPrefs.SetInt(Statistics.TotalScore, 0);
        PlayerPrefs.SetInt(Statistics.OrbCollected, 0);
        PlayerPrefs.Save();
    }

    public static void ResetCheatData()
    {
        /* 
        "NoDamage" : Int
        "OneHitKill" : Int
        "2xSpeedUp" : Int
        "Motherlode" : Int
        "NoDamagePet" : Int
         */
        PlayerPrefs.DeleteKey(Cheats.NoDamage);
        PlayerPrefs.DeleteKey(Cheats.OneHitKill);
        PlayerPrefs.DeleteKey(Cheats.SpeedUp);
        PlayerPrefs.DeleteKey(Cheats.Motherlode);
        PlayerPrefs.DeleteKey(Cheats.NoDamagePet);
        PlayerPrefs.Save();
    }

    public static void ResetOrbData()
    {
        PlayerPrefs.DeleteKey("numDamageOrbPicked");
        PlayerPrefs.DeleteKey("numSpeedOrbPicked");
        PlayerPrefs.DeleteKey("numHealthOrbPicked");
        PlayerPrefs.Save();
    }

    public static void ResetPetsData()
    {
        /* 
            "rabbit"
            "mushroom"
            "ghost"
            "dog"
            "cactus"
            "bomb"
         */

        PlayerPrefs.DeleteKey(Pets.Rabbit);
        PlayerPrefs.DeleteKey(Pets.Mushroom);
        PlayerPrefs.DeleteKey(Pets.Ghost);
        PlayerPrefs.DeleteKey(Pets.Dog);
        PlayerPrefs.DeleteKey(Pets.Cactus);
        PlayerPrefs.DeleteKey(Pets.Bomb);
        PlayerPrefs.Save();
    }
}
