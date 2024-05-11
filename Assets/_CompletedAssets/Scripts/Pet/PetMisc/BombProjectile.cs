using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CompleteProject
{
    public class BombProjectile : MonoBehaviour
    {
        public Transform target;
        public float timeToTarget = 1.667f; // Time it takes for the projectile to reach the target
        public float rotationSpeed = 360f; // Speed at which the projectile rotates (in degrees per second)
        Vector3 offset = new Vector3(0, 0.5f, 0);
        private bool isMoving = true;
        private Animator animator;

        private void Start()
        {

            animator = GetComponent<Animator>();
            animator.SetTrigger("attack01");
        }
        void Update()
        {
            if (isMoving && target != null)
            {
                Vector3 targetPosition = target.position + offset;
                Vector3 direction = (targetPosition - transform.position).normalized;

                transform.position += direction * (Vector3.Distance(transform.position, targetPosition) / timeToTarget) * Time.deltaTime;

                transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);

                timeToTarget -= Time.deltaTime;

                if (timeToTarget <= 0)
                {
                    isMoving = false;
                    HitTarget();
                }
            }
        }

        void HitTarget()
        {
            Destroy(gameObject);
        }
    }
}