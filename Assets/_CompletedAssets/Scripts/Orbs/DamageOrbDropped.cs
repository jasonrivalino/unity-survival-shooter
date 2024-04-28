using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CompleteProject
{
    public class DamageOrbDropped : Orb
    {
        PlayerWeaponManager weaponManager;
        new void Awake()
        {
            // Setting up the references
            base.Awake();
            weaponManager = player.GetComponent<PlayerWeaponManager>();
        }

        void OnCollisionEnter(UnityEngine.Collision collision)
        {
            // If the entering collider is the player 
            if (collision.gameObject == player)
            {
                // If player is not pick damageOrb 15 times or more
                if (weaponManager.powerUp < 1.5f)
                {
                    Debug.Log(orbPickedAudio);
                    orbPickedAudio.Play();
                    weaponManager.PowerUp();
                    Dissapear();
                }
            }
        }
    }

}