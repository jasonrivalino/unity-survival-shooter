using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CompleteProject
{
    public class SpeedOrbDropped : Orb
    {
        PlayerMovement playerMovement;
        new void Awake()
        {
            // Setting up the references
            base.Awake();
            playerMovement = player.GetComponentInChildren<PlayerMovement>();
        }

        void OnCollisionEnter(UnityEngine.Collision collision)
        {
            if (!isPicked) {    
                // If the entering collider is the player 
                if (collision.gameObject == player)
                {
                    isPicked = true;
                    orbPickedAudio.Play();
                    // Set speedup for player
                    playerMovement.SpeedUp();

                    // Update statistics
                    if (PlayerPrefs.HasKey("numSpeedOrbPicked"))
                    {
                        PlayerPrefs.SetInt("numSpeedOrbPicked", PlayerPrefs.GetInt("numSpeedOrbPicked") + 1);
                    }
                    else 
                    {
                        PlayerPrefs.SetInt("numSpeedOrbPicked", 1);
                    }


                    base.Dissapear();
                }
            }
        }
    }
}