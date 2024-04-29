using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace CompleteProject
{ 
    public class CheatManager : MonoBehaviour
    {
        public TMP_InputField cheatInputField;
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

        public void ReadCheatInput(string cheatInput) 
        {
            







            // Set the cheat input field to empty 
            cheatInputField.text = "";

            // Hide the cheat input field
            HideCheatInputField();
        }
    }
}
