using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CompleteProject
{
    public class tempAttack : MonoBehaviour
    {
        public string enemyTag = "Enemy";
        public Transform target;               // Reference to the target's position.
        Transform pet;
        EnemyHealth enemyHealth;      // Reference to the target's health.
        PetHealth petHealth;        // Reference to this pet's health.
        UnityEngine.AI.NavMeshAgent nav;               // Reference to the nav mesh agent.
        public int damage = 10;
        public float speed = 5.0f;
        public float attackRange = 2.5f;
        AttackerAnimationBaseClass anim;


        void Awake()
        {
            pet = transform;
            petHealth = GetComponent<PetHealth>();
            nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
            anim = GetComponent<AttackerAnimationBaseClass>();
            StartCoroutine(AttackEnemy());
        }

        IEnumerator AttackEnemy()
        {
            while (true)
            {
                Debug.Log("call attak");
                anim.setTarget(target);
                ShootSpike shootSpike = GetComponent<ShootSpike>();
                if (shootSpike.isRanged)
                {
                    attackRange = 6f;
                    damage = 15;
                }
                else
                {
                    attackRange = 2.5f;
                    damage = 5;
                }
                anim.attack(shootSpike.isRanged);
                yield return new WaitForSeconds(1f);

            }
        }

    }
}