using UnityEngine;

public class WeaponController_Enemy : MonoBehaviour
{
    [Header("Cài đặt súng cho Enemy")]
    public Transform weaponHolder;
    public GameObject gunPrefab;

    private Transform player;
    private GameObject currentWeapon;
    private SpriteRenderer enemySprite;

    void Start()
    {
        enemySprite = GetComponent<SpriteRenderer>();
        EquipWeapon(gunPrefab);
        FindActivePlayer();
    }

    void Update()
    {
        if (player == null || !player.gameObject.activeInHierarchy)
        {
            FindActivePlayer();
            return;
        }

        if (weaponHolder == null) return;

        Vector3 scale = weaponHolder.localScale;
        scale.x = enemySprite.flipX
            ? -Mathf.Abs(scale.x)
            : Mathf.Abs(scale.x);
        weaponHolder.localScale = scale;
    }

    void FindActivePlayer()
    {
        foreach (var p in GameObject.FindGameObjectsWithTag("Player"))
        {
            if (p.activeInHierarchy)
            {
                player = p.transform;
                return;
            }
        }
        player = null;
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

    public void RemoveWeapon()
    {
        if (currentWeapon != null)
        {
            Destroy(currentWeapon);
            currentWeapon = null;
        }
    }
}