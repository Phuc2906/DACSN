using UnityEngine;

public class WeaponController_Enemy : MonoBehaviour
{
    [Header("Cài đặt súng cho Enemy")]
    public Transform weaponHolder;
    public GameObject gunPrefab;

    private Transform player;
    private GameObject currentWeapon;
    private SpriteRenderer enemySprite;
    private Gun currentGun;

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

        if (currentWeapon == null) return;

        Vector3 direction = player.position - currentWeapon.transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        currentWeapon.transform.rotation = Quaternion.Euler(0, 0, angle);

        var weaponSprite = currentWeapon.GetComponent<SpriteRenderer>();
        if (weaponSprite != null)
            weaponSprite.flipX = enemySprite.flipX;
    }

    void FindActivePlayer()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject p in players)
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
        if (weaponPrefab == null) return;

        if (currentWeapon != null)
            Destroy(currentWeapon);

        currentWeapon = Instantiate(
            weaponPrefab,
            weaponHolder.position,
            weaponHolder.rotation,
            weaponHolder
        );

        currentGun = currentWeapon.GetComponent<Gun>();
    }
}
