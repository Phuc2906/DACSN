using UnityEngine;
using UnityEngine.SceneManagement;

public class Return : MonoBehaviour
{
    public string defaultScene = "MainMenu";

    public void Back()
    {
        bool openedFromGame =
            PlayerPrefs.GetInt("OpenedFromGame", 0) == 1;

        if (openedFromGame && PlayerPrefs.HasKey("LastScene"))
        {
            SceneManager.LoadScene(
                PlayerPrefs.GetString("LastScene")
            );
        }
        else
        {
            SceneManager.LoadScene(defaultScene);
        }
    }
}
