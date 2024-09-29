using System.Collections;
using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    public float rotationSpeed = 45f;   // Speed at which the enemy rotates
    public GameObject projectilePrefab; // Reference to the projectile prefab
    public Transform firePoint1;         // Where the projectile will be shot from
    public Transform firePoint2;         // Where the projectile will be shot from
    public Transform firePoint3;         // Where the projectile will be shot from
    public float fireRate = 0.2f;       // Time between shots
    public int health = 10;             // Enemy health
    public float projectileSpeed = 200f;  // Speed of the projectile

    private void Start()
    {
        // Start shooting projectiles periodically
        StartCoroutine(ShootProjectiles()); 
    }

    private void Update()
    {
        // Rotate the enemy around its own axis
        RotateEnemy();
    }

    // Method to rotate the enemy
    void RotateEnemy()
    {
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime, Space.World);
    }

    // Coroutine to shoot projectiles at a set interval
    IEnumerator ShootProjectiles()
    {
        Debug.Log("Strzelaj w przeciwnka");
        while (true)
        {
            // Instantiate and shoot a projectile
            ShootProjectile();

            // Wait for the next shot based on fireRate
            yield return new WaitForSeconds(fireRate);
        }
    }

    // Method to shoot a projectile
    void ShootProjectile()
    {
        if (projectilePrefab != null)
        {
            // Instantiate the projectile
            GameObject projectile1 = Instantiate(projectilePrefab, firePoint1.position, firePoint1.rotation);
            GameObject projectile2 = Instantiate(projectilePrefab, firePoint2.position, firePoint2.rotation);
            GameObject projectile3 = Instantiate(projectilePrefab, firePoint3.position, firePoint3.rotation);
            // Apply force to the projectile to shoot it in a forward direction
            Rigidbody rb1 = projectile1.GetComponent<Rigidbody>();
            Rigidbody rb2 = projectile2.GetComponent<Rigidbody>();
            Rigidbody rb3 = projectile3.GetComponent<Rigidbody>();

            if (rb1 != null || rb2 != null)
            {
                rb1.velocity = firePoint1.forward * projectileSpeed;
                rb2.velocity = firePoint2.forward * projectileSpeed;
                rb3.velocity = firePoint3.forward * projectileSpeed;

            }
        }
    }

    // Method to handle damage when enemy is hit
    public void TakeDamage(int damage)
    {
        health -= damage;

        // If health drops to zero, destroy the enemy
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    // Example of detecting collision with player projectiles
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerProjectile"))
        {
            // Assuming projectiles have a damage value
            int damage = other.GetComponent<Projectile>().damage;
            TakeDamage(damage);

            // Destroy the projectile after it hits the enemy
            Destroy(other.gameObject);
        }
    }
}
