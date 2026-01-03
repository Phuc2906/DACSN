using UnityEngine;

public class BoxSave : MonoBehaviour
{
    public int boxID;
    string saveKey;

    void Awake()
    {
        saveKey = "Box_" + boxID;

        if (PlayerPrefs.GetInt(saveKey, 0) == 1)
        {
            Destroy(gameObject);
        }
    }

    public void BreakBox()
    {
        PlayerPrefs.SetInt(saveKey, 1);
        PlayerPrefs.Save();
        Destroy(gameObject);
    }
}
