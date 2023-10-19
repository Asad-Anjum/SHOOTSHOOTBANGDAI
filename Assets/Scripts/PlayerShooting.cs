using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public Transform firePoint;
    public GameObject projectilePrefab;
    public float fireRate;
    private float nextFireTime;
    public float bulletSpeed = 80f;

    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + 1 / fireRate;
        }
    }

    void Shoot()
    {
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();

        // Apply force to move the projectile
        rb.AddForce(firePoint.up * bulletSpeed, ForceMode2D.Impulse);

        // You can add additional logic, like setting the damage value, collision handling, and visual effects.

        Destroy(projectile, 2.0f); // Destroy the projectile after some time or when it hits something.
    }
}
