using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CompleteProject
{
    public class HealthOrbDropped : Orb
    {
        PlayerHealth playerHealth;
        new void Awake()
        {
            // Setting up the references
            base.Awake();
            playerHealth = player.GetComponentInChildren<PlayerHealth>();
        }

        void OnCollisionEnter(UnityEngine.Collision collision)
        {
            // If the entering collider is the player 
            if (collision.gameObject == player)
            {

                // Picked only if current health is < startinghealth
                if (playerHealth.currentHealth < playerHealth.startingHealth) {
                    Debug.Log(orbPickedAudio);
                    orbPickedAudio.Play();
                    playerHealth.Heal();
                    base.Dissapear();
                }
            }
        }
        
    }
}
