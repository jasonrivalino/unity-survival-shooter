using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;

namespace CompleteProject 
{ 
    public class Katana : Weapon
    {
        public Transform ownerTransform;

        bool isSlashing = false;
        AudioSource slashAudio;
        ParticleSystem slashParticles;


        void Awake()
        {
            // set up the references
            weapon = gameObject;
            if (isPlayerOwner){
                UnUseWeapon();
            } else {
                UseWeapon();
            }
            slashAudio = GetComponent<AudioSource>();
            slashParticles = GetComponentInChildren<ParticleSystem>();
        }

        // Update is called once per frame
        new void Update()
        {
            base.Update();
            if (isUsed && isPlayerOwner)
            {

                #if !MOBILE_INPUT
                // If the Fire1 button is being press and it's time to slash...
                if (Input.GetButton("Fire1") && timer >= base.timeBetweenAttack && Time.timeScale != 0)
                {
                    // ... slash the katana.
                    Slash();
                }
                #else
                            // If there is input on the shoot direction stick and it's time to fire...
                            // if ((CrossPlatformInputManager.GetAxisRaw("Mouse X") != 0 || CrossPlatformInputManager.GetAxisRaw("Mouse Y") != 0) && timer >= timeBetweenBullets)
                            // {
                            //     // ... shoot the gun
                            //     Shoot();
                            // }
                #endif

            } else if (!isPlayerOwner) {
                if (timer >= timeBetweenAttack && Time.timeScale != 0)
                {
                    Slash();
                }
            }

            if (isSlashing) 
            {
                // Rotate the owner body
                float yRotationDegree;
                if (timer > timeBetweenAttack) {
                    isSlashing = false;
                    yRotationDegree = ((Time.deltaTime - (timer - timeBetweenAttack)) / timeBetweenAttack) * (-180f);
                    ownerTransform.Rotate(0, yRotationDegree, 0);
                    ownerTransform.Rotate(0, 90, 0);
                } else { 
                    yRotationDegree = (Time.deltaTime/timeBetweenAttack) * (-180f);
                    ownerTransform.Rotate(0, yRotationDegree, 0);
                }
            }
        }
        private void OnTriggerEnter(Collider other)
        {
            // Debug.Log("Trigger with katana");

            if (isSlashing)
            {
                /// Try and find an EnemyHealth script on the gameobject hit.
                if (isPlayerOwner)
                {
                    Debug.Log("player tusuk enemy");
                    // If the EnemyHealth component exist...
                    if (other.gameObject.TryGetComponent<EnemyHealth>(out var enemyHealth))
                    {
                        Debug.Log("Damage Katana: " + (damagePerAttack * (1f + powerUp)));
                        // ... the enemy should take damage.
                        enemyHealth.TakeDamage(damagePerAttack * (1f + powerUp), other.transform.position);

                    }
                }
                else // If the user is enemy
                {
                    // If the PlayerHealth component exist...
                    Debug.Log("player kena tusuk");
                    if (other.gameObject.TryGetComponent<PlayerHealth>(out var playerHealth))
                    {
                        Debug.Log("Damage Katana ke player: " + (damagePerAttack * (1f + powerUp)));
                        // ... the player should take damage.
                        playerHealth.TakeDamage(damagePerAttack * (1f + powerUp));

                    }
                }
            }
        }

        void Slash() {
            // Reset the timer.
            timer = 0f;
            isSlashing = true;
            
            // Play the katana slash audioclip
            slashAudio.Play();
            // Debug.Log("Slash");

            // print(isSlashing);

            // Katana Animation

            // Rotate Owner Body to Slashing Position
            ownerTransform.Rotate(0, 90, 0);

            // Stop the particles from playing if they were, then start the particles.
            slashParticles.Stop();
            slashParticles.Play();


        }
    }
}
