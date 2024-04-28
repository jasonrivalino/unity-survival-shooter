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
            weapons[0].PowerUp();
            weapons[1].PowerUp();
            weapons[2].PowerUp();
        }

        // Update is called once per frame
        void Update()
        {
            // If the 1, 2, or 3 button is being press, change the weapon
#if !MOBILE_INPUT
            if (Input.GetKeyDown(KeyCode.Alpha1)) {
                if (weaponSlotUsed != 1)
                {
                    weaponSlotUsed = 1;
                    weapons[0].UseWeapon();
                    weapons[1].UnUseWeapon();
                    weapons[2].UnUseWeapon();
                }
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                if(weaponSlotUsed != 2)
                {
                    weaponSlotUsed = 2;
                    weapons[0].UnUseWeapon();
                    weapons[1].UseWeapon();
                    weapons[2].UnUseWeapon();
                }
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                if (weaponSlotUsed != 3)
                {
                    weaponSlotUsed = 3;
                    weapons[0].UnUseWeapon();
                    weapons[1].UnUseWeapon();
                    weapons[2].UseWeapon();
                }
            }
        }
    }
#endif
}