using UnityEngine;
using System.Collections.Generic;

public class EnemyGun : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 10f; // giữ biến
    public float fireRate = 1f;
    public float detectionRange = 8f;

    private float fireTimer;

    private List<Transform> players = new List<Transform>();
    private Transform targetPlayer;

    void Update()
    {
        fireTimer -= Time.deltaTime;

        RefreshPlayers();

        targetPlayer = GetClosestPlayer();
        if (targetPlayer == null) return;

        float dist = Vector2.Distance(transform.position, targetPlayer.position);
        if (dist > detectionRange) return;

        if (fireTimer <= 0f)
        {
            Shoot();
            fireTimer = fireRate;
        }
    }

    void Shoot()
    {
        if (!bulletPrefab || !firePoint || !targetPlayer) return;

        GameObject bullet = Instantiate(
            bulletPrefab,
            firePoint.position,
            Quaternion.identity
        );

        Vector2 dir = (targetPlayer.position - firePoint.position).normalized;

        Bullet_Enemy b = bullet.GetComponent<Bullet_Enemy>();
        if (b != null)
        {
            b.SetDirection(dir);
        }
    }

    void RefreshPlayers()
    {
        players.Clear();

        GameObject[] foundPlayers = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject p in foundPlayers)
        {
            if (p.activeInHierarchy)
                players.Add(p.transform);
        }
    }

    Transform GetClosestPlayer()
    {
        Transform closest = null;
        float minDist = Mathf.Infinity;

        foreach (Transform p in players)
        {
            if (p == null) continue;

            float d = Vector2.Distance(transform.position, p.position);
            if (d < minDist)
            {
                minDist = d;
                closest = p;
            }
        }

        return closest;
    }
}
