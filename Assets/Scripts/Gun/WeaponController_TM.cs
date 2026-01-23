using UnityEngine;

public class WeaponController_TM : MonoBehaviour
{
    [Header("Weapon Settings")]
    public Transform weaponHolder;
    public GameObject gunPrefab;

    private GameObject currentWeapon;
    private SpriteRenderer teammateSprite;

    void Awake()
    {
        teammateSprite = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        EquipWeapon(gunPrefab);
    }

    void LateUpdate()
    {
        if (weaponHolder == null || teammateSprite == null) return;

        Vector3 scale = weaponHolder.localScale;
        scale.x = teammateSprite.flipX
            ? -Mathf.Abs(scale.x)
            : Mathf.Abs(scale.x);

        weaponHolder.localScale = scale;
    }

    public void EquipWeapon(GameObject weaponPrefab)
    {
        if (weaponPrefab == null || weaponHolder == null) return;

        if (currentWeapon != null)
            Destroy(currentWeapon);

        currentWeapon = Instantiate(
            weaponPrefab,
            weaponHolder.position,
            weaponHolder.rotation,
            weaponHolder
        );
    }
}
