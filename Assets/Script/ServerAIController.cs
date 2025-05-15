using UnityEngine;

public class ServerAIController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public float followDistance = 3f;

    [Header("Combat")]
    public float fireRange = 15f;
    public float fireCooldown = 1.5f;
    public LayerMask obstacleMask; // Assign "Default" or whatever blocks vision

    private float fireTimer;
    private Transform player1;
    private TankShooter shooter;

    void Start()
    {
        player1 = GameObject.FindGameObjectWithTag("Player")?.transform;
        shooter = GetComponent<TankShooter>();
    }

    void Update()
    {
        if (player1 == null || shooter == null) return;

        fireTimer -= Time.deltaTime;

        FollowPlayer();
        TryShootAtEnemy();
    }

    void FollowPlayer()
    {
        Vector3 targetPos = player1.position - player1.forward * followDistance;
        float distanceToTarget = Vector3.Distance(transform.position, targetPos);

        if (distanceToTarget > 1f)
        {
            Vector3 direction = (targetPos - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        }
    }

    void TryShootAtEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        Transform closestTarget = null;
        float minDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            if (enemy == null || !enemy.activeInHierarchy) continue;

            float dist = Vector3.Distance(transform.position, enemy.transform.position);
            if (dist <= fireRange && dist < minDistance)
            {
                // Check for line of sight
                Vector3 dirToEnemy = (enemy.transform.position - transform.position).normalized;
                Ray ray = new Ray(transform.position + Vector3.up * 1.0f, dirToEnemy);
                if (Physics.Raycast(ray, out RaycastHit hit, fireRange, ~obstacleMask))
                {
                    if (hit.collider.gameObject == enemy)
                    {
                        closestTarget = enemy.transform;
                        minDistance = dist;
                    }
                }
            }
        }

        if (closestTarget != null && fireTimer <= 0f)
        {
            // Rotate to aim at enemy
            Vector3 dir = (closestTarget.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(dir.x, 0, dir.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10f);

            // Fire
            shooter.Fire();
            fireTimer = fireCooldown;
        }
    }
}
