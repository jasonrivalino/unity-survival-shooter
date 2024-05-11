using UnityEngine;

namespace CompleteProject
{
    public class ShotgunLine : MonoBehaviour
    {
        // public GameObject shotgunLine;

        void Awake()
        {
            // Create a layer mask for the Shootable layer.
            Destroy(gameObject, 0.1f);
        }
    }
}