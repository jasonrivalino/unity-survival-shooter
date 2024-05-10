using System.Collections;
using System.Collections.Generic;
using Unity.Services.Analytics.Internal;
using UnityEngine;
using UnityEngine.AI;

namespace CompleteProject
{
    public class AttackerMovement : MonoBehaviour
    {
        // Start is called before the first frame update
        Transform target;               // Reference to the target's position.
        public string enemyTag = "Enemy";
        Transform pet;
        EnemyHealth enemyHealth;      // Reference to the target's health.
        PetHealth petHealth;        // Reference to this pet's health.
        UnityEngine.AI.NavMeshAgent nav;               // Reference to the nav mesh agent.
        public float speed = 5.0f;
        private AttackerAnimationBaseClass anim;
        private Animator animator;
        void Awake()
        {
            anim = GetComponent<AttackerAnimationBaseClass>();
            animator = GetComponent<Animator>();
            pet = transform;
            petHealth = GetComponent<PetHealth>();
            nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
            nav.speed = speed;

        }

        void Update()
        {
            FindClosestEnemy();

            if (target != null)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);
                if (enemyHealth.currentHealth > 0 && petHealth.currentHealth > 0)
                {
                    if (distanceToTarget > pet.GetComponent<Attack>().attackRange)
                    {
                        anim.walk();
                        nav.SetDestination(target.position);
                    }
                    else
                    {
                        animator.SetTrigger("idle");
                    }
                }
            }
        }

        void FindClosestEnemy()
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag); // Find all GameObjects with the enemyTag

            float closestDistanceSqr = Mathf.Infinity;

            foreach (GameObject enemy in enemies)
            {
                float distanceSqr = (enemy.transform.position - transform.position).sqrMagnitude; // Squared distance for faster calculation

                if (distanceSqr < closestDistanceSqr)
                {
                    closestDistanceSqr = distanceSqr;
                    target = enemy.transform;
                    enemyHealth = target.GetComponent<EnemyHealth>();
                }
            }
        }
    }
}
