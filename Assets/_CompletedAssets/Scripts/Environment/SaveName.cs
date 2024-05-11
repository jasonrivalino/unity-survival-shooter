using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class SaveName : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Text displayText;
    public TMP_InputField slotNameInput;
    [SerializeField] private Button confirmButton;
    [SerializeField] private Text ConfirmButtonText;
    public void ActivateDialog(string displayText, string confirmText, UnityAction confirmAction)
    {
        this.displayText.text = displayText;
        this.ConfirmButtonText.text = confirmText;
        confirmButton.onClick.RemoveAllListeners();
        confirmButton.onClick.AddListener(confirmAction);
        confirmButton.onClick.AddListener(CloseDialog);
        gameObject.SetActive(true);
    }
    
    public void CloseDialog()
    {
        gameObject.SetActive(false);
    }
}