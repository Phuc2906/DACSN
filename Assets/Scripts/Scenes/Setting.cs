using UnityEngine;
using UnityEngine.SceneManagement;

public class Setting : MonoBehaviour
{
    [Header("Mở từ Game?")]
    public bool openedFromGame = false;

    public void OpenSetting()
    {
        PlayerPrefs.SetInt("OpenedFromGame", openedFromGame ? 1 : 0);

        if (openedFromGame)
        {

            PlayerPrefs.SetString(
                "LastScene",
                SceneManager.GetActiveScene().name
            );
            PlayerMove player = Object.FindFirstObjectByType<PlayerMove>();
            if (player != null)
                player.SavePosition();

            PlayerPrefs.SetInt("ReturnFromSetting", 1);
        }
        else
        {
            PlayerPrefs.SetInt("ReturnFromSetting", 0);
            PlayerPrefs.DeleteKey("LastScene");
        }

        PlayerPrefs.Save();
        SceneManager.LoadScene("Setting");
    }
}
