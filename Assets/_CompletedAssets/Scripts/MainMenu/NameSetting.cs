// NameSetting.cs
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NameSetting : MonoBehaviour
{
    public Text text;
    public TMP_InputField tmpInputField;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("PlayerName"))
        {
            PlayerPrefs.SetString("PlayerName", "Player1");
        }

        string savedName = PlayerPrefs.GetString("PlayerName");
        text.text = savedName;
        tmpInputField.text = savedName;
        
        Debug.Log("NameSetting::Start() savedName: " + savedName);
    }

    public void OnNameListener(string name)
    {
        Debug.Log("NameSetting::OnNameChanged() name: " + name);
        text.text = name;
        PlayerPrefs.SetString("PlayerName", name);
    }

    public void OnAcceptButton()
    {
        string name = tmpInputField.text;
        text.text = name;
        Debug.Log("NameSetting::OnNameChanged() name: " + name);
        PlayerPrefs.SetString("PlayerName", name);
    }   
}