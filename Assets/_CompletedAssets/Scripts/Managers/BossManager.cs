using System.Collections;
using System.Collections.Generic;
using CompleteProject;
using UnityEngine;
using UnityEngine.Events;

namespace CompleteProject
{
    public class BossManager : MonoBehaviour
    {
        public GameObject boss;

        public StatisticManager statisticManager;
        EnemyHealth bossHealth;
        public UnityEvent interactAction;

        public GameObject missionAccomplishedText;

        public GameObject player;

        public GameObject enemyManager;
        public GameObject shotgun;
        public GameObject gunbarrelEnd;
        public GameObject katana;

        public AudioClip scoreSoundClip;
        AudioSource scoreAudio;
        void Awake()
        {
            bossHealth = boss.GetComponent<EnemyHealth>();
            scoreAudio = this.gameObject.AddComponent<AudioSource>();
        }

        // Update is called once per frame
        void Update()
        {
            if (!(bossHealth.currentHealth > 0))
            {
                missionAccomplishedText.SetActive(true);
                scoreAudio.volume = 0.15f;
                scoreAudio.PlayOneShot(scoreSoundClip);
                PlayerMovement playerMovement = player.GetComponentInChildren<PlayerMovement>();
                PlayerWeaponManager playerWeaponManager = player.GetComponentInChildren<PlayerWeaponManager>();
                EnemyManager enemy = enemyManager.GetComponentInChildren<EnemyManager>();
                playerMovement.Stop();
                shotgun.SetActive(false);
                gunbarrelEnd.SetActive(false);
                katana.SetActive(false);
                statisticManager.UpdateData();
                statisticManager.CalculateAllTimeStatistic();
                statisticManager.showStatistic();
                StartCoroutine(ActivateFunction());
            }
        }

        IEnumerator ActivateFunction()
        {
            yield return new WaitForSeconds(3);
            interactAction.Invoke();
        }
    }
}

