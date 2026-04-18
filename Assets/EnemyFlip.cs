using UnityEngine;

public class EnemyFaceActivePlayer : MonoBehaviour
{
    public Transform[] players; // 4 player

    private Transform target;
    private SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        FindActivePlayer();
        FaceTarget();
    }

    void FindActivePlayer()
    {
        target = null;

        foreach (Transform p in players)
        {
            if (p.gameObject.activeInHierarchy)
            {
                target = p;
                break; 
            }
        }
    }

    void FaceTarget()
    {
        if (target == null) return;

        if (target.position.x > transform.position.x)
        {
            sr.flipX = false; 
        }
        else
        {
            sr.flipX = true;
        }
    }
}