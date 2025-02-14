using System.Collections.Generic;
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
                    
                    MainMenu.ResetCheatData();
                    MainMenu.ResetGameStatData();
                    MainMenu.ResetPetsData();

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
                LoadManager.instance.DeletePlayer(saveSlot.ProfileId);
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
        GameData gameData = LoadManager.instance.LoadPlayer(profileId);
        

        MainMenu.ResetGameStatData();
        MainMenu.ResetPetsData();
        MainMenu.ResetOrbData();

        levelLoader.LoadLevelString(gameData.sceneName);
    }

    public void ActivareMenu()
    {
        gameObject.SetActive(true);

        Dictionary<string, GameData> players = DataFileHandler.LoadAllPlayers(LoadManager.instance.dirPath, LoadManager.instance.fileName);

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