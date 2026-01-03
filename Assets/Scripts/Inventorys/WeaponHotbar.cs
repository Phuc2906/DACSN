using UnityEngine;
using UnityEngine.UI;
using System;



public class WeaponHotbar : MonoBehaviour
{
    [Header("UI icon cho 6 ô")]
    public Image[] slots = new Image[6];

    [Header("Vũ khí trong 6 slot")]
    public WeaponSlotData[] weapons = new WeaponSlotData[6];

    [Header("Shop Weapon Data")]
    public Sprite shopGun01Icon;
    public GameObject shopGun01Prefab;

    public Sprite shopGun02Icon;
    public GameObject shopGun02Prefab;

    public Sprite shopGun03Icon;
    public GameObject shopGun03Prefab;

    public Sprite shopGun04Icon;
    public GameObject shopGun04Prefab;

    public Sprite shopGun05Icon;
    public GameObject shopGun05Prefab;

    public Sprite shopGun06Icon;
    public GameObject shopGun06Prefab;

    [Header("Default Icons")]
    public Sprite defaultIcon1;  // Assign icon cho gunSlot1
    public Sprite defaultIcon2;  // Assign icon cho gunSlot2

    [Header("Hotbar State")]
    public int activeIndex = 0;
    public event Action<WeaponSlotData> OnSelectionChanged;  // Event để Controller equip

    // PlayerPrefs keys
    private string shopGun01Key = "Item_01_Bought";
    private string shopGun02Key = "Item_02_Bought";
    private string shopGun03Key = "Item_03_Bought";
    private string shopGun04Key = "Item_04_Bought";
    private string shopGun05Key = "Item_05_Bought";
    private string shopGun06Key = "Item_06_Bought";

    void Start()
    {
        InitializeWeapons();
        activeIndex = GetHighestBoughtSlot();  // Initial: Chọn slot cao nhất đã mua
        RefreshUI();
        SelectSlot(activeIndex);  // Trigger equip initial
    }

    void InitializeWeapons()
    {
        // Check array size để tránh IndexOutOfRange
        if (weapons.Length != 6)
        {
            Debug.LogWarning("[Hotbar] weapons array size wrong! Expected 6, resizing...");
            System.Array.Resize(ref weapons, 6);
        }

        // Fixed slot: Luôn gán theo vị trí, fallback default icon nếu chưa mua
        // Slot 0: Item_01 hoặc default1
        bool has1 = PlayerPrefs.GetInt(shopGun01Key, 0) == 1;
        weapons[0] = has1 && shopGun01Prefab && shopGun01Icon ?
            new WeaponSlotData { icon = shopGun01Icon, prefab = shopGun01Prefab } :
            new WeaponSlotData { icon = defaultIcon1, prefab = null };

        // Slot 1: Item_02 hoặc default2
        bool has2 = PlayerPrefs.GetInt(shopGun02Key, 0) == 1;
        weapons[1] = has2 && shopGun02Prefab && shopGun02Icon ?
            new WeaponSlotData { icon = shopGun02Icon, prefab = shopGun02Prefab } :
            new WeaponSlotData { icon = defaultIcon2, prefab = null };

        // Slot 2: Item_03 hoặc default1
        bool has3 = PlayerPrefs.GetInt(shopGun03Key, 0) == 1;
        weapons[2] = has3 && shopGun03Prefab && shopGun03Icon ?
            new WeaponSlotData { icon = shopGun03Icon, prefab = shopGun03Prefab } :
            new WeaponSlotData { icon = defaultIcon1, prefab = null };

        // Slot 3: Item_04 hoặc default1
        bool has4 = PlayerPrefs.GetInt(shopGun04Key, 0) == 1;
        weapons[3] = has4 && shopGun04Prefab && shopGun04Icon ?
            new WeaponSlotData { icon = shopGun04Icon, prefab = shopGun04Prefab } :
            new WeaponSlotData { icon = defaultIcon1, prefab = null };

        // Slot 4: Item_05 hoặc default1
        bool has5 = PlayerPrefs.GetInt(shopGun05Key, 0) == 1;
        weapons[4] = has5 && shopGun05Prefab && shopGun05Icon ?
            new WeaponSlotData { icon = shopGun05Icon, prefab = shopGun05Prefab } :
            new WeaponSlotData { icon = defaultIcon1, prefab = null };

        // Slot 5: Item_06 hoặc default1
        bool has6 = PlayerPrefs.GetInt(shopGun06Key, 0) == 1;
        weapons[5] = has6 && shopGun06Prefab && shopGun06Icon ?
            new WeaponSlotData { icon = shopGun06Icon, prefab = shopGun06Prefab } :
            new WeaponSlotData { icon = defaultIcon1, prefab = null };

        Debug.Log("[Hotbar] Initialized fixed slots with bought status");
    }

