using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CompleteProject
{

    public class PlayerWeaponManager : MonoBehaviour
    {
        public int weaponSlotUsed = 0;
        public float powerUp = 0f;
        public Text powerUpText;
        
        public Weapon[] weapons;

        private void Awake()
        {
            powerUpText.text = "x 1.0";
        }

        public void PowerUp() {
            powerUp += 0.1f;
            weapons[0].PowerUp();
            weapons[1].PowerUp();
            weapons[2].PowerUp();

            powerUpText.text = "x " + (1 + powerUp);
        }

        void ChangeWeapon(int weaponSlot) {
            weaponSlotUsed = weaponSlot;
            for (int i = 0; i <= 2; i++) {
                if (i == weaponSlot)
                {
                    weapons[i].UseWeapon();
                }
                else {
                    weapons[i].UnUseWeapon();
                }
            }
        
        }

        // Update is called once per frame
        void Update()
        {
#if !MOBILE_INPUT
            // If the 1, 2, or 3 button is being press, change the weapon
            if (Input.GetKeyDown(KeyCode.Alpha1)) {
                if (weaponSlotUsed != 0)
                {
                    ChangeWeapon(0);
                }
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                if(weaponSlotUsed != 1)
                {
                    ChangeWeapon(1);
                }
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                if (weaponSlotUsed != 2)
                {
                    ChangeWeapon(2);
                }
            }

            // If mousewheel scrolled, change the weapon
            if (Input.GetAxisRaw("Mouse ScrollWheel") > 0f)
            {
                ChangeWeapon((weaponSlotUsed - 1 + weapons.Length) % weapons.Length);

            } else if (Input.GetAxisRaw("Mouse ScrollWheel") < 0f)
            {
                ChangeWeapon((weaponSlotUsed + 1 + weapons.Length) % weapons.Length);
            }
        }
    }
#endif
}