using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CompleteProject
{
    public class SpeedOrbDropped : MonoBehaviour
    {
        UnityEngine.GameObject player; // Reference to the player GameObject.
        PlayerMovement playerMovement;
        float timer;
        void Awake()
        {
            // Setting up the references
            player = UnityEngine.GameObject.FindGameObjectWithTag("Player");
            playerMovement = player.GetComponentInChildren<PlayerMovement>();
            timer = 0f;
        }

        void OnCollisionEnter(UnityEngine.Collision collision)
        {
            // If the entering collider is the player 
            if (collision.gameObject == player)
            {
                Debug.Log("Nyentuh speed orb");
                // Set speedup for player
                playerMovement.SpeedUp();
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