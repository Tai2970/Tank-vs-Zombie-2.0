using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireRate = 2f;
    public float bulletSpeed = 15f;
    public float detectionRange = 20f;

    private float fireTimer;

    void Update()
    {
        GameObject target = FindClosestPlayer();
        if (target != null && Vector3.Distance(transform.position, target.transform.position) <= detectionRange)
        {
            fireTimer += Time.deltaTime;
            if (fireTimer >= fireRate)
            {
                ShootAt(target.transform);
                fireTimer = 0f;
            }
        }
    }

    void ShootAt(Transform target)
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            Vector3 direction = (target.position - firePoint.position).normalized;
            rb.linearVelocity = direction * bulletSpeed;
        }
    }

    GameObject FindClosestPlayer()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        GameObject[] aiPlayers = GameObject.FindGameObjectsWithTag("AI");
        GameObject[] all = new GameObject[players.Length + aiPlayers.Length];
        players.CopyTo(all, 0);
        aiPlayers.CopyTo(all, players.Length);

        GameObject closest = null;
        float minDist = Mathf.Infinity;

        foreach (GameObject p in all)
        {
            float dist = Vector3.Distance(transform.position, p.transform.position);
            if (dist < minDist)
            {
                closest = p;
                minDist = dist;
            }
        }

        return closest;
    }
}
