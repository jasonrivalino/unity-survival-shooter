﻿using UnityEngine;
using UnitySampleAssets.CrossPlatformInput;

namespace CompleteProject
{
    public class Riffle : Weapon
    {
        public float range = 100f;                      // The distance the gun can fire.

        Ray shootRay = new Ray();                       // A ray from the gun end forwards.
        RaycastHit shootHit;                            // A raycast hit to get information about what was hit.
        int shootableMask;                              // A layer mask so the raycast only hits things on the shootable layer.
        ParticleSystem gunParticles;                    // Reference to the particle system.
        LineRenderer gunLine;                           // Reference to the line renderer.
        AudioSource gunAudio;                           // Reference to the audio source.
        Light gunLight;                                 // Reference to the light component.
        public Light faceLight;								// Duh
        float effectsDisplayTime = 0.2f;                // The proportion of the timeBetweenBullets that the effects will display for.        
        void Awake ()
        {
            // Create a layer mask for the Shootable layer.
            shootableMask = LayerMask.GetMask ("Shootable");

            // Set up the references.
            gunParticles = GetComponent<ParticleSystem> ();
            gunLine = GetComponent <LineRenderer> ();
            gunAudio = GetComponent<AudioSource> ();
            gunLight = GetComponent<Light> ();

            weapon = UnityEngine.GameObject.FindGameObjectWithTag("Riffle");
            faceLight = GetComponentInChildren<Light> ();
        }


        new void Update ()
        {
            base.Update();

            if (isUsed) { 
            
                #if !MOBILE_INPUT
                            // If the Fire1 button is being press and it's time to fire...
			                if(Input.GetButton ("Fire1") && timer >= timeBetweenAttack && Time.timeScale != 0)
                            {
                                // ... shoot the gun.
                                Shoot ();
                            }
                #else
                            // If there is input on the shoot direction stick and it's time to fire...
                            if ((CrossPlatformInputManager.GetAxisRaw("Mouse X") != 0 || CrossPlatformInputManager.GetAxisRaw("Mouse Y") != 0) && timer >= timeBetweenBullets)
                            {
                                // ... shoot the gun
                                Shoot();
                            }
                #endif
            
            }

            // If the timer has exceeded the proportion of timeBetweenBullets that the effects should be displayed for...
            if(timer >= timeBetweenAttack * effectsDisplayTime)
            {
                // ... disable the effects.
                DisableEffects ();
            }
        }


        public void DisableEffects ()
        {
            // Disable the line renderer and the light.
            gunLine.enabled = false;
			faceLight.enabled = false;
            gunLight.enabled = false;
        }
        
        void Shoot ()
        {
            Debug.Log("Damage: "+(damagePerAttack*(1f + powerUp)));
            // Reset the timer.
            timer = 0f;

            // Play the gun shot audioclip.
            gunAudio.Play ();

            // Enable the lights.
            gunLight.enabled = true;
			faceLight.enabled = true;

            // Stop the particles from playing if they were, then start the particles.
            gunParticles.Stop ();
            gunParticles.Play ();

            // Enable the line renderer and set it's first position to be the end of the gun.
            gunLine.enabled = true;
            gunLine.SetPosition (0, transform.position);

            // Set the shootRay so that it starts at the end of the gun and points forward from the barrel.
            shootRay.origin = transform.position;
            shootRay.direction = transform.forward;

            // Perform the raycast against gameobjects on the shootable layer and if it hits something...
            if(Physics.Raycast (shootRay, out shootHit, range, shootableMask))
            {
                PetHealth petHealth = shootHit.collider.GetComponent<PetHealth>();
                if (isPlayerOwner)
                {
                    // Try and find an EnemyHealth script on the gameobject hit.
                    EnemyHealth enemyHealth = shootHit.collider.GetComponent<EnemyHealth>();
                    // if miss just add 1 to totalShootCount
                    if (enemyHealth == null && petHealth == null)
                    {
                        totalShootCount += 1;
                    }

                    // If the EnemyHealth component exist...
                    if (enemyHealth != null)
                    {
                        // ... the enemy should take damage.
                        enemyHealth.TakeDamage(damagePerAttack * (1f + powerUp), shootHit.point);
                        hitCount += 1;
                        totalShootCount += 1;
                    }
                    if (petHealth != null && shootHit.collider.gameObject.tag == "PetEnemy")
                    {

                        petHealth.TakeDamage(damagePerAttack * (1f + powerUp));
                    }
                }
                else // If the user is enemy
                { 
                    PlayerHealth playerHealth = shootHit.collider.GetComponent<PlayerHealth>();
                    if (playerHealth != null)
                    {
                        // .. the player should take damage
                        playerHealth.TakeDamage(damagePerAttack * (1f + powerUp));
                    }
                    if (petHealth != null && shootHit.collider.gameObject.tag == "Pet")
                    {
                        petHealth.TakeDamage(damagePerAttack * (1f + powerUp));
                    }
                }

                // Set the second position of the line renderer to the point the raycast hit.
                gunLine.SetPosition (1, shootHit.point);
            }
            // If the raycast didn't hit anything on the shootable layer...
            else
            {
                // ... set the second position of the line renderer to the fullest extent of the gun's range.
                gunLine.SetPosition (1, shootRay.origin + shootRay.direction * range);
            }
        }
    }
}