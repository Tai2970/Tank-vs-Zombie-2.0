using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    public int maxHealth = 3;
    private int currentHealth;
    private bool isDead = false;
    public int CurrentHealth => currentHealth;

    [Header("Health Bar UI")]
    public GameObject healthBarPrefab;            // Drag the HealthBarUI prefab here
    private TankHealthBar healthBar;              // Script reference from the prefab

    private string playerTag;                     // Tag used to identify player
    private SurvivalManager survivalManager;      // Reference to the game manager

    void Start()
    {
        currentHealth = maxHealth;
        playerTag = gameObject.tag;

        // Find the unified SurvivalManager in scene
        survivalManager = Object.FindFirstObjectByType<SurvivalManager>();
        if (survivalManager == null)
        {
            Debug.LogWarning("❌ SurvivalManager not found.");
        }

        // Instantiate floating health bar above player
        if (healthBarPrefab != null)
        {
            GameObject bar = Instantiate(healthBarPrefab);
            healthBar = bar.GetComponent<TankHealthBar>();
            healthBar.target = transform;
            healthBar.SetHealth(currentHealth, maxHealth);
        }
    }

    public void TakeDamage(int amount)
    {
        if (isDead) return;

        currentHealth -= amount;
        Debug.Log($"{gameObject.name} took damage. Current health: {currentHealth}");

        // Update UI
        if (healthBar != null)
        {
            healthBar.SetHealth(currentHealth, maxHealth);
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (isDead) return;
        isDead = true;

        Debug.Log($"{gameObject.name} has been destroyed!");
        gameObject.SetActive(false);

        if (survivalManager != null)
        {
            survivalManager.OnPlayerDeath();
        }
    }
}
