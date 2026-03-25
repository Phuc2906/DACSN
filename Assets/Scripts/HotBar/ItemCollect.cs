using UnityEngine;

public class ItemCollect : MonoBehaviour
{
    [Header("ID")]
    public int itemID;

    string itemKey;

    [Header("Canvas")]
    public GameObject Canvas;
    
    void Awake()
    {
        itemKey = "Item_" + itemID;

        if (PlayerPrefs.GetInt(itemKey, 0) == 1)
            Destroy(gameObject);
            Canvas.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        PlayerPrefs.SetInt(itemKey, 1);
        PlayerPrefs.Save();

        if (WeaponHotbarManager.Instance != null)
            WeaponHotbarManager.Instance.LoadHotbar();

        Destroy(gameObject);
        Canvas.SetActive(true);
    }
}
