using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMissile : MonoBehaviour
{
    public int damage = 1;  // How much damage the missile does (1 life)
    public float missileSpeed = 5f;  // Speed at which the missile falls
    public float missileDestroyY = -10f;  // Y position at which missile is destroyed (off-screen)

    private void Update()
    {
        // Move the missile down every frame
        transform.Translate(Vector3.down * missileSpeed * Time.deltaTime);

        // If the missile goes below a certain point (off-screen), destroy it
        if (transform.position.y < missileDestroyY)
        {
            Destroy(gameObject);
        }
    }

    // Handle collision with the player using OnCollisionEnter2D
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))  // Check if missile hits the player
        {
            Debug.Log("Missile hit the player!");  // Debug log for collision detection

            // Access the PlayerController and reduce the player's lives
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            if (player != null)
            {
                Debug.Log("PlayerController found, reducing lives.");
                player.TakeDamage(damage);  // Reduce player lives by the missile's damage value (1 life)
            }

             // Destroy the missile after it hits the player
            Debug.Log("Missile destroyed after hitting player.");
            Destroy(gameObject);  // Destroy the missile
        }
        else
        {
            // Optionally, destroy the missile if it hits something else (like walls or obstacles)
            Debug.Log("Missile collided with something else: " + collision.gameObject.name);
            Destroy(gameObject);
        }
    }
}
