using UnityEngine;

public class Get : MonoBehaviour
{
    public string itemKey;

    [Header("UI")]
    public GameObject canvasToClose;

    public void OnClickAddItem()
    {
        PlayerPrefs.SetInt(itemKey, 1);
        PlayerPrefs.Save();

        if (WeaponHotbarManager.Instance != null)
        {
            WeaponHotbarManager.Instance.LoadHotbar();
        }

        if (canvasToClose != null)
            canvasToClose.SetActive(false);
    }
}
