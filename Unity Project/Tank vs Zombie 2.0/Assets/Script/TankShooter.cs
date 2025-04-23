using UnityEngine;

public class TankShooter : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireForce = 700f;
    public KeyCode fireKey = KeyCode.Space;

    void Update()
    {
        if (Input.GetKeyDown(fireKey))
        {
            Fire(); // Now using the new public method
        }
    }

    // This allows ServerAIController to shoot programmatically
    public void Fire()
    {
        if (bulletPrefab != null && firePoint != null)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddForce(firePoint.forward * fireForce);
            }
        }
    }
}
