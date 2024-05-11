using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CompleteProject
{
    public class GameManager : MonoBehaviour
    {
        string playerName;

        void Awake()
        {
            // Set from PlayerPrefs this attr if it exists:
            playerName = PlayerPrefs.GetString("playerName", "Player");            
            DontDestroyOnLoad(this.gameObject);
        }

        void SetPlayerName(string name)
        {
            playerName = name;
            PlayerPrefs.SetString("playerName", name);
        }
    }

}
    

