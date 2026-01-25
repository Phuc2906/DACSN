using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyMove : MonoBehaviour
{
    [Header("ID")]
    public int enemyID;

    [Header("Speed")]
    public float patrolSpeed = 2f;
    public float chaseSpeed = 3.5f;

    [Header("Detect Player")]
    public float detectRange = 4f;
    public float maxHeightDiff = 1.2f;

    [Header("Same Y Check")]
    public float sameYThreshold = 0.1f;

    private Transform player;

    [Header("Ground Check (Edge)")]
    public Transform groundCheck;
    public float groundCheckDistance = 0.6f;

    [Header("Ground Body Check")]
    public Transform groundBodyCheck;
    public LayerMask obstacleLayer;

    [Header("Edge Ignore")]
    public float ignoreEdgeTime = 0.15f;

    [Header("Visual")]
    public SpriteRenderer sprite;

    private Rigidbody2D rb;
    private bool movingRight = true;
    private float ignoreEdgeTimer;

    private Vector2 spawnPosition;

    // ===== SAVE KEY (GIỐNG SCRIPT MẪU) =====
    private string keyX;
    private string keyY;
    private string keyFacing;

    public bool IsFacingRight => movingRight;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        spawnPosition = transform.position;
        FindActivePlayer();

        // Init save keys
        keyX = "Enemy_X_" + enemyID;
        keyY = "Enemy_Y_" + enemyID;
        keyFacing = "Enemy_Facing_" + enemyID;

        LoadEnemy();
    }

    void FixedUpdate()
    {
        if (player == null || !player.gameObject.activeInHierarchy)
            FindActivePlayer();

        if (!IsGrounded())
        {
            TeleportBackToSpawn();
            return;
        }

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

        bool seePlayer = PlayerInRange();
        bool sameGround = seePlayer && PlayerSameGround();
        bool sameY = seePlayer && PlayerSameY();

        if (seePlayer && sameGround && sameY)
        {
            FacePlayer();
            Move(chaseSpeed);
        }
        else
        {
            Move(patrolSpeed);
        }

        // ===== AUTO SAVE (GIỐNG SCRIPT DƯỚI) =====
        PlayerPrefs.SetFloat(keyX, transform.position.x);
        PlayerPrefs.SetFloat(keyY, transform.position.y);

        if (sprite != null)
            PlayerPrefs.SetInt(keyFacing, movingRight ? 1 : 0);
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

    bool PlayerSameY()
    {
        if (player == null) return false;
        float dy = Mathf.Abs(player.position.y - transform.position.y);
        return dy <= sameYThreshold;
    }

    bool PlayerSameGround()
    {
        if (player == null) return false;

        RaycastHit2D enemyHit = Physics2D.Raycast(
            groundBodyCheck.position,
            Vector2.down,
            groundCheckDistance,
            obstacleLayer
        );

        RaycastHit2D playerHit = Physics2D.Raycast(
            player.position,
            Vector2.down,
            groundCheckDistance,
            obstacleLayer
        );

        if (!enemyHit || !playerHit) return false;
        return enemyHit.collider == playerHit.collider;
    }

    void FacePlayer()
    {
        bool playerOnRight = player.position.x > transform.position.x;
        if (playerOnRight != movingRight)
            Flip();
    }

    bool IsGrounded()
    {
        return Physics2D.Raycast(
            groundBodyCheck.position,
            Vector2.down,
            groundCheckDistance,
            obstacleLayer
        );
    }

    bool IsGroundAhead()
    {
        Vector2 dir = movingRight ? Vector2.right : Vector2.left;
        Vector2 origin = (Vector2)groundBodyCheck.position + dir * 0.4f;

        return Physics2D.Raycast(
            origin,
            Vector2.down,
            groundCheckDistance,
            obstacleLayer
        );
    }

    void TeleportBackToSpawn()
    {
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;

        transform.position = spawnPosition;

        ignoreEdgeTimer = ignoreEdgeTime;
        movingRight = true;

        if (sprite != null)
            sprite.flipX = false;
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

    // ===== MANUAL SAVE (GIỮ TÊN HÀM CŨ) =====
    public void SaveEnemy()
    {
        PlayerPrefs.SetFloat(keyX, transform.position.x);
        PlayerPrefs.SetFloat(keyY, transform.position.y);
        PlayerPrefs.SetInt(keyFacing, movingRight ? 1 : 0);
    }

    // ===== LOAD (GIỐNG SCRIPT MẪU) =====
    public void LoadEnemy()
    {
        if (!PlayerPrefs.HasKey(keyX) || !PlayerPrefs.HasKey(keyY))
            return;

        float x = PlayerPrefs.GetFloat(keyX);
        float y = PlayerPrefs.GetFloat(keyY);

        transform.position = new Vector2(x, y);
        spawnPosition = transform.position;

        if (PlayerPrefs.HasKey(keyFacing))
            movingRight = PlayerPrefs.GetInt(keyFacing) == 1;

        if (sprite != null)
            sprite.flipX = !movingRight;
    }
}
