using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public float lifeTime = 2f;

    public GameObject buffCanvas;
    public int normalDamage = 5;

    [Header("Auto Aim")]
    public float detectRange = 6f;
    public LayerMask enemyLayer;

    private Vector2 direction;

    public void SetDirection(Vector2 defaultDir)
    {
        Transform enemy = FindNearestEnemy();

        if (enemy != null)
            direction = (enemy.position - transform.position).normalized;
        else
            direction = defaultDir.normalized;

        RotateBullet();
    }

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
    }

    void RotateBullet()
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    Transform FindNearestEnemy()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(
            transform.position,
            detectRange,
            enemyLayer
        );

        if (hits.Length == 0) return null;

        Transform nearest = null;
        float minDist = Mathf.Infinity;

        foreach (var hit in hits)
        {
            float dist = Vector2.Distance(transform.position, hit.transform.position);
            if (dist < minDist)
            {
                minDist = dist;
                nearest = hit.transform;
            }
        }

        return nearest;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            EnemyHealth enemy = collision.GetComponent<EnemyHealth>();
            if (enemy != null)
            {
                int finalDamage = normalDamage;
                if (buffCanvas != null && buffCanvas.activeSelf)
                    finalDamage *= 2;

                enemy.TakeDamage(finalDamage);
                Destroy(gameObject);
            }
        }
    }
}
