using UnityEngine;

namespace CompleteProject
{
    public class EnemyHellephantManager : MonoBehaviour
    {
        public PlayerHealth playerHealth;       // Reference to the player's health.
        public Transform hellephantTransform;  // Reference to the hellephant's transform.
        public GameObject zomBear;             // The enemy prefab to be spawned.
        public float spawnRadius = 5f;
        public float spawnCollisionCheckRadius = 0.5f;
        public float spawnTime = 3f;           // How long between each spawn.
        public Transform[] spawnPoints;         // An array of the spawn points this enemy can spawn from.

        void Start()
        {
            // Call the Spawn function after a delay of the spawnTime and then continue to call after the same amount of time.
            InvokeRepeating("Spawn", spawnTime, spawnTime);
        }


        void Spawn()
        {
            // If the player has no health left...
            if (playerHealth.currentHealth <= 0f)
            {
                // ... exit the function.
                return;
            }

            // Generate Random Point for Spawn
            Vector3 spawnPoint = Vector3.zero;
            spawnPoint.y = spawnCollisionCheckRadius + 0.01f;

            // Get random point
            spawnPoint.x = hellephantTransform.position.x + (2 * Random.value - 1) * spawnRadius;
            spawnPoint.z = hellephantTransform.position.z + (2 * Random.value - 1) * spawnRadius;

            // Adjust to not exit the orb spawn radius
            if (spawnPoint.x < 0)
            {
                spawnPoint.x = Mathf.Max(spawnPoint.x, -spawnRadius);
            }
            else if (spawnPoint.x > 0)
            {
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
                // Find a random index between zero and one less than the number of spawn points.
                int spawnPointIndex = Random.Range(0, spawnPoints.Length);

                // Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
                Instantiate(zomBear, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
            }
        }
    }
}
