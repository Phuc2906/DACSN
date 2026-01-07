using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [Header("Config")]
    public int enemyID;             
    public int maxHealth = 10;
    public int maxExp = 5;

    private int currentHealth;
    private bool isDead = false;

    public Slider healthBar;
    private Animator anim;
    private EnemySave save;

    string healthKey;
    string deadKey;

    void Start()
    {
        anim = GetComponent<Animator>();
        save = GetComponent<EnemySave>();

        healthKey = "EnemyHealth_" + enemyID;
        deadKey   = "EnemyDead_" + enemyID;

        if (PlayerPrefs.GetInt(deadKey, 0) == 1)
        {
            Destroy(gameObject);
            return;
        }

        if (PlayerPrefs.HasKey(healthKey))
            currentHealth = PlayerPrefs.GetInt(healthKey);
        else
            currentHealth = maxHealth;

        if (healthBar != null)
        {
            healthBar.maxValue = maxHealth;
            healthBar.value = currentHealth;
        }
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        PlayerPrefs.SetInt(healthKey, currentHealth);
        PlayerPrefs.Save();

        if (healthBar != null)
            healthBar.value = currentHealth;

        if (currentHealth <= 0)
            Die();
    }

    void Die()
    {
        isDead = true;
        anim.SetTrigger("Dead");

        PlayerPrefs.SetInt(deadKey, 1);
        PlayerPrefs.DeleteKey(healthKey);
        PlayerPrefs.Save();

        FindObjectOfType<PlayerExpManager>().GainExp(maxExp);

        Collider2D col = GetComponent<Collider2D>();
        if (col != null) col.enabled = false;

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null) rb.isKinematic = true;

        foreach (var script in GetComponents<MonoBehaviour>())
        {
            if (script != this)
                script.enabled = false;
        }

        EnemyGun gun = GetComponentInChildren<EnemyGun>();
        if (gun != null)
            Destroy(gun.gameObject);

        foreach (GameObject bullet in GameObject.FindGameObjectsWithTag("Bullet_Enemy"))
            Destroy(bullet);

        if (save != null)
            save.Collect();
        else
            Destroy(gameObject, 1f);
    }
}
