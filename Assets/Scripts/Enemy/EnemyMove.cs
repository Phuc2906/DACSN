using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyMove : MonoBehaviour
{
    public int enemyID;

    public float patrolSpeed = 2f;
    public float chaseSpeed = 3.5f;

    public float detectRange = 4f;
    public float maxHeightDiff = 1.2f;
    private Transform player;

    public Transform groundCheck;
    public float groundCheckDistance = 0.6f;
    public LayerMask obstacleLayer;

    public float ignoreEdgeTime = 0.15f;

    public SpriteRenderer sprite;

    private Rigidbody2D rb;
    private bool movingRight = true;
    private float ignoreEdgeTimer;

    public bool IsFacingRight => movingRight;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        FindActivePlayer();
    }

    void FixedUpdate()
    {
        if (player == null || !player.gameObject.activeInHierarchy)
            FindActivePlayer();

        bool seePlayer = PlayerInRange();

        if (ignoreEdgeTimer > 0)
        {
            ignoreEdgeTimer -= Time.fixedDeltaTime;
        }
        else
        {
            if (!IsGroundAhead())
            {
                Flip();
                return;
            }
        }

        if (seePlayer)
        {
            FacePlayer();
            Move(chaseSpeed);
        }
        else
        {
            Move(patrolSpeed);
        }
    }

    void Move(float speed)
    {
        rb.linearVelocity = new Vector2(
            (movingRight ? 1 : -1) * speed,
            rb.linearVelocity.y
        );
    }

    bool PlayerInRange()
    {
        if (player == null) return false;

        float dx = Mathf.Abs(player.position.x - transform.position.x);
        float dy = Mathf.Abs(player.position.y - transform.position.y);

        return dx <= detectRange && dy <= maxHeightDiff;
    }

    void FacePlayer()
    {
        bool playerOnRight = player.position.x > transform.position.x;
        if (playerOnRight != movingRight)
            Flip();
    }

    bool IsGroundAhead()
    {
        return Physics2D.Raycast(
            groundCheck.position,
            Vector2.down,
            groundCheckDistance,
            obstacleLayer
        );
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (!collision.collider.CompareTag("Obstacle")) return;
        if (ignoreEdgeTimer > 0) return;

        foreach (ContactPoint2D c in collision.contacts)
        {
            if (Mathf.Abs(c.normal.x) > 0.5f)
            {
                Flip();
                return;
            }
        }
    }

    void Flip()
    {
        movingRight = !movingRight;
        ignoreEdgeTimer = ignoreEdgeTime;

        if (sprite != null)
            sprite.flipX = !movingRight;
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
}
