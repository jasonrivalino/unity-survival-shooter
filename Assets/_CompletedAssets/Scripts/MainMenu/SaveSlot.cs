using UnityEngine;
using UnityEngine.UI;

public class SaveSlot : MonoBehaviour
{
    [Header("Profile Slot Info")]
    [SerializeField] private string profileId;

    [Header("Slot Info")]
    [SerializeField] private GameObject noDataUI;
    [SerializeField] private GameObject hasDataUI;

    [Header("Slot available data")]
    [SerializeField] private Text slotName;
    [SerializeField] private Text sceneName;
    [SerializeField] private Text playerName;
    [SerializeField] private Text saveTime;
    [SerializeField] private Text money;
    [SerializeField] private Text score;
    // [SerializeField] private Text playTime;
    
    public bool hasData { get; private set;} = false;
    Button loadButton;

    public string ProfileId { get => profileId; set => profileId = value; }

    private void Awake()
    {
        loadButton = GetComponent<Button>();
    }

    public void SetData(GameData gameData) 
    {
        // No data for this profileId
        if (gameData == null) 
        {
            slotName.text = "Empty Slot";
            hasData = false;
            noDataUI.SetActive(true);
            hasDataUI.SetActive(false);
            Debug.Log("SaveSlot::SetData() " + profileId + " gameData is null");
        }
        // Data found for this profileId
        else 
        {
            hasData = true;
            noDataUI.SetActive(false);
            hasDataUI.SetActive(true);

            slotName.text = gameData.slotName;
            sceneName.text = gameData.sceneName;
            playerName.text = gameData.playerName;
            saveTime.text = gameData.saveTime;
            money.text = gameData.money.ToString();
            score.text = gameData.score.ToString();
            // playTime.text = gameData.playTime;
        }
    }

    public string GetSlotName() 
    {
        return slotName.text;
    }

    public void SetInteractable(bool interactable) 
    {
        loadButton.interactable = interactable;
    }
}
