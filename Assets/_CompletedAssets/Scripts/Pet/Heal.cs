using CompleteProject;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CompleteProject
{
    public class Heal : MonoBehaviour
    {
        int healAmount = 10;
        Transform player;
        PlayerHealth playerHealth;      // Reference to the player's health.
        PetHealth petHealth;        // Reference to this pet's health.
        private Animator animator;
        void Awake()
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
            animator = this.GetComponent<Animator>();
            playerHealth = player.gameObject.GetComponent<PlayerHealth>();
            petHealth = GetComponent<PetHealth>();
            StartCoroutine(IncreasePlayerHealth());
        }

        IEnumerator IncreasePlayerHealth()
        {
            while (petHealth.currentHealth > 0 && playerHealth.currentHealth > 0)
            {
                playerHealth.currentHealth += healAmount;
                if (playerHealth.currentHealth > 100)
                {
                    playerHealth.currentHealth = 100;
                }
                Debug.Log("Player healed. current health: " + playerHealth.currentHealth);

                yield return new WaitForSeconds(2f);
            }
        }

    }
}