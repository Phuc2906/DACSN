using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyAttack : MonoBehaviour
{
    public int damage = 5;
    public float attackCooldown = 0.6f;

    [HideInInspector] public bool isAttacking;

    private float lastHitTime;
    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        TryAttack(collision);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        TryAttack(collision);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
            isAttacking = false;
    }

    void TryAttack(Collision2D collision)
    {
        if (!collision.collider.CompareTag("Player")) return;

        isAttacking = true;

        rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);

        if (Time.time - lastHitTime < attackCooldown) return;

        DealDamage(collision);
        lastHitTime = Time.time;
    }

    void DealDamage(Collision2D collision)
    {
        if (collision.collider.TryGetComponent(out PlayerHealth player))
        {
            player.TakeDamage(damage);
        }
    }
}
