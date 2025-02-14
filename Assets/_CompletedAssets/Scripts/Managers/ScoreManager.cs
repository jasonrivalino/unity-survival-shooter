﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Events;

namespace CompleteProject
{
    public class ScoreManager : MonoBehaviour
    {
        public static int score;        // The player's score.

        public int scoreObjective;

        public UnityEvent interactAction;

        public GameObject missionAccomplishedText;

        public GameObject player;

        public GameObject enemyManager;

        public AudioClip scoreSoundClip;
        AudioSource scoreAudio;
        public GameObject shotgun;
        public GameObject gunbarrelEnd;
        public GameObject katana;


        Text text;                      // Reference to the Text component.


        void Awake()
        {
            // Set up the reference.
            text = GetComponent<Text>();

            // Reset the score.
            score = 0;
            scoreAudio = this.gameObject.AddComponent<AudioSource>();

        }


        void Update()
        {
            // Set the displayed text to be the word "Score" followed by the score value.
            text.text = "Score: " + score;
            if (score >= scoreObjective)
            {
                missionAccomplishedText.SetActive(true);
                scoreAudio.volume = 0.15f;
                scoreAudio.PlayOneShot(scoreSoundClip);
                PlayerMovement playerMovement = player.GetComponentInChildren<PlayerMovement>();
                PlayerWeaponManager playerWeaponManager = player.GetComponentInChildren<PlayerWeaponManager>();
                EnemyManager enemy = enemyManager.GetComponentInChildren<EnemyManager>();
                enemy.DisableEnemy();
                playerMovement.Stop();
                shotgun.SetActive(false);
                gunbarrelEnd.SetActive(false);
                katana.SetActive(false);
                StartCoroutine(ActivateFunction());
            }
        }

        public bool IsScoreReached()
        {
            return score >= scoreObjective;
        }

        IEnumerator ActivateFunction()
        {
            yield return new WaitForSeconds(3);
            interactAction.Invoke();

        }
    }
}