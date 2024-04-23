using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CompleteProject
{
    public class DamageOrbDropped : MonoBehaviour
    {
        UnityEngine.GameObject player; // Reference to the player GameObject.
        PlayerShooting playerShooting;
        float timer;
        void Awake()
        {
            // Setting up the references
            player = UnityEngine.GameObject.FindGameObjectWithTag("Player");
            playerShooting = player.GetComponentInChildren<PlayerShooting>();
            timer = 0f;
        }

        void OnCollisionEnter(UnityEngine.Collision collision)
        {
            // If the entering collider is the player 
            if (collision.gameObject == player)
            {
                // If player is not pick damageOrb 15 times or more
                if (playerShooting.powerUp < 1.5f)
                {
                    playerShooting.PowerUp();
                    GetComponent<Renderer>().enabled = false;
                    Destroy(gameObject, 1f);
                }
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