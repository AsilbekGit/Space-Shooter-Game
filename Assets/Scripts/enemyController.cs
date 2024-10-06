using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyController : MonoBehaviour
{
    public float speed = 5f;  // Speed of the enemy

    void Update()
    {
        // Move the enemy upwards (adjust the movement as per your game)
        transform.Translate(Vector3.up * speed * Time.deltaTime);

        // Optional: Destroy the enemy if it moves off-screen (adjust based on your game's boundaries)
        if (transform.position.y > 6f)  // Assuming 6f is the upper screen limit
        {
            Destroy(gameObject);  // Destroy the enemy when it goes off-screen
        }
    }

    // Detect collision with player's projectiles or other objects
    private void OnCollisionEnter2D(Collision2D collision)
{
    if (collision.gameObject.CompareTag("PlayerProjectile"))
    {
        // Destroy the enemy
        Destroy(gameObject);

        // Destroy the player's projectile
        Destroy(collision.gameObject);
    }
}
}
