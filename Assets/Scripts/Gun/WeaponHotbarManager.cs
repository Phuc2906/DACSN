using UnityEngine;
using UnityEngine.UI;

public class WeaponHotbarManager : MonoBehaviour
{
    public static WeaponHotbarManager Instance;

    [Header("Hotbar Slots")]
    public Image[] slots;

    [Header("Items")]
    public HotbarItem[] items;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        LoadHotbar();
    }

    public void LoadHotbar()
    {
        
        foreach (Image slot in slots)
            ClearIcon(slot);

        
        foreach (HotbarItem item in items)
        {
            if (PlayerPrefs.GetInt(item.playerPrefKey, 0) == 1)
            {
                if (item.slotIndex >= 0 && item.slotIndex < slots.Length)
                {
                    SetIcon(slots[item.slotIndex], item.icon);
                }
            }
        }
    }

    void SetIcon(Image slot, Sprite icon)
    {
        Transform iconTf = slot.transform.Find("Icon");
        if (iconTf == null) return;

        Image iconImage = iconTf.GetComponent<Image>();
        iconImage.sprite = icon;
        iconImage.enabled = true;
    }

    void ClearIcon(Image slot)
    {
        Transform iconTf = slot.transform.Find("Icon");
        if (iconTf == null) return;

        Image iconImage = iconTf.GetComponent<Image>();
        iconImage.sprite = null;
        iconImage.enabled = false;
    }
}
