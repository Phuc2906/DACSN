using UnityEngine;

public class ItemHealthSave : MonoBehaviour
{
    [Header("ID duy nhất cho ItemHealth")]
    public int itemID;

    string saveKey;

    void Awake()
    {
        saveKey = "ItemHealth_" + itemID;

        if (PlayerPrefs.GetInt(saveKey, 0) == 1)
        {
            Destroy(gameObject);
        }
    }

    public void Collect()
    {
        PlayerPrefs.SetInt(saveKey, 1);
        PlayerPrefs.Save();
        Destroy(gameObject);
    }
}
