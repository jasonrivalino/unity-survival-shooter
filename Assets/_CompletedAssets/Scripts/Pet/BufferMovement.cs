using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace CompleteProject
{
    public class BuffMovement : MonoBehaviour
    {
        Transform player;               // Reference to the player's position.
        Transform pet;
        PlayerHealth playerHealth;      // Reference to the player's health.
        PetHealth petHealth;        // Reference to this pet's health.
        UnityEngine.AI.NavMeshAgent nav;               // Reference to the nav mesh agent.
        public float speed = 5.0f;
        private Animator animator;
        void Awake()
        {
            animator = this.GetComponent<Animator>();
            player = GameObject.FindGameObjectWithTag("Player").transform;
            pet = transform; // Assign the pet's transform
            playerHealth = player.gameObject.GetComponent<PlayerHealth>();
            petHealth = GetComponent<PetHealth>();
            nav = GetComponent<UnityEngine.AI.NavMeshAgent>();

        }

        void Update()
        {
            if (playerHealth.currentHealth > 0 && petHealth.currentHealth > 0)
            {
                float distanceToPlayer = Vector3.Distance(player.position, pet.position);

                animator.SetTrigger("walk");
                if (distanceToPlayer < 10)
                {
                    Vector3 awayFromPlayerDirection = (pet.position - player.position).normalized;
                    Vector3 targetPosition = pet.position + awayFromPlayerDirection * 4;

                    nav.SetDestination(targetPosition);
                }
                else
                {
                    Vector3 randomDirection = Random.insideUnitSphere * 4;
                    randomDirection += transform.position;
                    NavMeshHit hit;
                    NavMesh.SamplePosition(randomDirection, out hit, 4, 1);
                    Vector3 targetPosition = hit.position;
                    nav.SetDestination(targetPosition);
                }
            }
            else
            {
                nav.enabled = false;
            }
        }

    }
}
