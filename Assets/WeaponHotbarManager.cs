using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class HotbarItem
{
    public string playerPrefKey;
    public Sprite icon;
}

public class WeaponHotbarManager : MonoBehaviour
{
    public static WeaponHotbarManager Instance;

    [Header("Slot")]
    public Image[] slots;

    [Header("Icon")]
    public HotbarItem[] items;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        LoadHotbar();
    }

    public void LoadHotbar()
    {
        foreach (Image slot in slots)
            ClearIcon(slot);

        int slotCursor = 0;

        foreach (HotbarItem item in items)
        {
            if (PlayerPrefs.GetInt(item.playerPrefKey, 0) == 1)
            {
                if (slotCursor < slots.Length)
                {
                    SetIcon(slots[slotCursor], item.icon);
                    slotCursor++;
                }
            }
        }
    }

    void SetIcon(Image slot, Sprite icon)
    {
        Image iconImage = slot.transform.Find("Icon").GetComponent<Image>();
        iconImage.sprite = icon;
        iconImage.enabled = true;
    }

    void ClearIcon(Image slot)
    {
        Image iconImage = slot.transform.Find("Icon").GetComponent<Image>();
        iconImage.sprite = null;
        iconImage.enabled = false;
    }
}
