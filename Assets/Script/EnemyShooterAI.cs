using UnityEngine;

public class EnemyShooterAI : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 3f;
    public float rotationSpeed = 5f;

    [Header("Target Detection")]
    public float detectionRange = 0f; // 0 means no range limit

    [Header("Shooting Settings")]
    public bool canShoot = false;
    public float fireRate = 2f;
    public GameObject bulletPrefab;
    public Transform firePoint;

    private Transform target;
    private float fireCooldown = 0f;

    void Update()
    {
        target = FindClosestPlayer();

        if (target != null)
        {
            float distance = Vector3.Distance(transform.position, target.position);
            if (detectionRange > 0f && distance > detectionRange)
                return;

            // Rotate toward target
            Vector3 direction = (target.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);

            // Move forward
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);

            // Shoot if enabled
            if (canShoot)
            {
                fireCooldown -= Time.deltaTime;
                if (fireCooldown <= 0f)
                {
                    Fire();
                    fireCooldown = fireRate;
                }
            }
        }
    }

    Transform FindClosestPlayer()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        Transform closest = null;
        float minDist = Mathf.Infinity;

        foreach (GameObject player in players)
        {
            if (!player.activeInHierarchy) continue;

            float dist = Vector3.Distance(transform.position, player.transform.position);
            if (dist < minDist)
            {
                minDist = dist;
                closest = player.transform;
            }
        }

        return closest;
    }

    void Fire()
    {
        if (bulletPrefab == null || firePoint == null) return;

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = firePoint.forward * 10f; // ? Fixed bug: should use velocity
        }
    }
}
