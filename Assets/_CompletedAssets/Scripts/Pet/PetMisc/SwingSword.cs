using CompleteProject;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CompleteProject
{
    public class SwingSword : AttackerAnimationBaseClass
    {
        // Start is called before the first frame update
        private Animator animator;
        public Transform pet;

        void Start()
        {
            pet = transform;
            animator = pet.GetComponent<Animator>();
        }

        public override void attack(bool ranged = true)
        {
            animator.SetTrigger("attack");
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
