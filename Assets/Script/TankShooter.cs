using UnityEngine;

public class TankShooter : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireForce = 700f;
    public KeyCode fireKey = KeyCode.Space;

    public bool isInputLocked = false;

    private TankSoundManager soundManager;

    void Start()
    {
        soundManager = GetComponent<TankSoundManager>();
    }

    void Update()
    {
        if (isInputLocked)
            return;

        if (Input.GetKeyDown(fireKey))
        {
            Fire();
        }
    }

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

            if (soundManager != null)
            {
                soundManager.PlayShootSound();
            }
        }
    }
}
