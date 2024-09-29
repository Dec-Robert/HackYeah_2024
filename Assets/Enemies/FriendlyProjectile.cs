using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendlyProjectile : MonoBehaviour
{
    public float projectileSpeed = 20f;  // Speed of the projectile
    public int damage = 1;               // Damage the projectile deals
    public float lifetime = 5f;          // Lifetime of the projectile (in seconds)
    public LayerMask enemyLayer;         // Layer for enemies
    public GameObject hitEffect;         // Optional: Effect that will play on hit

    private void Start()
    {
        // Destroy the projectile after its lifetime has passed
        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        // Move the projectile forward at the specified speed
        transform.Translate(Vector3.forward * projectileSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object hit is on the "Enemy" layer or is not the player
        if ((enemyLayer.value & 1 << other.gameObject.layer) != 0)
        {

            // Instantiate hit effect (optional)
            if (hitEffect != null)
            {
                Instantiate(hitEffect, transform.position, transform.rotation);
            }

            // Destroy the projectile after hitting an enemy
            Destroy(gameObject);
        }
        else if (!other.CompareTag("Player"))
        {
            // If the object hit is not the player, destroy the projectile
            if (hitEffect != null)
            {
                Instantiate(hitEffect, transform.position, transform.rotation);
            }

            // Destroy the projectile
            Destroy(gameObject);
        }
    }
}
