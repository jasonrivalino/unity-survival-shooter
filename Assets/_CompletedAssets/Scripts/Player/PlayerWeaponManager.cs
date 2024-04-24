using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CompleteProject
{

    public class PlayerWeaponManager : MonoBehaviour
    {
        public int weaponSlotUsed = 1;
        public float powerUp = 0f;
        public Weapon[] weapons;

        public void PowerUp() {
            powerUp += 0.1f;

        }

        // Update is called once per frame
        void Update()
        {
            // If the 1, 2, or 3 button is being press, change the weapon
#if !MOBILE_INPUT
            if (Input.GetKeyDown(KeyCode.Alpha1)) {
                Debug.Log("1 is pressed");
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                Debug.Log("2 is pressed");
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                Debug.Log("3 is pressed");
            }
        }
    }
#endif
}