// DifficultyToggleGroup.cs
using UnityEngine;
using UnityEngine.UI;

public class DifficultyToggleGroup : MonoBehaviour
{
    [SerializeField] private Toggle easy, medium, hard;
    
    private void Start()
    {
        

        if (!PlayerPrefs.HasKey("Difficulty"))
        {
            PlayerPrefs.SetString("Difficulty", "sedang");
        }

        string savedDifficulty = PlayerPrefs.GetString("Difficulty");
        // transform.Find(savedDifficulty).GetComponentInChildren<Toggle>().isOn = true;
        Debug.Log("DifficultyToggleGroup::Start() savedDifficulty: " + savedDifficulty);

        if (savedDifficulty == "mudah")
        {
            easy.isOn = true;
        }
        else if (savedDifficulty == "sedang")
        {
            medium.isOn = true;
        }
        else if (savedDifficulty == "sulit")
        {
            hard.isOn = true;
        }
    }

    public void OnDifficultyChanged(string difficulty)
    {
        Debug.Log("DifficultyToggleGroup::OnDifficultyChanged() difficulty: " + difficulty);
        PlayerPrefs.SetString("Difficulty", difficulty);
    }
}
