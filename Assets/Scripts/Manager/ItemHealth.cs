using UnityEngine;

public class ItemHealth : MonoBehaviour
{
    public int healAmount = 20;

    private ItemHealthSave save;

    void Awake()
    {
        save = GetComponent<ItemHealthSave>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.Heal(healAmount);
        }
        if (save != null)
            save.Collect();
        else
            Destroy(gameObject);
    }
}
