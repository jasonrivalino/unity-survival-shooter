using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 6f;
    Vector3 movement;
    Animator anim;
    Rigidbody playerRigidbody;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // Get input for movement
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        // Call movement and animation functions
        Move(h, v);
        Animating(h, v);
    }

    void Move(float h, float v)
    {
        // Calculate movement vector and normalize it
        movement.Set(h, 0f, v);
        movement = movement.normalized * speed * Time.deltaTime;

        // Move the player
        playerRigidbody.MovePosition(transform.position + movement);
    }

    void Animating(float h, float v)
    {
        // Set the IsWalking parameter in the Animator based on movement input
        bool walking = h != 0f || v != 0f;
        anim.SetBool("IsWalking", walking);
    }
}
