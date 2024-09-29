using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemies : MonoBehaviour
{
    public float shootingRange = 50f; // Maximum range for detecting and shooting enemies
    public LayerMask enemyLayer; // Layer mask for enemies
    public Transform firePoint; // The point from where the projectile is shot
    public GameObject projectilePrefab; // The projectile to shoot
    public float projectileSpeed = 20f; // Speed of the projectile
    public float fireRate = 1f; // Time in seconds between each shot

    private float lastShotTime = 0f; // Time when the last shot was fired

    private void Update()
    {
        // Try to shoot if enough time has passed since the last shot
        if (Time.time >= lastShotTime + fireRate)
        {
            // Find and shoot the closest enemy
            ShootAtClosestEnemy();
        }
    }

    void ShootAtClosestEnemy()
    {
        // Find all enemies within range
        Collider[] enemiesInRange = Physics.OverlapSphere(transform.position, shootingRange, enemyLayer);

        // If no enemies found, return early
        if (enemiesInRange.Length == 0)
        {
            return;
        }

        // Initialize variables for tracking the closest enemy
        Transform closestEnemy = null;
        float closestDistance = Mathf.Infinity;

        // Loop through all detected enemies
        foreach (Collider enemyCollider in enemiesInRange)
        {
            Transform enemyTransform = enemyCollider.transform;

            // Calculate the distance to the enemy
            float distanceToEnemy = Vector3.Distance(transform.position, enemyTransform.position);

            // Check for the closest enemy with a clear line of sight
            if (distanceToEnemy < closestDistance && HasClearLineOfSight(enemyTransform))
            {
                closestEnemy = enemyTransform;
                closestDistance = distanceToEnemy;
            }
        }

        // If a closest enemy is found with a clear line of sight, shoot
        if (closestEnemy != null)
        {
            ShootProjectile(closestEnemy);
        }
    }

    bool HasClearLineOfSight(Transform target)
    {
        // Perform a raycast to check for obstacles between the shooter and the enemy
        RaycastHit hit;
        Vector3 directionToEnemy = (target.position - firePoint.position).normalized;
        if (Physics.Raycast(firePoint.position, directionToEnemy, out hit, shootingRange))
        {
            // Check if the raycast hit the enemy (check the layer or tag)
            if (hit.collider.CompareTag("Enemy"))
            {
                return true; // Clear line of sight to the enemy
            }
        }
        return false; // Something is obstructing the shot
    }

    void ShootProjectile(Transform target)
    {
        if (projectilePrefab != null && firePoint != null)
        {
            // Instantiate a new projectile at the firePoint
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);

            // Calculate the direction to the target
            Vector3 direction = (target.position - firePoint.position).normalized;

            // Set the projectile rotation towards the enemy
            projectile.transform.LookAt(target.position); // This aligns the projectile towards the target

            // Set the velocity of the projectile towards the target
            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = direction * projectileSpeed;
            }

            // Update the last shot time
            lastShotTime = Time.time;
        }
    }
}
