using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Level : MonoBehaviour
{

    public int levelIndex;
    public Button button;

    void Start()
    {
        bool unlocked = levelIndex == 1 ||
            PlayerPrefs.GetInt("Level" + (levelIndex - 1), 0) == 1;

        button.interactable = unlocked;
    }

    public void PlayLevel()
    {
        SceneManager.LoadScene("Level" + levelIndex);
    }
}
