using UnityEngine;

public class HealthItem : MonoBehaviour
{
    public float healAmount = 20f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth health = other.GetComponent<PlayerHealth>();
            if (health != null)
            {
                health.Heal((int)healAmount); 
                Debug.Log("Đã hồi máu cho Player!");
                Destroy(gameObject);
            }
            else
            {
                Debug.LogWarning("PlayerHealth not found on " + other.name);
            }
        }
    }
}