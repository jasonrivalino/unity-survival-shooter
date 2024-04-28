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
            // If the entering collider is the player 
            if (collision.gameObject == player)
            {
                Debug.Log("Speed orb Picked");
                // Set speedup for player
                playerMovement.SpeedUp();
                base.Dissapear();
            }
        }
    }
}