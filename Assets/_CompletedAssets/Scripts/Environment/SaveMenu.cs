using System.Collections.Generic;
using System.Xml.Serialization;
using CompleteProject;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SaveMenu : MonoBehaviour {
    [Header("Confirmation Dialog")]
    [SerializeField] private ConfirmationDialog confirmationDialog;
    [SerializeField] private SaveName slotnNameInput;
    
    private SaveSlot[] saveSlots;

    private void Awake() 
    {
        saveSlots = GetComponentsInChildren<SaveSlot>();
    }

    private void Start()
    {
        ActivareMenu();
    }

    private void DisableInteraction() 
    {
        foreach (SaveSlot saveSlot in saveSlots) 
        {
            saveSlot.SetInteractable(false);
        }
    }

    private void EnableInteraction() 
    {
        foreach (SaveSlot saveSlot in saveSlots) 
        {
            saveSlot.SetInteractable(true);
        }
    }

    public void OnSaveSlotClicked() 
    {
        SaveSlot saveSlot = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.GetComponent<SaveSlot>();

        if (saveSlot.hasData) 
        {
            // confirmationDialog.ActivateDialog (
            //     "Load this save?", "Load", "Cancel",
            //      () => {
            //         LoadGame(saveSlot.ProfileId);
            //      },
            //      () => {
            //         EnableInteraction();
            //      }
            // );
        }
        else 
        {
            confirmationDialog.ActivateDialog (
                "Save to this slot?", "Save", "Cancel",
                 () => {
                    DisableInteraction();
                    // ask for slot name
                    slotnNameInput.ActivateDialog(
                        "Enter Slot Name",
                        "Save",
                        () => {
                            SaveManager.instance.SavePlayer(new GameData(
                                string.IsNullOrEmpty(slotnNameInput.slotNameInput.text) || string.IsNullOrWhiteSpace(slotnNameInput.slotNameInput.text) ? saveSlot.GetSlotName() : slotnNameInput.slotNameInput.text,
                                PlayerPrefs.GetString("PlayerName", "Player1"),
                                0,
                                0,
                                UnityEngine.SceneManagement.SceneManager.GetActiveScene().name,
                                System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                                (System.DateTime.Now - System.DateTime.Now.AddMinutes(-10)).ToString()
                                // new string[] { "dog", "rabbit", "mushroom", "ghost", "cactus", "bomb" }
                                
                                ), saveSlot.ProfileId);

                            saveSlot.SetData(SaveManager.instance.LoadPlayer(saveSlot.ProfileId));
                        }
                    );

                    ActivareMenu();
                    EnableInteraction();

                 },
                 () => {
                    EnableInteraction();
                 }
            );
        }
    }

    // Ssave the Game
    public void SaveGame(string profileId)
    {
        DisableInteraction();
    }
        
    public void ActivareMenu()
    {
        gameObject.SetActive(true);

        Dictionary<string, GameData> players = DataFileHandler.LoadAllPlayers(SaveManager.instance.dirPath, SaveManager.instance.fileName);

        foreach (SaveSlot saveSlot in saveSlots) 
        {
            if (players.ContainsKey(saveSlot.ProfileId)) 
            {
                saveSlot.SetData(players[saveSlot.ProfileId]);
                Debug.Log("SaveMenu::ActivareMenu() " + players[saveSlot.ProfileId]);
            }
            else 
            {
                saveSlot.SetData(null);
            }
        }
    }
}