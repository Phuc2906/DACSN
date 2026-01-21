using UnityEngine;
using System.Collections.Generic;

public class EnemyGun : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 10f;
    public float fireRate = 1f;
    public float detectionRange = 8f;

    private float fireTimer;

    private List<Transform> players = new List<Transform>();
    private Transform targetPlayer;

    void Start()
    {
        RefreshPlayers();
    }

    void Update()
    {
        fireTimer -= Time.deltaTime;

        targetPlayer = GetClosestPlayer();
        if (!targetPlayer) return;

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
        if (!bulletPrefab || !firePoint) return;

        GameObject bullet = Instantiate(
            bulletPrefab,
            firePoint.position,
            firePoint.rotation
        );

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
            rb.linearVelocity = firePoint.right * bulletSpeed;
    }

    void RefreshPlayers()
    {
        players.Clear();
        foreach (var p in GameObject.FindGameObjectsWithTag("Player"))
        {
            if (p.activeInHierarchy)
                players.Add(p.transform);
        }
    }

    Transform GetClosestPlayer()
    {
        Transform closest = null;
        float min = Mathf.Infinity;

        foreach (var p in players)
        {
            if (!p) continue;
            float d = Vector2.Distance(transform.position, p.position);
            if (d < min)
            {
                min = d;
                closest = p;
            }
        }
        return closest;
    }
}
