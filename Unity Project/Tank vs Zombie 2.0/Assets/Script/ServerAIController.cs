using UnityEngine;

public class ServerAIController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float followDistance = 3f;
    public float fireRange = 15f;
    public float fireCooldown = 1.5f;

    private float fireTimer;
    private Transform player1;
    private TankShooter shooter;

    void Start()
    {
        player1 = GameObject.FindGameObjectWithTag("Player1")?.transform;
        shooter = GetComponent<TankShooter>();
    }

    void Update()
    {
        if (player1 == null) return;

        fireTimer -= Time.deltaTime;

        // ✅ Always follow player
        FollowPlayer();

        // ✅ Only shoot if enemies exist
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance <= fireRange && fireTimer <= 0f)
            {
                shooter.Fire();
                fireTimer = fireCooldown;
                break;
            }
        }
    }

    void FollowPlayer()
    {
        // Target position is behind the player
        Vector3 targetPos = player1.position - player1.forward * followDistance;
        float distanceToTarget = Vector3.Distance(transform.position, targetPos);

        // Only move if AI is not already close
        if (distanceToTarget > 1f)
        {
            Vector3 direction = (targetPos - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        }
    }

}
