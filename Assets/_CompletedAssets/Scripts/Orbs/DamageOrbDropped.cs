using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CompleteProject
{
    public class DamageOrbDropped : Orb
    {
        PlayerShooting playerShooting;
        new void Awake()
        {
            // Setting up the references
            base.Awake();
            playerShooting = player.GetComponentInChildren<PlayerShooting>();
        }

        void OnCollisionEnter(UnityEngine.Collision collision)
        {
            // If the entering collider is the player 
            if (collision.gameObject == player)
            {
                // If player is not pick damageOrb 15 times or more
                if (playerShooting.powerUp < 1.5f)
                {
                    Debug.Log("Damage Orb Picked");
                    playerShooting.PowerUp();
                    GetComponent<Renderer>().enabled = false;
                    Destroy(gameObject, 1f);
                }
            }
        }
    }

}