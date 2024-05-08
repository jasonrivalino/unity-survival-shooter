using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CompleteProject
{
    public class PetHealth : MonoBehaviour
    {
        public int startingHealth = 50;
        public int currentHealth;
        public float sinkSpeed = 2.5f;
        public AudioClip deathClip;
        Animator anim;
        bool isDead;
        bool isSinking;
        // Start is called before the first frame update
        void Awake()
        {
            anim = GetComponent<Animator>();
            currentHealth = startingHealth;
        }

        // Update is called once per frame
        void Update()
        {
            if (isSinking)
            {
                transform.Translate(-Vector3.up * sinkSpeed * Time.deltaTime);
            }
        }

        public void TakeDamage(int amount, Vector3 hitPoint)
        {
            if (isDead)
                return;

            currentHealth -= amount;
            Debug.Log("Enemy Health: " + currentHealth);

            if (currentHealth <= 0)
            {
                Death();
            }
        }

        void Death()
        {
            isDead = true;
            StartSinking();
        }

        public void StartSinking()
        {
            GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
            GetComponent<Rigidbody>().isKinematic = true;
            isSinking = true;
            Destroy(gameObject, 2f);
        }
    }
}
