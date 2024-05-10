using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CompleteProject
{
    public class PetHealth : MonoBehaviour
    {
        public float startingHealth = 50;
        public float currentHealth;
        public float sinkSpeed = 2.5f;
        public AudioClip deathClip;
        public AudioSource audioSource;
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

        public void TakeDamage(float amount)
        {
            bool isFriendly = transform.gameObject.tag.Equals("Pet");
            if (!PlayerPrefs.HasKey("NoDamagePet") || !isFriendly)
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
        }

        void Death()
        {
            isDead = true;
            anim.SetTrigger("die");
            StartSinking();
        }

        public void StartSinking()
        {
            GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
            GetComponent<Rigidbody>().isKinematic = true;
            isSinking = true;
            Destroy(gameObject, 2f);
        }

        public void kill()
        {
            TakeDamage(999);
        }

    }
}
