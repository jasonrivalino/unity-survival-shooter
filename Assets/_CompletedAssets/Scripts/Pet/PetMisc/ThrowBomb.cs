using CompleteProject;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CompleteProject
{
    public class ThrowBomb : AttackerAnimationBaseClass
    {
        // Start is called before the first frame update
        public Animator animator;
        public Transform pet;
        public GameObject projectilePrefabs;

        void Start()
        {
            animator = this.GetComponent<Animator>();
            pet = transform;
        }

        // Update is called once per frame
        public override void attack(bool ranged = true)
        {
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            string currentStateName = animator.GetCurrentAnimatorClipInfo(0)[0].clip.name;
            if (currentStateName != "mon00_damage")
            animator.SetTrigger("damage");
            BombProjectile bombProjectile = projectilePrefabs.GetComponent<BombProjectile>();
            bombProjectile.target = this.target;
            Instantiate(projectilePrefabs, pet.position + new Vector3(0, 0.5f, 0), pet.rotation);
        }

        public override void walk()
        {
            animator.SetTrigger("walk");
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            string currentStateName = animator.GetCurrentAnimatorClipInfo(0)[0].clip.name;

            Debug.Log("Current state name: " + currentStateName);
        }

        public override void die()
        {
            animator.SetTrigger("attack01");
        }
    }
}
