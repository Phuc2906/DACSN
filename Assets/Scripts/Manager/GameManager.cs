using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("UI")]
    public GameObject pauseGameCanvas;
    public GameObject gameoverCanvas;
    public GameObject gamewinCanvas;

    bool isPaused = false;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        Time.timeScale = 1f;
        isPaused = false;

        if (pauseGameCanvas) pauseGameCanvas.SetActive(false);
        if (gameoverCanvas) gameoverCanvas.SetActive(false);
        if (gamewinCanvas) gamewinCanvas.SetActive(false);

        if (PlayerPrefs.GetInt("ReturnFromSetting", 0) == 1)
        {
            PauseGame();
            PlayerPrefs.SetInt("ReturnFromSetting", 0);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J) && !IsEndGameShowing())
        {
            TogglePause();
        }
    }

    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f;

        if (pauseGameCanvas)
            pauseGameCanvas.SetActive(true);
    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;

        if (pauseGameCanvas)
            pauseGameCanvas.SetActive(false);
    }

    public void TogglePause()
    {
        if (isPaused) ResumeGame();
        else PauseGame();
    }

    bool IsEndGameShowing()
    {
        return (gameoverCanvas && gameoverCanvas.activeSelf)
            || (gamewinCanvas && gamewinCanvas.activeSelf);
    }


    public void GameOver()
    {
        Time.timeScale = 0f;
        if (gameoverCanvas)
            gameoverCanvas.SetActive(true);
    }

    public void GameWinner()
    {
        Time.timeScale = 0f;
        if (gamewinCanvas)
            gamewinCanvas.SetActive(true);
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