    private int GetHighestBoughtSlot()
    {
        if (PlayerPrefs.GetInt(shopGun06Key, 0) == 1) return 5;
        if (PlayerPrefs.GetInt(shopGun05Key, 0) == 1) return 4;
        if (PlayerPrefs.GetInt(shopGun04Key, 0) == 1) return 3;
        if (PlayerPrefs.GetInt(shopGun03Key, 0) == 1) return 2;
        if (PlayerPrefs.GetInt(shopGun02Key, 0) == 1) return 1;
        if (PlayerPrefs.GetInt(shopGun01Key, 0) == 1) return 0;
        return 0;  // Default slot 0
    }

    void Update()
    {
        // Input phím 1-6
        for (int i = 0; i < weapons.Length; i++)
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
                SelectSlot(i);

        // Scroll wheel
        float scroll = Input.mouseScrollDelta.y;
        if (scroll != 0)
        {
            int dir = (int)Mathf.Sign(scroll);
            SelectSlot((activeIndex + dir + weapons.Length) % weapons.Length);  // + dir cho scroll up/down đúng
        }
    }

    public void SelectSlot(int idx)
    {
        idx = Mathf.Clamp(idx, 0, weapons.Length - 1);
        if (idx == activeIndex) return;

        activeIndex = idx;
        RefreshUI();
        OnSelectionChanged?.Invoke(GetCurrentSlot());  // Trigger equip ở Controller
        Debug.Log($"[Hotbar] Selected slot {idx}: {weapons[idx].prefab?.name ?? "Default"}");
    }

    void RefreshUI()
    {
        // Check slots array size tương tự, nhưng dùng Warning để không báo Error
        if (slots.Length != 6)
        {
            Debug.LogWarning("[Hotbar] slots array size wrong! Expected 6, resizing...");
            System.Array.Resize(ref slots, 6);
        }

        for (int i = 0; i < slots.Length && i < weapons.Length; i++)
        {
            if (i >= slots.Length || slots[i] == null) continue;

            WeaponSlotData data = weapons[i];
            slots[i].sprite = data.icon;
            slots[i].enabled = true;  // Luôn hiển thị (không ẩn slot)

            slots[i].color = (i == activeIndex) ? Color.white : new Color(1, 1, 1, 0.45f);
        }
    }

    public WeaponSlotData GetCurrentSlot()
    {
        if (activeIndex >= 0 && activeIndex < weapons.Length)
            return weapons[activeIndex];
        return new WeaponSlotData();
    }

    public bool AddWeapon(Sprite icon, GameObject prefab)
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            if (weapons[i].prefab == null)  // Tìm slot trống
            {
                weapons[i] = new WeaponSlotData { icon = icon, prefab = prefab };
                RefreshUI();
                SelectSlot(i);  // Auto select mới
                Debug.Log($"[Hotbar] Added weapon to slot {i}: {prefab?.name}");
                return true;
            }
        }
        Debug.LogWarning("[Hotbar] FULL! Không thể thêm weapon");
        return false;
    }
}