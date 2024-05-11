using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackerAnimationBaseClass : MonoBehaviour
{
    protected Transform target;

    public void setTarget(Transform target)
    { this.target = target; }
    public virtual void walk()
    {

    }

    public virtual void die()
    {
        
    }

    public virtual void attack(bool ranged = true)
    {

    }
}
