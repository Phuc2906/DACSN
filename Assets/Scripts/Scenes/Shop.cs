using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; 
using System.Collections.Generic;

public class Shop : MonoBehaviour
{
    [Header("Mở từ Game?")]
    public bool openedFromGame = false;

    [Header("Danh sách Canvas")]
    public List<GameObject> checkCanvases = new List<GameObject>();

    [Header("Button mở shop")] 
    public Button openShopButton;

    void Update() 
    {
        if (openShopButton != null)
        {
            openShopButton.interactable = !IsAnyCanvasActive();
        }
    }

    public void OpenShop()
    {
        if (IsAnyCanvasActive())
        {
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