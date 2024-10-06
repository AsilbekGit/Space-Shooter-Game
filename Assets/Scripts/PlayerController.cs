using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed = 10f;  // Player movement speed
    [Header("Missile")]
    public GameObject missile;  // Missile prefab for player to shoot
    public Transform missileSpawnPosition;  // Where the missile spawns from
    public float destroyTime = 5f;  // Time after which missile is destroyed
    public Transform muzzleSpawnPosition;  // Position for the muzzle flash effect
    public bool isShieldActive = false;  // Whether the shield power-up is active
    public float shieldDuration = 5f;  // Duration of shield power-up
    public GameObject shieldSprite;  // Sprite representing the shield
    public bool isMultiShotActive = false;  // Whether the multi-shot power-up is active
    public float multiShotDuration = 5f;  // Duration of multi-shot power-up

    public int lives = 3;  // Player's starting lives
    public Text livesText;  // UI text for displaying lives

    private AudioManager audioManager;

    private void Awake()
    {
        // Initialize AudioManager by finding it in the scene
        GameObject audioManagerObject = GameObject.FindGameObjectWithTag("Audio");
        if (audioManagerObject != null)
        {
            audioManager = audioManagerObject.GetComponent<AudioManager>();
        }
        else
        {
            Debug.LogError("No GameObject found with the 'Audio' tag");
        }
    }

    private void Start()
    {
        UpdateLivesUI(); // Update the UI to show lives at the start
    }

    private void Update()
    {
        PlayerMovement();  // Handle player movement
        PlayerShoot();  // Handle player shooting
    }

    // Handle player movement based on input
    void PlayerMovement()
    {
        float xpos = Input.GetAxis("Horizontal");
        float ypos = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(xpos, ypos, 0) * speed * Time.deltaTime;
        transform.Translate(movement);
    }

    // Handle player shooting
    void PlayerShoot()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            audioManager.PlaySFX(audioManager.shoot);  // Play shooting sound effect
            if (isMultiShotActive)
            {
                // Fire three missiles in a spread pattern if multi-shot is active
                SpawnMissile(missileSpawnPosition.position + new Vector3(-0.5f, 0, 0));  // Left missile
                SpawnMissile(missileSpawnPosition.position);  // Middle missile
                SpawnMissile(missileSpawnPosition.position + new Vector3(0.5f, 0, 0));  // Right missile
            }
            else
            {
                // Fire one missile
                SpawnMissile(missileSpawnPosition.position);
            }
            SpawnMuzzleFlash();  // Spawn muzzle flash effect
        }
    }

    // Spawns a missile from a given position
    void SpawnMissile(Vector3 position)
    {
        GameObject gm = Instantiate(missile, position, Quaternion.identity);
        gm.transform.SetParent(null);  // Ensure the missile is independent of the player
        Destroy(gm, destroyTime);  // Destroy missile after a certain time
    }

    // Spawns muzzle flash effect
    void SpawnMuzzleFlash()
    {
        GameObject muzzle = Instantiate(GameManager.instance.muzzleFlash, muzzleSpawnPosition);
        muzzle.transform.SetParent(null);  // Ensure muzzle flash is independent of the player
        Destroy(muzzle, destroyTime);  // Destroy muzzle flash after a certain time
    }

    // Activates shield power-up
    public void ActivateShield()
    {
        isShieldActive = true;
        shieldSprite.SetActive(true);  // Show shield sprite
        StartCoroutine(DeactivateShieldAfterTime());
    }

    // Deactivates shield after a set duration
    private IEnumerator DeactivateShieldAfterTime()
    {
        yield return new WaitForSeconds(shieldDuration);
        isShieldActive = false;
        shieldSprite.SetActive(false);  // Hide shield sprite
    }

    // Activates multi-shot power-up
    public void ActivateMultiShot()
    {
        isMultiShotActive = true;
        StartCoroutine(DeactivateMultiShotAfterTime());
    }

    // Deactivates multi-shot after a set duration
    private IEnumerator DeactivateMultiShotAfterTime()
    {
        yield return new WaitForSeconds(multiShotDuration);
        isMultiShotActive = false;
    }

    // Handles collision detection with enemies and other objects
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (!isShieldActive)
            {
                // Instantiate explosion effect
                GameObject explosion = Instantiate(GameManager.instance.explosion, transform.position, Quaternion.identity);
                Destroy(explosion, 2f);  // Destroy explosion after 2 seconds

                lives--;  // Decrease lives
                UpdateLivesUI();  // Update the UI when lives decrease

                if (lives <= 0)
                {
                    // Player is out of lives
                    Die();
                }
                else
                {
                    Destroy(collision.gameObject);  // Destroy the enemy on collision
                }
            }
            else
            {
                // Player is shielded, only destroy the enemy
                Destroy(collision.gameObject);
            }
        }
    }

    // Method to handle damage from enemy missiles or other damage sources
    public void TakeDamage(int damage)
    {
        if (!isShieldActive)  // Check if the player is not shielded
        {
            lives -= damage;  // Reduce the player's lives
            UpdateLivesUI();  // Update the UI to reflect the change

            if (lives <= 0)
            {
                // Player is out of lives
                Die();
            }
        }
    }

    // Handles player death and triggers game over
    void Die()
    {
        // Instantiate explosion effect
        GameObject explosion = Instantiate(GameManager.instance.explosion, transform.position, Quaternion.identity);
        Destroy(explosion, 2f);  // Destroy the explosion after 2 seconds

        // Destroy the player object
        Destroy(gameObject);

        // Call Game Over in the GameManager
        GameManager.instance.GameOver();
    }

    // Updates the UI text showing the number of lives
    void UpdateLivesUI()
    {
        livesText.text = "Lives: " + lives;
    }
}
