﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

namespace CompleteProject
{
    public class PlayerHealth : MonoBehaviour
    {
        public float startingHealth = 125f;                            // The amount of health the player starts the game with.
        public float currentHealth;                                   // The current health the player has.
        public Slider healthSlider;                                 // Reference to the UI's health bar.
        public Image effectImage;                                   // Reference to an image to flash on the screen on being hurt.
        public AudioClip deathClip;                                 // The audio clip to play when the player dies.
        public float flashSpeed = 5f;                               // The speed the effectImage will fade at.

        Color healFlashColour = new(0f, 0.455f, 0f, 0.1f);     // The colour the heal effectImage is set to, to flash.
        Color damageFlashColour = new(0.455f, 0f, 0f, 0.1f);     // The colour the damaged effectImage is set to, to flash.

        Animator anim;                                              // Reference to the Animator component.
        AudioSource playerAudio;                                    // Reference to the AudioSource component.
        PlayerMovement playerMovement;                              // Reference to the player's movement.
        Riffle riffle;                              // Reference to the PlayerShooting script.
        bool isDead;                                                // Whether the player is dead.
        bool damaged;                                               // True when the player gets damaged.
        bool healed;                                                // True when the player gets healed.

        void Awake()
        {
            // Setting up the references.
            anim = GetComponent<Animator>();
            playerAudio = GetComponent<AudioSource>();
            playerMovement = GetComponent<PlayerMovement>();
            riffle = GetComponentInChildren<Riffle>();

            // set starting health based on difficulty level mudah = 100, normal = 50, sulit = 25
            
            switch (PlayerPrefs.GetString("Difficulty", "mudah"))
            {
                case "mudah":
                    startingHealth = 125f;
                    break;
                case "normal":
                    startingHealth = 100f;
                    break;
                case "sulit":
                    startingHealth = 50f;
                    break;
                default:
                    startingHealth = 125f;
                    break;
            }
            
            // Set the initial health of the player.
            currentHealth = startingHealth;


        }


        void Update()
        {
            // If the player has just been damaged...
            if (damaged)
            {
                // ... set the colour of the effectImage to the damaged flash colour.
                effectImage.color = damageFlashColour;
                // Reset the damaged flag.
                damaged = false;
            }
            else if (healed)
            {
                // ... set the colour of the effectImage to the healed flash colour.
                effectImage.color = healFlashColour;
                // Reset the healed flag.
                healed = false;
            }
            // Otherwise...
            else
            {
                // ... transition the colour back to clear.
                effectImage.color = Color.Lerp(effectImage.color, Color.clear, flashSpeed * Time.deltaTime);
            }
        }

        public void Heal(float amount = 0)
        {
            healed = true;
            if (amount > 0)
            {
                currentHealth += amount;
                if (currentHealth > startingHealth)
                {
                    currentHealth = startingHealth;
                }
            }
            else
            {
                if ((currentHealth / startingHealth) > 0.8f)
                {
                    currentHealth = startingHealth;
                }
                else
                {
                    currentHealth += 0.2f * startingHealth;
                }
            }
            healthSlider.value = currentHealth;
        }

        public void TakeDamage(float amount)
        {
            // if NoDamage cheat is not activated, take the damage
            if (!PlayerPrefs.HasKey("NoDamage"))
            {
                // Set the damaged flag so the screen will flash.
                damaged = true;

                // Reduce the current health by the damage amount.
                currentHealth -= amount;

                // Set the health bar's value to the current health.
                healthSlider.value = currentHealth;

                // Play the hurt sound effect.
                playerAudio.Play();

                // Play the hurt sound effect.
                playerAudio.Play();

                // If the player has lost all it's health and the death flag hasn't been set yet...
                if (currentHealth <= 0 && !isDead)
                {
                    // ... it should die.
                    Death();
                }
            }
        }


        void Death()
        {
            // Set the death flag so this function won't be called again.
            isDead = true;

            // Turn off any remaining shooting effects.
            riffle.DisableEffects();

            // Tell the animator that the player is dead.
            anim.SetTrigger("Die");

            // Set the audiosource to play the death clip and play it (this will stop the hurt sound from playing).
            playerAudio.PlayOneShot(deathClip);

            // Turn off the movement and shooting scripts.
            playerMovement.enabled = false;
            riffle.enabled = false;
        }


        // public void RestartLevel()
        // {
        //     // Reload the level that is currently loaded.
        //     SceneManager.LoadScene(0);
        // }
    }
}