using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyShipController : MonoBehaviour
{
    public float shipSpeed = 3f;  // Speed of the enemy ship
    public GameObject missile;
    public float missileSpeed = 8f;  // Speed of the missile (faster than the ship)
    public float missileDestroyTime = 3f;  // Time after which the missile is destroyed
    public Transform missileSpawnPosition;  // Where the missile is spawned

    public float missileFireInterval = 2f;  // Time interval for missile firing

    private void Start()
    {
        // Start firing missiles at the interval
        StartCoroutine(FireMissiles());
    }

    void Update()
    {
        // Enemy ship moves downwards (or adjust the movement as needed)
        transform.Translate(Vector3.up * shipSpeed * Time.deltaTime);
        
        // Optional: Destroy the enemy if it goes off-screen
        if (transform.position.y < -6f)  // Adjust boundary based on your game's screen size
        {
            Destroy(gameObject);  // Destroy enemy if it leaves the screen
        }
    }

    IEnumerator FireMissiles()
    {
        while (true)
        {
            yield return new WaitForSeconds(missileFireInterval);  // Wait for the interval before firing the next missile
            SpawnMissile();
        }
    }

    void SpawnMissile()
    {
        // Instantiate the missile at the missile spawn position
        GameObject gm = Instantiate(missile, missileSpawnPosition.position, Quaternion.identity);
        
        // Set the missile to move straight down faster than the ship
        Rigidbody2D missileRb = gm.GetComponent<Rigidbody2D>();
        if (missileRb != null)
        {
            missileRb.velocity = Vector2.down * missileSpeed;  // Apply faster downward speed to the missile
        }

        // Ensure the missile is destroyed after a certain time
        Destroy(gm, missileDestroyTime);
    }

    // Detect collisions with player projectiles and destroy the ship
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerProjectile"))
        {
            // Destroy the enemy ship when hit by a player projectile
            Destroy(gameObject);

            
            // Destroy the player projectile
            Destroy(collision.gameObject);
        }
    }
}
