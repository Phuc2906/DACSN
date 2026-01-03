using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Collider2D))]
public class Box : MonoBehaviour
{
    public string playerTag = "Player";   
    public float delayBeforeDestroy = 0.3f;       
    public Transform lootSpawnPoint;     

    private Animator anim;
    private bool opened = false;

    void Awake()
    {
        anim = GetComponent<Animator>();
        anim.enabled = false;

        var col = GetComponent<Collider2D>();
        col.isTrigger = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (opened) return;
        if (!other.CompareTag(playerTag)) return;

        opened = true;
        anim.enabled = true;            
        anim.SetTrigger("Open");       

        StartCoroutine(DestroyAfterAnimation());
    }

    IEnumerator DestroyAfterAnimation()
    {
        AnimatorStateInfo info = anim.GetCurrentAnimatorStateInfo(0);
        float animLength = info.length;

        yield return new WaitForSeconds(animLength + delayBeforeDestroy);

        Destroy(gameObject);
    }
}
