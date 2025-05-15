using UnityEngine;

public class Bullet : MonoBehaviour
{
    public enum BulletType { Player, Enemy, SpecialEnemy }
    public BulletType bulletType = BulletType.Player;

    public float speed = 15f;
    public float lifetime = 3f;
    public int damage = 1;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        switch (bulletType)
        {
            case BulletType.Player:
                if (other.CompareTag("Enemy"))
                {
                    Destroy(other.gameObject);
                    Destroy(gameObject);
                }
                break;

            case BulletType.Enemy:
                if (other.CompareTag("Player") || other.CompareTag("Player2"))
                {
                    ApplyDamageToPlayer(other);
                    Destroy(gameObject);
                }
                break;

            case BulletType.SpecialEnemy:
                if (other.CompareTag("Player") || other.CompareTag("Player2") || other.CompareTag("AI"))
                {
                    ApplyDamageToPlayer(other);

                    PlayerHealth health = other.GetComponent<PlayerHealth>();
                    if (health != null && health.CurrentHealth <= 0)
                    {
                        SurvivalManager sm = Object.FindFirstObjectByType<SurvivalManager>();
                        if (sm != null)
                        {
                            sm.OnPlayerDeath();
                        }
                    }

                    Destroy(gameObject);
                }
                break;
        }
    }

    void ApplyDamageToPlayer(Collider player)
    {
        PlayerHealth health = player.GetComponent<PlayerHealth>();
        if (health != null)
        {
            health.TakeDamage(damage);
        }
    }
}