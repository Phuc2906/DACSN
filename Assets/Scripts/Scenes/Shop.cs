using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; 
using System.Collections.Generic;

public class Shop : MonoBehaviour
{
    [Header("Scene hiện tại")]
    public string currentSceneName;
    [Header("Scene so sánh")]
    public string compareSceneName;
    [Header("Scene muốn chuyển đến")]
    public string targetSceneName;

    [Header("Pause")]
    public bool savePauseState = false;

    [Header("Canvases")]
    public List<GameObject> lockCanvases = new List<GameObject>();

    [Header("Button")] 
    public Button loadButton;

    void Update() 
    {
        if (loadButton != null)
        {
            loadButton.interactable = !IsAnyLockCanvasActive();
        }
    }

    public void LoadTargetScene()
    {
        if (IsAnyLockCanvasActive())
        {
            return; 
        }

        PlayerPrefs.SetString("LastScene", currentSceneName);

        PlayerMove playerMove = FindObjectOfType<PlayerMove>();
        if (playerMove != null)
            playerMove.SavePosition();
        PlayerPrefs.Save();

        SceneManager.LoadScene(targetSceneName);
    }

    bool IsAnyLockCanvasActive()
    {
        foreach (var canvas in lockCanvases)
        {
            if (canvas != null && canvas.activeInHierarchy)
                return true;
        }
        return false;
    }
}