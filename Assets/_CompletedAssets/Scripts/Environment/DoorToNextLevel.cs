using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DoorToNextLevel : MonoBehaviour
{
    public UnityEvent interactAction;
    public void Interact()
    {
        interactAction.Invoke();
    }
}
