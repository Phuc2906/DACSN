using UnityEngine;

public class ResetGame : MonoBehaviour
{
    public GameObject Canvas_A;
    public void LoadResetGame()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();

        Canvas_A.SetActive(true);
    }
}
