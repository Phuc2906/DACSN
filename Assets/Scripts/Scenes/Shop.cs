using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class Shop : MonoBehaviour
{
    [Header("Mở từ Game?")]
    public bool openedFromGame = false;

    [Header("Warning Canvas")]
    public GameObject warningCanvas;

    [Header("Danh sách Canvas cần check")]
    public List<GameObject> checkCanvases = new List<GameObject>();

    public void OpenShop()
    {
        if (IsAnyCanvasActive())
        {
            if (warningCanvas != null)
                warningCanvas.SetActive(true);
            return;
        }

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

    private bool IsAnyCanvasActive()
    {
        foreach (GameObject canvas in checkCanvases)
        {
            if (canvas != null && canvas.activeSelf)
                return true;
        }
        return false;
    }
}
