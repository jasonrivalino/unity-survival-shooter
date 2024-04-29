using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CompleteProject
{
    public abstract class Orb : MonoBehaviour
    {
        protected UnityEngine.GameObject player; // Reference to the player GameObject.
        float timer; // Timer for orb lifetime
        protected bool isPicked = false;

        protected AudioSource orbPickedAudio;

        // Start is called before the first frame update
        protected void Awake()
        {
            // Setting up the references
            player = UnityEngine.GameObject.FindGameObjectWithTag("Player");
            timer = 0f;
            orbPickedAudio = GetComponent<AudioSource>();
        }

        // Update is called once per frame
        protected void Update()
        {
            timer += Time.deltaTime;

            // If orb was dropped more than 5 seconds, orb will disappear
            if (timer > 5f)
            {
                Dissapear();
            }
        }

        protected void Dissapear()
        {
            Renderer[] renderers = GetComponentsInChildren<Renderer>();
            Debug.Log(renderers.Length);
            foreach(Renderer renderer in renderers) { 
                renderer.enabled = false;
            }
            Destroy(gameObject, 1f);
        }
    }
}
