// SaveManager.cs
// MelakukanSavePlayer
using UnityEngine;

namespace CompleteProject
{
    public class SaveManager : SaveLoad
    {
        public static SaveManager instance;

        private void Awake()
        {
            if (instance == null)
            {
#if UNITY_EDITOR
                dirPath = Application.dataPath + "/SaveGame";
#else
                dirPath = Application.persistentDataPath;
#endif
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
