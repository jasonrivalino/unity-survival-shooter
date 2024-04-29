using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CompleteProject
{ 
    public class CheatManager : MonoBehaviour
    {
        public TMP_InputField cheatInputField;
        public Text cheatStatusText;

        float cheatStatusTextShowTime = 0;
        // Start is called before the first frame update
        void Start()
        {
            // Make cheat input field disappear
            cheatInputField.interactable = false;
            cheatInputField.GetComponent<CanvasGroup>().alpha = 0;
        }

        // Update is called once per frame
        void Update()
        {
            #if !MOBILE_INPUT
            // If the C is pressed Change visibility of cheat input field
            if (Input.GetKeyDown(KeyCode.C))
            {
                if (cheatInputField.interactable) 
                {
                    // Make cheat input field disappear, if it appears
                    HideCheatInputField();
                } else
                {
                    // Make cheat input field appear, if it disappears
                    ShowCheatInputField();
                }
            }
            #else
                            // If there is input on the shoot direction stick and it's time to fire...
                            if ((CrossPlatformInputManager.GetAxisRaw("Mouse X") != 0 || CrossPlatformInputManager.GetAxisRaw("Mouse Y") != 0) && timer >= timeBetweenBullets)
                            {
                                // ... shoot the gun
                                Shoot();
                            }
            #endif

            // Manage cheat status text visibility
            if (cheatStatusTextShowTime > 0 )
            {
                cheatStatusTextShowTime -= Time.deltaTime;

                if (cheatStatusTextShowTime < 0)
                {
                    cheatStatusTextShowTime = 0;
                } 
                cheatStatusText.color = new Color(1, 1, 1, cheatStatusTextShowTime);
            }
        }

        void ShowCheatInputField() 
        {
            cheatInputField.interactable = true;
            cheatInputField.GetComponent<CanvasGroup>().alpha = 1;
        }

        void HideCheatInputField()
        {
            cheatInputField.interactable = false;
            cheatInputField.GetComponent<CanvasGroup>().alpha = 0;
        }

        bool ProcessCheat(string cheat)
        {
            bool activateCheat = true;
            if (PlayerPrefs.HasKey(cheat))
            {
                // Deactivate cheat if it was activated
                activateCheat = false;
                PlayerPrefs.DeleteKey(cheat);
            }
            else
            {
                // Set Key in PlayerPrefs so it can be accessed globally
                PlayerPrefs.SetInt(cheat, 1);
            }
            return activateCheat;
        }

        public void ReadCheatInput(string cheatInput) 
        {
            bool isCheatExisted = true;
            bool isCheatActivated = false;

            // Activate Cheat if exist

            // Cheat no damage
            if (cheatInput == "IMMORTAL")
            {
                isCheatActivated = ProcessCheat("NoDamage");
            }
            else if (cheatInput == "ONE HIT MAN")
            {
                isCheatActivated = ProcessCheat("OneHitKill");
            }
            else if (cheatInput == "KAMINARI NO KOKYU")
            {
                isCheatActivated = ProcessCheat("2xSpeedUp");
            }
            else
            {
                isCheatExisted = false;
            }

            if (isCheatExisted)
            {
                if (isCheatActivated)
                {
                    cheatStatusText.text = "Cheat Activated";
                }
                else
                {
                    cheatStatusText.text = "Cheat Deactivated";
                }
                // Set the cheat input field to empty 
                cheatInputField.text = "";

                // Hide the cheat input field
                HideCheatInputField();
            } else 
            {
                cheatStatusText.text = "Cheat is Not Exist";
            }

            cheatStatusTextShowTime = 1f;
        }
    }
}
