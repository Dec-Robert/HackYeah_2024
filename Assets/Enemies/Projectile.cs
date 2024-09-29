using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage = 1; // The damage value of the projectile
    public float lifeTime = 5f; // Time before the projectile is destroyed
    public GameObject hitEffect; // Reference to the hit effect prefab
    
    private void Start()
    {
        // Destroy the projectile after a set time
        Destroy(gameObject, lifeTime);
    }

    // Detect collision with any object
    private void OnTriggerEnter(Collider other)
    {
        // If the projectile hits an enemy, apply damage
        if (other.CompareTag("Enemy"))
        {
            // Apply damage to the enemy
            other.GetComponent<EnemyFollow>()?.TakeDamage(damage);
        }

        // Instantiate hit effect if applicable
        if (hitEffect != null)
        {
            Instantiate(hitEffect, transform.position, transform.rotation);
        }

        // Destroy the projectile upon contact with any object, except enemies
        if (!other.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }
}
