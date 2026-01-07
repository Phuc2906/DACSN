using UnityEngine;
using UnityEngine.UI;

public class WeaponController : MonoBehaviour
{
    [Header("Hotbar UI")]
    public Image[] slots;

    [Header("Weapon data")]
    public WeaponData[] weapons;

    [Header("Weapon holder")]
    public Transform weaponHolder;

    [Header("Slot color")]
    public Color normalColor = Color.white;
    public Color selectedColor = new Color(1f, 1f, 1f, 0.5f);

    private int currentSlot = -1;
    private GameObject currentWeapon;

    private void Start()
    {
        EquipFirstWeapon();
        UpdateSlotVisual();
    }

    private void Update()
    {
        HandleInput();
    }

    void HandleInput()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                TryEquipWeapon(i);
            }
        }
    }

    void TryEquipWeapon(int slotIndex)
    {
        if (slotIndex >= weapons.Length) return;

       
        if (PlayerPrefs.GetInt(weapons[slotIndex].playerPrefKey, 0) != 1)
            return;

        EquipWeapon(slotIndex);
    }

    void EquipWeapon(int slotIndex)
{
    if (currentSlot == slotIndex) return;

    if (currentWeapon != null)
        Destroy(currentWeapon);

    currentWeapon = Instantiate(
        weapons[slotIndex].weaponPrefab,
        weaponHolder
    );

    currentWeapon.transform.localPosition = Vector3.zero;
    currentWeapon.transform.localRotation = Quaternion.identity;
    currentWeapon.transform.localScale = Vector3.one;

    currentSlot = slotIndex;
    UpdateSlotVisual();
}


    void EquipFirstWeapon()
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            if (PlayerPrefs.GetInt(weapons[i].playerPrefKey, 0) == 1)
            {
                EquipWeapon(i);
                break;
            }
        }
    }

    void UpdateSlotVisual()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].color = (i == currentSlot) ? selectedColor : normalColor;
        }
    }
}
