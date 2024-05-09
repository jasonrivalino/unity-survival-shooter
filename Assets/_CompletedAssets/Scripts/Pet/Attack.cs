using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CompleteProject
{
    public class Attack : MonoBehaviour
    {
        public string enemyTag = "Enemy";
        Transform target;               // Reference to the target's position.
        Transform pet;
        EnemyHealth enemyHealth;      // Reference to the target's health.
        PetHealth petHealth;        // Reference to this pet's health.
        UnityEngine.AI.NavMeshAgent nav;               // Reference to the nav mesh agent.
        public float damage = 10;
        public float attackRange = 2.5f;
        public float attackRate = 1.2f;
        private float rangedDamage;
        private float meleeDamage;
        private float rangedRange;
        private float meleeRange;
        private AttackerAnimationBaseClass anim;
        bool isRanged;

        void Awake()
        {
            pet = transform;
            petHealth = GetComponent<PetHealth>();
            nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
            anim = GetComponent<AttackerAnimationBaseClass>();
            ShootSpike shootSpike = GetComponent<ShootSpike>();
            if (shootSpike != null)
            {
                meleeDamage = damage;
                meleeRange = attackRange;
                rangedDamage = 5 * damage;
                rangedRange = 5 * attackRange;
            }
            StartCoroutine(AttackEnemy());
        }

        IEnumerator AttackEnemy()
        {
            while (true)
            {
                FindClosestEnemy();
                if (target != null)
                {
                    ShootSpike shootSpike = GetComponent<ShootSpike>();
                    if (shootSpike != null)
                    {
                        if (shootSpike.isRanged)
                        {
                            damage = rangedDamage;
                            attackRange = rangedRange;
                        }
                        else
                        {
                            damage = meleeDamage;
                            attackRange = meleeRange;
                        }
                    }
                    float distanceToTarget = Vector3.Distance(transform.position, target.position);
                    Debug.Log("Target Found, distance: " + distanceToTarget);
                    if (enemyHealth.currentHealth > 0 && petHealth.currentHealth > 0 && distanceToTarget < attackRange)
                    {
                        anim.setTarget(target);
                        if (shootSpike == null)
                        {
                            anim.attack();
                        }
                        else
                        {
                            anim.attack(shootSpike.isRanged);
                        }
                        enemyHealth.TakeDamage(damage, transform.position);

                        Debug.Log("pet ngedamage musuh, sisa hp musuh: " + enemyHealth.currentHealth);
                        
                        yield return new WaitForSeconds(attackRate);
                    }
                    else
                    {
                        yield return new WaitForSeconds(0.1f);
                    }
                }
                else
                {
                    yield return new WaitForSeconds(0.1f);
                }
                Debug.Log("pet Location: " + pet.position);
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
                    Debug.Log("EnemyFound");

                }
            }
        }

    }
}