using CompleteProject;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CompleteProject
{
    public class HealerMovement : MonoBehaviour
    {
        // Start is called before the first frame update
        // Start is called before the first frame update
        Transform player;               // Reference to the player's position.
        Transform pet;
        PlayerHealth playerHealth;      // Reference to the player's health.
        PetHealth petHealth;        // Reference to this enemy's health.
        UnityEngine.AI.NavMeshAgent nav;               // Reference to the nav mesh agent.
        private Animator animator;
        bool canMove = false;
        public float moveSpeed;
        void Awake()
        {
            animator = this.GetComponent<Animator>();
            player = GameObject.FindGameObjectWithTag("Player").transform;
            pet = transform; // Assign the enemy's transform
            playerHealth = player.gameObject.GetComponent<PlayerHealth>();
            petHealth = GetComponent<PetHealth>();
            nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
            nav.speed = moveSpeed;

        }

        public void StartMove()
        {
            canMove = true;
        }

        void Update()
        {
            if (playerHealth.currentHealth > 0 && petHealth.currentHealth > 0)
            {
                if (canMove && Vector3.Distance(player.position, pet.position) > 2) 
                {
                    Vector3 targetPosition = (player.position + pet.position) / 2f;
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
