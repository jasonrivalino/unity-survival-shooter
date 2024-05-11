using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CompleteProject
{
    public class IgnorePetCollision : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void OnCollisionEnter(Collision collision)
        {
            // Check if the colliding GameObject has the player tag
            if (collision.gameObject.CompareTag("Pet") || collision.gameObject.CompareTag("PetEnemy"))
            {
                // Ignore collision with player GameObject
                Physics.IgnoreCollision(collision.collider, GetComponent<Collider>());
            }
        }
    }
}