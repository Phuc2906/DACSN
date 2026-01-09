using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyMove : MonoBehaviour
{
    [Header("Unique ID for saving enemy state")]
    public int enemyID;

    [Header("Speed")]
    public float patrolSpeed = 2f;
    public float chaseSpeed = 3.5f;

    [Header("Detect Player")]
    public float detectRange = 4f;
    public float maxHeightDiff = 1.2f;
    private Transform player;

    [Header("Ground / Wall Check")]
    public Transform groundCheck;
    public Transform wallCheck;
    public float groundCheckDistance = 0.6f;
    public float wallCheckDistance = 0.3f;
    public LayerMask groundLayer;

    [Header("Edge Fix")]
    public float ignoreEdgeTime = 0.15f;

    private Rigidbody2D rb;
    private bool movingRight = true;
    private float ignoreEdgeTimer;

    private string keyX;
    private string keyY;
    private string keyFacing;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        keyX = "Enemy_X_" + enemyID;
        keyY = "Enemy_Y_" + enemyID;
        keyFacing = "Enemy_Facing_" + enemyID;

        if (PlayerPrefs.HasKey(keyX) && PlayerPrefs.HasKey(keyY))
        {
            float x = PlayerPrefs.GetFloat(keyX);
            float y = PlayerPrefs.GetFloat(keyY);
            transform.position = new Vector3(x, y, transform.position.z);
        }

        if (PlayerPrefs.HasKey(keyFacing))
        {
            movingRight = PlayerPrefs.GetInt(keyFacing) == 1;
            Vector3 scale = transform.localScale;
            scale.x = Mathf.Abs(scale.x) * (movingRight ? 1 : -1);
            transform.localScale = scale;
        }

        FindActivePlayer();
    }

    void FixedUpdate()
    {
        if (player == null || !player.gameObject.activeInHierarchy)
        {
            FindActivePlayer();
        }

        bool seePlayer = PlayerInRange();

        if (ignoreEdgeTimer > 0)
        {
            ignoreEdgeTimer -= Time.fixedDeltaTime;
        }
        else
        {
            if (!IsGroundAhead() || IsWallAhead())
            {
                StopMove();
                Flip();
                return;
            }
        }

        if (seePlayer)
        {
            FacePlayer();
            Chase();
        }
        else
        {
            Patrol();
        }

        PlayerPrefs.SetFloat(keyX, transform.position.x);
        PlayerPrefs.SetFloat(keyY, transform.position.y);
        PlayerPrefs.SetInt(keyFacing, movingRight ? 1 : 0);
        PlayerPrefs.Save();
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

    void Patrol()
    {
        float dir = movingRight ? 1 : -1;
        rb.linearVelocity = new Vector2(dir * patrolSpeed, rb.linearVelocity.y);
    }

    void Chase()
    {
        float dir = movingRight ? 1 : -1;
        rb.linearVelocity = new Vector2(dir * chaseSpeed, rb.linearVelocity.y);
    }

    void StopMove()
    {
        rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
    }

    bool PlayerInRange()
    {
        if (player == null) return false;

        float distX = Mathf.Abs(player.position.x - transform.position.x);
        float heightDiff = Mathf.Abs(player.position.y - transform.position.y);

        return distX <= detectRange && heightDiff <= maxHeightDiff;
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
            groundLayer
        );
    }

    bool IsWallAhead()
    {
        Vector2 dir = movingRight ? Vector2.right : Vector2.left;
        return Physics2D.Raycast(
            wallCheck.position,
            dir,
            wallCheckDistance,
            groundLayer
        );
    }

    void Flip()
    {
        movingRight = !movingRight;
        ignoreEdgeTimer = ignoreEdgeTime;

        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    void OnDrawGizmos()
    {
        if (groundCheck)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(
                groundCheck.position,
                groundCheck.position + Vector3.down * groundCheckDistance
            );
        }

        if (wallCheck)
        {
            Gizmos.color = Color.blue;
            Vector3 dir = movingRight ? Vector3.right : Vector3.left;
            Gizmos.DrawLine(
                wallCheck.position,
                wallCheck.position + dir * wallCheckDistance
            );
        }

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectRange);
    }
}
