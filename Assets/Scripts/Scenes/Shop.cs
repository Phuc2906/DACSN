using UnityEngine;
using UnityEngine.SceneManagement;

public class Shop : MonoBehaviour
{
    [Header("Mở từ Game?")]
    public bool openedFromGame = false;

    public void OpenShop()
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
        }

        PlayerPrefs.Save();
        SceneManager.LoadScene("Shop");
    }
}
