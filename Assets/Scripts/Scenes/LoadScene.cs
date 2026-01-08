using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public string SceneName;
    public void LoadSceneGame()
    {
        PlayerPrefs.DeleteKey("PlayerX");
        PlayerPrefs.DeleteKey("PlayerY");
        PlayerPrefs.DeleteKey("PlayerFacing");
        PlayerPrefs.Save();
        
        SceneManager.LoadScene(SceneName);
    }
}