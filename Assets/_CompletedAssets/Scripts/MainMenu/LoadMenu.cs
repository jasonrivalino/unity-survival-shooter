using System.Collections.Generic;
using System.Xml.Serialization;
using CompleteProject;
using UnityEngine;
using UnityEngine.UI;

public class LoadMenu : MonoBehaviour {
    [Header("Confirmation Dialog")]
    [SerializeField] private ConfirmationDialog confirmationDialog;
    
    [Header("Navigation Buttons")] 
    [SerializeField] private Button backButton;

    [Header("Transition")]
    [SerializeField] private LevelLoader levelLoader;

    private SaveSlot[] saveSlots;

    private void Awake() 
    {
        saveSlots = GetComponentsInChildren<SaveSlot>();
        ActivareMenu();
    }

    private void DisableInteraction() 
    {
        foreach (SaveSlot saveSlot in saveSlots) 
        {
            saveSlot.SetInteractable(false);
        }

        backButton.interactable = false;
    }

    private void EnableInteraction() 
    {
        foreach (SaveSlot saveSlot in saveSlots) 
        {
            saveSlot.SetInteractable(true);
        }

        backButton.interactable = true;
    }

    public void OnSaveSlotClicked() 
    {
        SaveSlot saveSlot = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.GetComponent<SaveSlot>();

        if (saveSlot.hasData) 
        {
            confirmationDialog.ActivateDialog (
                "Load this save?", "Load", "Cancel",
                 () => {
                    LoadGame(saveSlot.ProfileId);
                 },
                 () => {
                    EnableInteraction();
                 }
            );
        }
    }

    public void OnDeleteButtonClicked(SaveSlot saveSlot) 
    {
        confirmationDialog.ActivateDialog (
            "Delete this save?", "Delete", "Cancel",
            () => {
                SaveManager.instance.DeletePlayer(saveSlot.ProfileId);
                saveSlot.SetData(null);
                EnableInteraction();
            },
            () => {
                EnableInteraction();
            }
        );
    }
        
/* 
Data yang harus sama ketika selesai load
 */
    public void LoadGame(string profileId)
    {
        DisableInteraction();
        GameData gameData = SaveManager.instance.LoadPlayer(profileId);
        levelLoader.LoadLevelString(gameData.sceneName);
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
            }
            else 
            {
                saveSlot.SetData(null);
            }
        }
    }
}