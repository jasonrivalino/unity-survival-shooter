using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CompleteProject
{
    public class Shotgun : Weapon
    {
        public float range = 10f;                      // The distance the gun can fire.
        public int bulletsPerShoot = 7;
        public float inaccuracyDistance = 2f;
        public GameObject shotgunLine;

        float maxShotgunLineLength;
        Ray shootRay = new Ray();                       // A ray from the gun end forwards.
        RaycastHit shootHit;                            // A raycast hit to get information about what was hit.
        int shootableMask;                              // A layer mask so the raycast only hits things on the shootable layer.
        ParticleSystem gunParticles;                    // Reference to the particle system.
        AudioSource gunAudio;                           // Reference to the audio source.
        Light gunLight;                                 // Reference to the light component.
        public Light faceLight;								// Duh
        float effectsDisplayTime = 0.2f;                // The proportion of the timeBetweenBullets that the effects will display for.

        void Awake()
        {
            // Create a layer mask for the Shootable layer.
            shootableMask = LayerMask.GetMask("Shootable");

            // Set up the references.
            gunParticles = GetComponent<ParticleSystem>();
            gunAudio = GetComponent<AudioSource>();
            gunLight = GetComponent<Light>();
            faceLight = GetComponentInChildren<Light>();

            // Setup the physical 3D reference
            weapon= UnityEngine.GameObject.FindGameObjectsWithTag("Shotgun")[0];
            weapon2 = UnityEngine.GameObject.FindGameObjectsWithTag("Shotgun")[1];

            // UnUse weapon at init state of game
            UnUseWeapon();

            // Count maxShotgunLineLength for damage Count
            maxShotgunLineLength = Mathf.Sqrt(Mathf.Pow(range+inaccuracyDistance, 2) + Mathf.Pow(inaccuracyDistance, 2) + Mathf.Pow(inaccuracyDistance, 2));
        }


        new void Update()
        {
            base.Update();

            if (isUsed && isPlayerOwner) { 
            
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
            
            } else if (!isPlayerOwner) {
                if (timer >= timeBetweenAttack && Time.timeScale != 0)
                {
                    Shoot();
                }
            }

            // If the timer has exceeded the proportion of timeBetweenBullets that the effects should be displayed for...
            if (timer >= timeBetweenAttack * effectsDisplayTime)
            {
                // ... disable the effects.
                DisableEffects();
            }
        }

        Vector3 getShootingDirection() { 
            Vector3 targetPosition = shootRay.origin + transform.forward;
            targetPosition = new Vector3(
                    targetPosition.x  + Random.Range(-inaccuracyDistance, inaccuracyDistance),
                    targetPosition.y + Random.Range(-inaccuracyDistance, inaccuracyDistance),
                    targetPosition.z + Random.Range(-inaccuracyDistance, inaccuracyDistance)
                );
            Vector3 direction = targetPosition - shootRay.origin;

            return direction.normalized;
        }
        IEnumerator DestroyLine(GameObject shootLineObject) { 
            float timeDisplay = timeBetweenAttack* effectsDisplayTime;

            while (timeDisplay > 0) { 
                timeDisplay-= Time.deltaTime;
                yield return null;
            }
            
            shootLineObject.GetComponent<LineRenderer>().enabled = false;
            Destroy(shootLineObject);
        }

        void CreateShootLine(Vector3 endLine) {
            GameObject shootLineObject = Instantiate(shotgunLine);
            LineRenderer shootLine = shootLineObject.GetComponent<LineRenderer>();
            shootLine.SetPositions(new Vector3[2] { transform.position, endLine });
            StartCoroutine(DestroyLine(shootLineObject));
        }

        float countBulletDamage(Vector3 shootHitPoint) {
            // Count basic damage
            float damage = damagePerAttack * (1f + powerUp);

            // Count shotgunLineLength
            float shotgunLineLength = (shootHitPoint - transform.position).magnitude;

            // Count final damage using distance function
            damage = ( 1 - ((bulletsPerShoot - 1) / (float) bulletsPerShoot) * (shotgunLineLength / maxShotgunLineLength)) * damage;
            return damage; 
        }

        public void DisableEffects()
        {
            // Disable the the light.
            faceLight.enabled = false;
            gunLight.enabled = false;
        }

        void Shoot()
        {
            // Debug.Log("Damage Peluru Shotgun: " + (damagePerAttack * (1f + powerUp)));
            // Reset the timer.
            timer = 0f;

            // Play the gun shot audioclip.
            gunAudio.Play();

            // Enable the lights.
            gunLight.enabled = true;
            faceLight.enabled = true;

            // Stop the particles from playing if they were, then start the particles.
            gunParticles.Stop();
            gunParticles.Play();

            for (int i = 0; i< bulletsPerShoot; i++)
            {
                // Set the shootRay so that it starts at the end of the gun and points forward from the barrel.
                shootRay.origin = transform.position;
                shootRay.direction = getShootingDirection();

                // Perform the raycast against gameobjects on the shootable layer and if it hits something...
                if (Physics.Raycast(shootRay, out shootHit, range, shootableMask))
                {
                    // Try and find an EnemyHealth script on the gameobject hit.
                    if (isPlayerOwner)
                    {
                        // if miss just add 1 to totalShootCount
                        if (shootHit.collider.TryGetComponent<EnemyHealth>(out var enemyHealth) == false && shootHit.collider.TryGetComponent<PetHealth>(out var petHealth) == false)
                        {
                            totalShootCount += 1;
                        }
                        // If the EnemyHealth component exist...
                        if (shootHit.collider.TryGetComponent<EnemyHealth>(out enemyHealth))
                        {
                            // ... the enemy should take damage.
                            float bulletDamage = countBulletDamage(shootHit.point);
                            Debug.Log("Peluru ke-" + i.ToString() + " damage: " + bulletDamage);

                            hitCount += 1;
                            totalShootCount += 1;
                            enemyHealth.TakeDamage(bulletDamage, shootHit.point);
                        }
                        if (shootHit.collider.TryGetComponent<PetHealth>(out petHealth))
                        {
                            // ... the enemy should take damage.
                            if (shootHit.collider.tag == "PetEnemy")
                            {
                                float bulletDamage = countBulletDamage(shootHit.point);
                                Debug.Log("Peluru ke-" + i.ToString() + " damage: " + bulletDamage);
                                petHealth.TakeDamage(bulletDamage);
                            }
                        }

                    }
                    else // If the user is enemy
                    { 
                        // If the PlayerHealth component exist...
                        if (shootHit.collider.TryGetComponent<PlayerHealth>(out var playerHealth))
                        {
                            // ... the player should take damage
                            float bulletDamage = countBulletDamage(shootHit.point);
                            playerHealth.TakeDamage(bulletDamage);
                        }
                        if (shootHit.collider.TryGetComponent<PetHealth>(out var petHealth))
                        {
                            // ... the enemy should take damage.
                            if (shootHit.collider.tag == "Pet")
                            {
                                float bulletDamage = countBulletDamage(shootHit.point);
                                Debug.Log("Peluru ke-" + i.ToString() + " damage: " + bulletDamage);
                                petHealth.TakeDamage(bulletDamage);
                            }
                        }
                    }

                    // Set the the line renderer to the point the raycast hit.
                    CreateShootLine(shootHit.point);
                }
                // If the raycast didn't hit anything on the shootable layer...
                else
                {
                    // ... set the  the line renderer to the fullest extent of the gun's range.
                    CreateShootLine(shootRay.origin + shootRay.direction * range);
                }
            }

        }
    }
}