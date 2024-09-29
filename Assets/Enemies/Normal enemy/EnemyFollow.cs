using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public Transform player;   // Reference to the player's position
    public float speed = 2f;   // Speed of the enemy
    public int health = 3;     // Enemy health

    private void Update()
    {
        // Move towards the player slowly
        MoveTowardsPlayer();
    }

    // Method to make the enemy move towards the player
    void MoveTowardsPlayer()
    {
        if (player == null)
            return;

        // Get the direction to the player
        Vector3 direction = (player.position - transform.position).normalized;

        // Move the enemy slowly towards the player
        transform.position += direction * speed * Time.deltaTime;
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
