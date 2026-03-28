using UnityEngine;
using UnityEngine.UI;

public class CheckCanvas : MonoBehaviour
{
    [Header("UI")]
    public GameObject buttonObject; 

    [Header("PlayerPrefs Key")]
    public string key = "Level4";

    void Start()
    {
        CheckButton();
    }

    void CheckButton()
    {
        if (PlayerPrefs.GetInt(key, 0) == 1)
        {
            buttonObject.SetActive(true);
        }
        else
        {
            buttonObject.SetActive(false);
        }
    }
}