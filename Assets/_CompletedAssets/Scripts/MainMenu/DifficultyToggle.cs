// DifficultyToggle.cs
using UnityEngine;
using UnityEngine.UI;

public class DifficultyToggle : MonoBehaviour
{
    Toggle toggle;
    public ToggleGroup toggleGroup;
    public Text text;

    private void Start()
    {
        toggle = GetComponent<Toggle>();
        toggle.onValueChanged.AddListener(OnValueChanged);
    }

    private void OnValueChanged(bool isOn)
    {
        if (isOn)
        {
            string difficulty = text.text.ToLower();
            Debug.Log("DifficultyToggle::OnValueChanged() difficulty: " + difficulty);
            toggleGroup.GetComponent<DifficultyToggleGroup>().OnDifficultyChanged(difficulty);
        }
    }
}
