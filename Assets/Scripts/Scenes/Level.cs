using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Level : MonoBehaviour
{
    public int levelIndex;
    public Button button;

    [Header("Complete Canvas (Menu)")]
    public GameObject completeCanvas;

    void Start()
    {
        bool unlocked = levelIndex == 1 ||
            PlayerPrefs.GetInt("Level" + (levelIndex - 1), 0) == 1;

        button.interactable = unlocked;
    }

    public void PlayLevel()
    {
        string key = "Level" + levelIndex;

        if (PlayerPrefs.GetInt(key, 0) == 1)
        {
            if (completeCanvas != null)
                completeCanvas.SetActive(true);
        }
        else
        {
            SceneManager.LoadScene("Level" + levelIndex);
        }
    }
}
