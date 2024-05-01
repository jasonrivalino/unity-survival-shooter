using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    public float speed = 5.0f;
    private Animator animator;
    void Start()
    {
        animator = this.GetComponent<Animator>();
    }


    void Update()
    {
        // Calculate the direction towards the player
        Vector3 directionToGhost = transform.position - player.transform.position;
        directionToGhost.Normalize();

        // Calculate the offset based on the direction to the ghost
        float offsetX, offsetY;
        do
        {
            offsetX = Mathf.Round(directionToGhost.x);
            offsetY = Mathf.Round(directionToGhost.y) * 2; // Multiply y component by 2
        } while (offsetX == 0 && offsetY == 0); // Repeat until both offsets are non-zero

        Vector3 offset = new Vector3(offsetX, offsetY, 0);

        // Calculate the direction towards the player plus the offset
        Vector3 direction = player.transform.position - transform.position + offset;
        direction.Normalize();

        // Move the object towards the player
        Vector3 oldPosition = transform.position;
        if (direction != Vector3.zero) // Avoid LookRotation error when direction is zero
        {
            Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, speed * Time.deltaTime);
        }
        transform.position += direction * speed * Time.deltaTime;
        Vector3 newPosition = transform.position;

        // Check if the object has moved
        if (oldPosition != newPosition)
        {
            // If the object has moved, set isMoving to true
            animator.SetBool("isMoving", true);
        }
        else
        {
            // If the object hasn't moved, set isMoving to false
            animator.SetBool("isMoving", false);
        }
    }

}
