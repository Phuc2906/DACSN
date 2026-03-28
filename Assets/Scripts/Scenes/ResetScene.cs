using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetScene : MonoBehaviour
{
    public string SceneName;
    public void LoadResetSceneGame()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        
        SceneManager.LoadScene(SceneName);
    }
}