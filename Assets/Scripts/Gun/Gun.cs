using UnityEngine;

public class Gun : MonoBehaviour
{
    public Transform player;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireRate = 0.3f;

    float fireTimer;

    void Update()
    {
        if (player == null) return;

        fireTimer -= Time.deltaTime;

        if (Input.GetKey(KeyCode.Z) && fireTimer <= 0f)
        {
            Shoot();
            fireTimer = fireRate;
        }
    }

    void Shoot()
    {
        if (!bulletPrefab || !firePoint) return;

        GameObject bullet = Instantiate(
            bulletPrefab,
            firePoint.position,
            Quaternion.identity
        );

        Bullet bulletScript = bullet.GetComponent<Bullet>();
        if (bulletScript == null) return;

        Vector2 dir = player.localScale.x > 0 ? Vector2.right : Vector2.left;
        bulletScript.SetDirection(dir);
    }
}
