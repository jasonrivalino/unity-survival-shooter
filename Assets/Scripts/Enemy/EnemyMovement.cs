using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
    Transform player;
    Transform enemy; // Added variable to hold the enemy's transform
    PlayerHealth playerHealth;
    EnemyHealth enemyHealth;
    UnityEngine.AI.NavMeshAgent nav;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        enemy = transform; // Assign the enemy's transform
        playerHealth = player.GetComponent<PlayerHealth>();
        enemyHealth = GetComponent<EnemyHealth>();
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();

        // Start coroutine to print location every 25 seconds
        StartCoroutine(PrintEnemyLocation());
    }

    IEnumerator PrintEnemyLocation()
    {
        while (true)
        {
            yield return new WaitForSeconds(25f);
            Debug.Log("Enemy Location: " + enemy.position);
        }
    }

    void Update()
    {
        if (enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0)
        {
            // Calculate a position halfway between the player and the enemy
            Vector3 targetPosition = (player.position + enemy.position) / 2f;
            nav.SetDestination(targetPosition);
        }
        else
        {
            nav.enabled = false;
        }
    }
}
