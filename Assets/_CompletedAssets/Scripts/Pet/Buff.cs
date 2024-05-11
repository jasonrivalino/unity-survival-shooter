using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
namespace CompleteProject
{
    public class Buff : MonoBehaviour
    {
        public Transform owner;
        PetHealth petHealth;        // Reference to this pet's health.
        EnemyHealth ownerHealth;
        private Animator animator;
        bool isBuffing = false;
        void Awake()
        {
            animator = this.GetComponent<Animator>();
            petHealth = GetComponent<PetHealth>();
        }

        void BuffOwner()
        {
            BossEffect bossEffect = ownerHealth.gameObject.GetComponent<BossEffect>();
            DevilEffect devilEffect = ownerHealth.gameObject.GetComponent<DevilEffect>();
            if (bossEffect != null)
            {
                Debug.Log("BOSS DETECTED. ATTACK DAMAGE: " + bossEffect.attackDamage);
                bossEffect.buffDamage();
                Debug.Log("BOSS BUFFED. ATTACK DAMAGE: " + bossEffect.attackDamage);
            }
            if (devilEffect != null)
            {
                Debug.Log("DEVIL DETECTED. ATTACK DAMAGE: " + devilEffect.attackDamage);
                devilEffect.buffDamage();
                Debug.Log("DEVIL BUFFED. ATTACK DAMAGE: " + devilEffect.attackDamage);
            }
        }

        private void Update()
        {
            if (!isBuffing)
            {
                if (owner != null)
                {
                    ownerHealth = owner.gameObject.GetComponent<EnemyHealth>();
                    BuffOwner();
                    isBuffing = true;
                }
            }
            if(isBuffing)
            {
                if (owner != null)
                {
                    if ( petHealth.currentHealth <= 0 )
                    {
                        BossEffect bossEffect = ownerHealth.gameObject.GetComponent<BossEffect>();
                        DevilEffect devilEffect = ownerHealth.gameObject.GetComponent<DevilEffect>();
                        
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
}
