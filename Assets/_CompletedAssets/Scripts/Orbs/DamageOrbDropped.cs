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
            if (!isPicked)
            {  
                // If the entering collider is the player 
                if (collision.gameObject == player)
                {
                    // If player is not pick damageOrb 15 times or more
                    if (weaponManager.powerUp < 1.5f)
                    {
                        isPicked = true;
                        orbPickedAudio.Play();
                        weaponManager.PowerUp();

                        // Update statistics
                        if (PlayerPrefs.HasKey("numDamageOrbPicked"))
                        {
                            PlayerPrefs.SetInt("numDamageOrbPicked", PlayerPrefs.GetInt("numDamageOrbPicked") + 1);
                        }
                        else
                        {
                            PlayerPrefs.SetInt("numDamageOrbPicked", 1);
                        }

                        base.Dissapear();
                    }
                }
            }
        }
    }

}