using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

namespace CompleteProject
{
    public class GameOverManager : MonoBehaviour
    {
        public PlayerHealth playerHealth;       // Reference to the player's health.


        Animator anim;                          // Reference to the animator component.
        public Text pointsText;
        public Text countdownText;

        public GameObject gameOverScreen;


        void Awake()
        {
            // Set up the reference.
            anim = GetComponent<Animator>();
        }


        void Update()
        {
            // if (PlayerPrefs.HasKey("numSpeedOrbPicked"))
            // {
            //     Debug.Log("Number of Speed Orb Picked:" + PlayerPrefs.GetInt("numSpeedOrbPicked").ToString());
            // }

            // if (PlayerPrefs.HasKey("numHealthOrbPicked"))
            // {
            //     Debug.Log("Number of Health Orb Picked:" + PlayerPrefs.GetInt("numHealthOrbPicked").ToString());
            // }

            // if (PlayerPrefs.HasKey("numDamageOrbPicked"))
            // {
            //     Debug.Log("Number of Damage Orb Picked:" + PlayerPrefs.GetInt("numDamageOrbPicked").ToString());
            // }

            // If the player has run out of health...
            if (playerHealth.currentHealth <= 0)
            {
                StartCoroutine(SetupGameOver());
                // ... tell the animator the game is over.
                this.enabled = false;
            }
        }

        IEnumerator SetupGameOver()
        {
            yield return new WaitForSeconds(2);
            gameOverScreen.SetActive(true);
            // stat score here
            pointsText.text = "score: " + ScoreManager.score.ToString();
            StopAllCoroutines();
            StartCoroutine(GameOverCountdown());
        }

        IEnumerator GameOverCountdown()
        {
            int countdownTime = 11;
            while (countdownTime > 0)
            {
                countdownText.text = "Returning to main menu in " + (countdownTime - 1).ToString();
                yield return new WaitForSeconds(1f);
                countdownTime--;
            }

            SceneManager.LoadScene(0);
        }
    }
}