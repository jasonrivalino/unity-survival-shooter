using CompleteProject;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace CompleteProject
{
    public class Heal : MonoBehaviour
    {
        AudioSource healAudio;
        public AudioClip healClip;
        public float healAmount;
        public float healRange;
        Transform player;
        PlayerHealth playerHealth;      // Reference to the player's health.
        PetHealth petHealth;        // Reference to this pet's health.
        void Awake()
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
            playerHealth = player.gameObject.GetComponent<PlayerHealth>();
            petHealth = GetComponent<PetHealth>();
            healAudio = player.GetComponent<AudioSource>();
            StartCoroutine(IncreasePlayerHealth());
        }

        IEnumerator IncreasePlayerHealth()
        {
            yield return new WaitForSeconds(4f);
            while (petHealth.currentHealth > 0 && playerHealth.currentHealth > 0)
            {
                if (Vector3.Distance(player.position, transform.position) <= healRange)
                {
                    healAudio.PlayOneShot(healClip);
                    playerHealth.Heal(healAmount);
                    Debug.Log("Player healed. current health: " + playerHealth.currentHealth);
                    yield return new WaitForSeconds(2f);
                }
                else
                {
                    yield return new WaitForSeconds(0.1f);
                }
            }
        }

    }
}