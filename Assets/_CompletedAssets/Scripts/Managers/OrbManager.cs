using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CompleteProject
{
    public class OrbManager : MonoBehaviour
    {
        public PlayerHealth playerHealth;       // Reference to the player's heatlh.
        public Transform playerTransform;
        public UnityEngine.GameObject[] orbs;   // The orb prefabs to be spawn. orbs[0]: Damage Orb, orbs[1]: Health Orb, orbs[2]: SpeedUp Orb
        public float spawnRadius = 5f;
        public float spawnCollisionCheckRadius = 0.5f;
        public float spawnTransformationX;

        public void CheatSpawn(int orbTypeIdx)
        {
            // If the player has no health left...
            if (playerHealth.currentHealth <= 0f)
            {
                // ... exit the function.
                return;
            }

            Vector3 spawnPoint = playerTransform.position;
            spawnPoint.x += spawnTransformationX;

            // Create an instance of the orb prefab at the randomly selected spawn point's position and rotation.
            Instantiate(orbs[orbTypeIdx], spawnPoint, playerTransform.rotation);
        }

        public void Spawn()
        {
            // If the player has no health left...
            if (playerHealth.currentHealth <= 0f)
            {
                // ... exit the function.
                return;
            }

            // Generate Random Point for Spawn
            Vector3 spawnPoint = Vector3.zero;
            spawnPoint.y = spawnCollisionCheckRadius+0.01f;
           
            // Get random point
            spawnPoint.x = playerTransform.position.x + (2 * Random.value - 1) * spawnRadius;
            spawnPoint.z = playerTransform.position.z + (2 * Random.value - 1) * spawnRadius;

            // Adjust to not exit the orb spawn radiu
            if (spawnPoint.x < 0)
            {
                spawnPoint.x = Mathf.Max(spawnPoint.x, -spawnRadius);
            }
            else if (spawnPoint.x > 0) { 
                spawnPoint.x = Mathf.Min(spawnPoint.x, spawnRadius);
            }

            if (spawnPoint.z < 0)
            {
                spawnPoint.z = Mathf.Max(spawnPoint.z, -spawnRadius);
            }
            else if (spawnPoint.z > 0)
            {
                spawnPoint.z = Mathf.Min(spawnPoint.z, spawnRadius);
            }
         

            if (!Physics.CheckSphere(spawnPoint, spawnCollisionCheckRadius))
            {
                // Randomly select orb type to spawn
                int orbTypeIdx = Random.Range(0, orbs.Length);

                // Create an instance of the orb prefab at the randomly selected spawn point's position and rotation.
                Instantiate(orbs[orbTypeIdx], spawnPoint, playerTransform.rotation);
            }
            

        }
    }
}
