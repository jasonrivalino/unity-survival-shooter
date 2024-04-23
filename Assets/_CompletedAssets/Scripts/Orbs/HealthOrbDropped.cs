using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CompleteProject
{
    public class HealthOrbDropped : MonoBehaviour
    {
        UnityEngine.GameObject player; // Reference to the player GameObject.
        PlayerHealth playerHealth;
        float timer;
        void Awake()
        {
            // Setting up the references
            player = UnityEngine.GameObject.FindGameObjectWithTag("Player");
            playerHealth = player.GetComponentInChildren<PlayerHealth>();
            timer = 0f;
        }

        void OnCollisionEnter(UnityEngine.Collision collision)
        {
            // If the entering collider is the player 
            if (collision.gameObject == player)
            {
                Debug.Log("Nyentuh Health orb");

                // Heal the player
                playerHealth.Heal();
                GetComponent<Renderer>().enabled = false;
                Destroy(gameObject, 1f);
            }
        }


        // Update is called once per frame
        void Update()
        {
            timer += Time.deltaTime;

            // If orb was dropped more than 5 seconds, orb will disappear
            if (timer > 5f)
            {
                GetComponent<Renderer>().enabled = false;
                Destroy(gameObject, 1f);
            }
        }
    }
}
