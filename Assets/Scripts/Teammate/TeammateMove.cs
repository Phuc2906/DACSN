using UnityEngine;
using System.Collections.Generic;

public class TeammateMove : MonoBehaviour
{
    [Header("Players")]
    public List<GameObject> players;

    [Header("Follow Settings")]
    public float followDistance = 1.5f;
    public float moveSpeed = 3f;

    [Header("Ground Check")]
    public float groundCheckDistance = 0.25f;

    [Header("Teleport Rescue")]
    public float maxDistanceY = 2.5f;
    public float teleportOffsetY = 0.1f;

    [Header("Save")]
    public string saveID = "Teammate_01";

    private GameObject activePlayer;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator anim;

    private int obstacleLayerMask;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        obstacleLayerMask = LayerMask.GetMask("Obstacle");

        LoadState(); 
    }

    void Update()
    {
        FindActivePlayer();

        if (activePlayer == null)
        {
            StopMove();
            return;
        }

        HandleFollow();
    }

    void FindActivePlayer()
    {
        foreach (GameObject p in players)
        {
            if (p != null && p.activeInHierarchy)
            {
                activePlayer = p;
                return;
            }
        }

        activePlayer = null;
    }

    void HandleFollow()
    {
        float deltaX = activePlayer.transform.position.x - transform.position.x;
        float absX = Mathf.Abs(deltaX);
        float dir = Mathf.Sign(deltaX);

        float deltaY = Mathf.Abs(
            activePlayer.transform.position.y - transform.position.y
        );

        bool playerGrounded = IsPlayerGrounded();

        if (playerGrounded && deltaY > maxDistanceY)
        {
            TeleportToPlayer();
            return;
        }

        if (absX <= followDistance)
        {
            StopMove();
            return;
        }

        if (!playerGrounded)
        {
            rb.linearVelocity = new Vector2(dir * moveSpeed, rb.linearVelocity.y);
            SetRunning(true);
        }
        else
        {
            Vector3 newPos = transform.position;
            newPos.x = activePlayer.transform.position.x - dir * followDistance;
            transform.position = newPos;

            SetRunning(true);
        }

        if (sr != null)
            sr.flipX = dir < 0;

        SaveState(); 
    }

    bool IsPlayerGrounded()
    {
        Collider2D col = activePlayer.GetComponent<Collider2D>();
        if (col == null) return false;

        Vector2 origin = new Vector2(
            col.bounds.center.x,
            col.bounds.min.y + 0.05f
        );

        RaycastHit2D hit = Physics2D.Raycast(
            origin,
            Vector2.down,
            groundCheckDistance,
            obstacleLayerMask
        );

        return hit.collider != null;
    }

    void TeleportToPlayer()
    {
        Collider2D col = activePlayer.GetComponent<Collider2D>();
        if (col == null) return;

        Vector2 origin = new Vector2(
            col.bounds.center.x,
            col.bounds.min.y
        );

        RaycastHit2D hit = Physics2D.Raycast(
            origin,
            Vector2.down,
            2f,
            obstacleLayerMask
        );

        Vector3 targetPos = activePlayer.transform.position;

        if (hit.collider != null)
            targetPos.y = hit.point.y + teleportOffsetY;
        else
            targetPos.y += teleportOffsetY;

        transform.position = targetPos;

        StopMove();
        SaveState(); 
    }

    void StopMove()
    {
        rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
        SetRunning(false);
    }

    void SetRunning(bool state)
    {
        if (anim != null)
            anim.SetBool("IsRunning", state);
    }

    void SaveState()
    {
        PlayerPrefs.SetFloat(saveID + "_X", transform.position.x);
        PlayerPrefs.SetFloat(saveID + "_Y", transform.position.y);
        PlayerPrefs.SetInt(saveID + "_Facing", sr != null && sr.flipX ? 1 : 0);
        PlayerPrefs.Save();
    }

    void LoadState()
    {
        if (!PlayerPrefs.HasKey(saveID + "_X")) return;

        float x = PlayerPrefs.GetFloat(saveID + "_X");
        float y = PlayerPrefs.GetFloat(saveID + "_Y");
        int facing = PlayerPrefs.GetInt(saveID + "_Facing");

        transform.position = new Vector3(x, y, transform.position.z);

        if (sr != null)
            sr.flipX = facing == 1;
    }

    public void DeleteSave()
    {
        PlayerPrefs.DeleteKey(saveID + "_X");
        PlayerPrefs.DeleteKey(saveID + "_Y");
        PlayerPrefs.DeleteKey(saveID + "_Facing");
        PlayerPrefs.Save();
    }

#if UNITY_EDITOR
    void OnDrawGizmosSelected()
    {
        if (activePlayer == null) return;

        Collider2D col = activePlayer.GetComponent<Collider2D>();
        if (col == null) return;

        Vector3 start = new Vector3(
            col.bounds.center.x,
            col.bounds.min.y + 0.05f,
            0
        );

        Gizmos.color = Color.green;
        Gizmos.DrawLine(start, start + Vector3.down * groundCheckDistance);
    }
#endif
}
