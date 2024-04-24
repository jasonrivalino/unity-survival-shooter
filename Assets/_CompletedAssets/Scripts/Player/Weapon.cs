using CompleteProject;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float powerUp = 0f;
    public bool isUsed = false;
    protected float timer;  // A timer to determine when to attack
    protected GameObject weapon; // Weapon 3D physics

    public void PowerUp()
    {
        powerUp += 0.1f;
    }

    public void UseWeapon()
    {
        isUsed = true;
        weapon.GetComponent<Renderer>().enabled = true;
    }

    public void UnUsedWeapon()
    {
        isUsed = false;
        weapon.GetComponent<Renderer>().enabled = false;
    }

    // Update is called once per frame
    protected void Update()
    {
        // Add the time since Update was last called to the timer.
        timer += Time.deltaTime;
    }
}
