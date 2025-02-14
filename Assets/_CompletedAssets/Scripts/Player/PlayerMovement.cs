﻿using UnityEngine;
using UnityEngine.UI;
using UnitySampleAssets.CrossPlatformInput;

namespace CompleteProject
{
    public class PlayerMovement : MonoBehaviour
    {
        public float speed = 6f;            // The speed that the player will move at.
        public float speedUp = 1f;
        public float speedUpTime = 0f;
        public Text speedUpTimeText;
        public Text speedUpText;
        float distanceTravelled = 0f; // Variable to store the distance travelled by the player


        Vector3 movement;                   // The vector to store the direction of the player's movement.
        Animator anim;                      // Reference to the animator component.
        Rigidbody playerRigidbody;          // Reference to the player's rigidbody.
#if !MOBILE_INPUT
        int floorMask;                      // A layer mask so that a ray can be cast just at gameobjects on the floor layer.
        float camRayLength = 100f;          // The length of the ray from the camera into the scene.
#endif

        void Awake()
        {
#if !MOBILE_INPUT
            // Create a layer mask for the floor layer.
            floorMask = LayerMask.GetMask("Floor");
#endif

            // Set up references.
            anim = GetComponent<Animator>();
            playerRigidbody = GetComponent<Rigidbody>();
        }


        void FixedUpdate()
        {
            // Store the input axes.
            float h = CrossPlatformInputManager.GetAxisRaw("Horizontal");
            float v = CrossPlatformInputManager.GetAxisRaw("Vertical");

            // Move the player around the scene.
            Move(h, v);

           
            // Turn the player to face the mouse cursor.
            Turning();

            // Animate the player.
            Animating(h, v);
        }


        void Move(float h, float v)
        {
            // Debug.Log("speed total: " + (speed * speedUp));
            // Set the movement vector based on the axis input.
            movement.Set(h, 0f, v);

            // Normalise the movement vector and make it proportional to the speed per second.
            movement = movement.normalized * speed * speedUp * Time.deltaTime;

            // If 2xSpeedUp cheat activated
            if (PlayerPrefs.HasKey("2xSpeedUp"))
            {
                // Speedup two times
                movement *= 2;
            }

            // Move the player to it's current position plus the movement.
            playerRigidbody.MovePosition(transform.position + movement);

            // Calculate distance travelled
            distanceTravelled += movement.magnitude;
        }

        public float GetDistanceTravelled()
        {
            return distanceTravelled;
        }


        void Turning()
        {
#if !MOBILE_INPUT
            // Create a ray from the mouse cursor on screen in the direction of the camera.
            Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

            // Create a RaycastHit variable to store information about what was hit by the ray.
            RaycastHit floorHit;

            // Perform the raycast and if it hits something on the floor layer...
            if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
            {
                // Create a vector from the player to the point on the floor the raycast from the mouse hit.
                Vector3 playerToMouse = floorHit.point - transform.position;

                // Ensure the vector is entirely along the floor plane.
                playerToMouse.y = 0f;

                // Create a quaternion (rotation) based on looking down the vector from the player to the mouse.
                Quaternion newRotatation = Quaternion.LookRotation(playerToMouse);

                // Set the player's rotation to this new rotation.
                playerRigidbody.MoveRotation(newRotatation);
            }
#else

            Vector3 turnDir = new Vector3(CrossPlatformInputManager.GetAxisRaw("Mouse X") , 0f , CrossPlatformInputManager.GetAxisRaw("Mouse Y"));

            if (turnDir != Vector3.zero)
            {
                // Create a vector from the player to the point on the floor the raycast from the mouse hit.
                Vector3 playerToMouse = (transform.position + turnDir) - transform.position;

                // Ensure the vector is entirely along the floor plane.
                playerToMouse.y = 0f;

                // Create a quaternion (rotation) based on looking down the vector from the player to the mouse.
                Quaternion newRotatation = Quaternion.LookRotation(playerToMouse);

                // Set the player's rotation to this new rotation.
                playerRigidbody.MoveRotation(newRotatation);
            }
#endif
        }


        void Animating(float h, float v)
        {
            // Create a boolean that is true if either of the input axes is non-zero.
            bool walking = h != 0f || v != 0f;

            // Tell the animator whether or not the player is walking.
            anim.SetBool("IsWalking", walking);
        }

        public void SpeedUp()
        {
            speedUp += 0.2f;
            speedUpTime = 15f;
            speedUpText.text = "x " + speedUp;
        }

        public void ApplyBossEffect()
        {
            speedUp -= 0.3f;
            speedUpText.text = "x " + speedUp;
        }

        public void UnApplyBossEffect()
        {
            speedUp += 0.3f;
            speedUpText.text = "x " + speedUp;
        }

        public void Stop()
        {
            speed = 0f;
        }

        public void StartMove()
        {
            speed = 6f;
        }

        private void Update()
        {
            // Reducing speedUpTime if any
            if (speedUpTime > 0)
            {
                speedUpTime -= Time.deltaTime;

                // Update UI
                speedUpTimeText.text = "Speed Up: " + ((int)speedUpTime + 1) + " s";

                // If speedUpTime over, set speedUp back to 1
                if (speedUpTime <= 0)
                {
                    speedUpTime = 0;
                    speedUp -= 0.2f;

                    // Update UI
                    speedUpTimeText.text = "";
                    speedUpText.text = "x " + speedUp;
                }
            }

        }
    }
}