using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneGame : MonoBehaviour
{
    public string SceneName;
    public void LoadScene()
    {
        PlayerPrefs.DeleteKey("Checkpoint");
        PlayerPrefs.DeleteKey("Checkpoint_02");
        PlayerPrefs.DeleteKey("SavedX");
        PlayerPrefs.DeleteKey("SavedY");
        PlayerPrefs.DeleteKey("SavedZ");

        PlayerPrefs.DeleteKey("PlayerX");
        PlayerPrefs.DeleteKey("PlayerY");
        PlayerPrefs.DeleteKey("PlayerFacing");
        PlayerPrefs.Save();
        
        SceneManager.LoadScene(SceneName);
    }
}