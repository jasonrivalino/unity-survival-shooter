using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
namespace CompleteProject
{
    public class Buff : MonoBehaviour
    {
        public string enemyTag = "Enemy";
        public string bossName = "Boss(Clone)";
        public string devilName = "Devils(Clone)";
        
        Transform target;               // Reference to the target's position.
        EnemyHealth enemyHealth;      // Reference to the target's health.
        PetHealth petHealth;        // Reference to this pet's health.
        UnityEngine.AI.NavMeshAgent nav;               // Reference to the nav mesh agent.
        public float speed = 5.0f;
        private Animator animator;
        private List<GameObject> buffedEnemy = new List<GameObject>();
        void Awake()
        {
            animator = this.GetComponent<Animator>();
            petHealth = GetComponent<PetHealth>();
            nav = GetComponent<UnityEngine.AI.NavMeshAgent>();

            StartCoroutine(BuffEnemy());
        }

        IEnumerator BuffEnemy()
        {
            while (petHealth.currentHealth > 0)
            {
                yield return new WaitForSeconds(1f);
                buffedEnemy.RemoveAll(obj => obj == null);

                GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
                foreach (GameObject obj in enemies)
                {
                    // Perform processing for each GameObject
                    if (!buffedEnemy.Contains(obj))
                    {
                        BossEffect bossEffect = obj.GetComponent<BossEffect>();
                        if (bossEffect != null)
                        {
                            buffedEnemy.Add(obj);
                            bossEffect.buffDamage();
                        }
                        DevilEffect devilEffect = obj.GetComponent<DevilEffect>();
                        if (devilEffect != null)
                        {
                            buffedEnemy.Add(obj);
                            devilEffect.buffDamage();
                        }
                    }
                }
            }
        }

        private void Update()
        {
            if(petHealth.currentHealth <= 0)
            {
                foreach (GameObject obj in buffedEnemy)
                {
                    BossEffect bossEffect = obj.GetComponent<BossEffect>();
                    DevilEffect devilEffect = obj.GetComponent<DevilEffect>();
                    
                    if (bossEffect != null)
                    {
                        bossEffect.debuffDamage();
                    }
                    if (devilEffect != null)
                    {
                        devilEffect.debuffDamage();
                    }
                }
            }
        }
    }
}
