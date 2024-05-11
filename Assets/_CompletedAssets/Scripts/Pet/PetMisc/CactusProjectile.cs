using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CompleteProject
{
    public class CactusProjectile : MonoBehaviour
    {
        public Transform target;
        public float timeToTarget = 0.3f; // Time it takes for the projectile to reach the target
        public float rotationSpeed = 1080f; // Speed at which the projectile rotates (in degrees per second)
        Vector3 offset = new Vector3(0, 0.5f, 0);
        private bool isMoving = true;



        void Update()
        {
            if (isMoving && target != null)
            {
                Vector3 targetPosition = target.position + offset;
                Vector3 direction = (targetPosition - transform.position).normalized;

                transform.position += direction * (Vector3.Distance(transform.position, targetPosition) / timeToTarget) * Time.deltaTime;
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
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