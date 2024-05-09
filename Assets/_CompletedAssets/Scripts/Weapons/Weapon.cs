using CompleteProject;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int damagePerAttack;
    public float timeBetweenAttack;
    public float powerUp = 0f;
    public bool isUsed = false;
    public bool isPlayerOwner = true;
    protected float timer;  // A timer to determine when to attack
    protected static int hitCount = 0; // The amount of hit count.
    protected GameObject weapon; // Weapon 3D physics
    protected GameObject weapon2; // for weapon rendered by more than 1 gameObject

    public void PowerUp()
    {
        powerUp += 0.1f;
    }

    public void ApplyBossEffect()
    {
        powerUp -= 0.3f;
    }

    public void UnApplyBossEffect()
    {
        powerUp += 0.3f;
    }

    public void UseWeapon()
    {
        isUsed = true;
        weapon.GetComponent<Renderer>().enabled = true;
        if ( weapon2 != null )
        {
            weapon2.GetComponent<Renderer>().enabled = true;
        }
    }

    public void UnUseWeapon()
    {
        isUsed = false;
        weapon.GetComponent<Renderer>().enabled = false;
        if ( weapon2 != null )
        {
            weapon2.GetComponent<Renderer>().enabled = false;
        }
    }

    // Update is called once per frame
    protected void Update()
    {
        // Add the time since Update was last called to the timer.
        timer += Time.deltaTime;
    }
}
