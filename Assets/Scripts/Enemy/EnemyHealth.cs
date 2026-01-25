using UnityEngine;
using UnityEngine.UI;
using TMPro; 
using System.Collections;
public class EnemyHealth : MonoBehaviour
{
    [Header("Config")]
    public int enemyID;             
    public int maxHealth = 10;
    public int maxExp = 5;

    private int currentHealth;
    private bool isDead = false;

    public Slider healthBar;

    [Header("UI")]
    public TMP_Text healthText;   

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

        UpdateHealthText(); 
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

        UpdateHealthText(); 

        if (currentHealth <= 0)
            Die();
    }

    void UpdateHealthText()
    {
        if (healthText != null)
            healthText.text = $"{currentHealth}/{maxHealth}";
    }

    void Die()
    {
        if (isDead) return;
        isDead = true;

        anim.SetTrigger("Dead");
        PlayerPrefs.SetInt(deadKey, 1);
        PlayerPrefs.DeleteKey(healthKey);
        PlayerPrefs.Save();

        var exp = FindObjectOfType<PlayerExpManager>();
        if (exp != null)
            exp.GainExp(maxExp);

        Collider2D col = GetComponent<Collider2D>();
        if (col != null) col.enabled = false;

        foreach (var script in GetComponents<MonoBehaviour>())
        {
            if (script != this &&
                !(script is EnemySave) &&
                !(script is Animator))
                script.enabled = false;
        }

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.simulated = false;
        }
        var weaponCtrl = GetComponent<WeaponController_Enemy>();
        if (weaponCtrl != null)
        {
            weaponCtrl.RemoveWeapon();
            weaponCtrl.enabled = false; 
        }

        foreach (GameObject bullet in GameObject.FindGameObjectsWithTag("Bullet_Enemy"))
        Destroy(bullet);

        StartCoroutine(DelayedDestroy());
    }

    IEnumerator DelayedDestroy()
    {
        yield return new WaitForSeconds(1.2f); 

        if (save != null)
            save.Collect();
        else
            Destroy(gameObject);
    }

}