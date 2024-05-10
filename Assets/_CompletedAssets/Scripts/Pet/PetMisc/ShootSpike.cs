using CompleteProject;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CompleteProject
{
    public class ShootSpike : AttackerAnimationBaseClass
    {
        // Start is called before the first frame update
        private Animator animator;
        public Transform pet;
        public GameObject projectilePrefabs;
        public bool isRanged = true;
        float timeElapsed = 0f;

        void Start()
        {
            Debug.Log("Start method called.");
            pet = transform;
            animator = pet.GetComponent<Animator>();
            if (animator == null)
            {
                Debug.LogError("Animator component not found on GameObject: " + gameObject.name);
            }
        }

        public override void attack(bool ranged = true)
        {
            if (animator == null)
            {
                pet = transform;
                animator = pet.GetComponent<Animator>();
            }
            if (ranged)
            {
                animator.SetTrigger("shoot");
                CactusProjectile cactusProjectile = projectilePrefabs.GetComponent<CactusProjectile>();
                cactusProjectile.target = this.target;
                Instantiate(projectilePrefabs, pet.position + new Vector3(0, 0.5f, 0), pet.rotation);
                isRanged = false;
                timeElapsed = 5f;
            }
            else
            {
                animator.SetTrigger("punch");
                timeElapsed -= 1.1f;
                Debug.Log(timeElapsed);
                if (timeElapsed < 0f)
                {
                    isRanged = true;
                }
            }
        }

        public override void walk()
        {
            animator.SetTrigger("walk");
        }

        public override void die()
        {
            animator.SetTrigger("die");
        }
    }
}
