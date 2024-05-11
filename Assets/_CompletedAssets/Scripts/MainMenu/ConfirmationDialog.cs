using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ConfirmationDialog : MonoBehaviour
{
    [Header("Components")]

    [SerializeField] private Text displayText;
    [SerializeField] private Button confirmButton;
    [SerializeField] private Text ConfirmButtonText;
    [SerializeField] private Button cancelButton;
    [SerializeField] private Text CancelButtonText;

    public void ActivateDialog(string displayText, string confirmText, string cancelText, UnityAction confirmAction, UnityAction cancelAction)
    {
        this.displayText.text = displayText;
        this.ConfirmButtonText.text = confirmText;
        this.CancelButtonText.text = cancelText;

        confirmButton.onClick.RemoveAllListeners();
        confirmButton.onClick.AddListener(confirmAction);
        confirmButton.onClick.AddListener(CloseDialog);

        cancelButton.onClick.RemoveAllListeners();
        cancelButton.onClick.AddListener(cancelAction);
        cancelButton.onClick.AddListener(CloseDialog);

        gameObject.SetActive(true);
    }

    public void CloseDialog()
    {
        gameObject.SetActive(false);
    }
}