using System.Collections.Generic;
using CompleteProject;
using UnityEngine;

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
        ActivateMenu();
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

    private GameData GetGameData() {
        return new GameData(
            slotName: slotnNameInput.slotNameInput.text,
            sceneName: UnityEngine.SceneManagement.SceneManager.GetActiveScene().name,
            playerName: PlayerPrefs.GetString("PlayerName", "Player1"),
            saveTime: System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
            money: PlayerPrefs.GetInt(Statistics.MoneyCollected, 0),
            score: PlayerPrefs.GetInt(Statistics.TotalScore, 0),
            playTime: PlayerPrefs.GetFloat(Statistics.PlayTime, 0),
            accuracy: PlayerPrefs.GetFloat(Statistics.ShootAccuracy, 0),
            distanceTravelled: PlayerPrefs.GetFloat(Statistics.DistanceTravelled, 0),
            enemiesKilled: PlayerPrefs.GetInt(Statistics.EnemiesKilled, 0),
            orbCollected: PlayerPrefs.GetInt(Statistics.OrbCollected, 0),
            pets: new Dictionary<string, int>()
            {
                { Pets.Rabbit, PlayerPrefs.GetInt(Pets.Rabbit, 0) },
                { Pets.Mushroom, PlayerPrefs.GetInt(Pets.Mushroom, 0) },
                { Pets.Ghost, PlayerPrefs.GetInt(Pets.Ghost, 0) },
                { Pets.Dog, PlayerPrefs.GetInt(Pets.Dog, 0) },
                { Pets.Cactus, PlayerPrefs.GetInt(Pets.Cactus, 0) },
                { Pets.Bomb, PlayerPrefs.GetInt(Pets.Bomb, 0) }
            }
        );
        /* 
        public static class Pets {
        public const string Rabbit = "rabbit";
        public const string Mushroom = "mushroom";
        public const string Ghost = "ghost";
        public const string Dog = "dog";
        public const string Cactus = "cactus";
        public const string Bomb = "bomb";
        }
         */
    }

    public void OnSaveSlotBeforeSave(SaveSlot saveSlot) {
        slotnNameInput.ActivateDialog(
            "Enter Slot Name",
            "Save",
            () => {
                GameData gameData = GetGameData();

                SaveManager.instance.SavePlayer(gameData, saveSlot.ProfileId);

                saveSlot.SetData(SaveManager.instance.LoadPlayer(saveSlot.ProfileId));
            }
        );
    }

    public void OnSaveSlotClicked() 
    {
        SaveSlot saveSlot = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.GetComponent<SaveSlot>();

        if (saveSlot.hasData) 
        {

            // confirmto overwrite then ask like save to this slot

            DisableInteraction();
            confirmationDialog.ActivateDialog (
                "Overwrite this slot?", "Overwrite", "Cancel",
                 () => {
                    SaveManager.instance.DeletePlayer(saveSlot.ProfileId);
                    // ask for slot name
                    OnSaveSlotBeforeSave(saveSlot);

                    ActivateMenu();
                    EnableInteraction();

                 },
                 () => {
                    EnableInteraction();
                 }
            );

        }
        else 
        {
            DisableInteraction();
            confirmationDialog.ActivateDialog (
                "Save to this slot?", "Save", "Cancel",
                 () => {
                    // ask for slot name
                    OnSaveSlotBeforeSave(saveSlot);

                    ActivateMenu();
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
        
    public void ActivateMenu()
    {
        gameObject.SetActive(true);

        Dictionary<string, GameData> players = DataFileHandler.LoadAllPlayers(SaveManager.instance.dirPath, SaveManager.instance.fileName);

        foreach (SaveSlot saveSlot in saveSlots) 
        {
            if (players.ContainsKey(saveSlot.ProfileId)) 
            {
                saveSlot.SetData(players[saveSlot.ProfileId]);
                Debug.Log("SaveMenu::ActivateMenu() " + players[saveSlot.ProfileId]);
            }
            else 
            {
                saveSlot.SetData(null);
            }
        }
    }
}