using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.UI;
public class PlayerController : MonoBehaviour
{
    public float speed = 10f; 
    [Header("Missile")]
    public GameObject missile;
    public Transform missileSpawnPositon;
    public float destroyTime = 5f ;
    public Transform muzzleSpawnPosition;
    public bool isShieldActive = false;
    public float shieldDuration = 5f;
    public GameObject shieldSprite;
    public bool isMultiShotActive = false;
    public float multiShotDuration = 5f;


    public int lives = 3;
    public Text livesText;

    private void Start()
    {
        UpdateLivesUI(); // Update UI at the start
    }
    private void Update()
    {

      PlayerMovement();
      PlayerShoot();
        
    }

    void PlayerMovement(){
        float xpos = Input.GetAxis("Horizontal");
        float ypos = Input.GetAxis("Vertical");
        
        UnityEngine.Vector3 movement = new UnityEngine.Vector3(xpos, ypos, 0) * speed * Time.deltaTime;
    transform.Translate(movement);
    }

    void PlayerShoot(){
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if (isMultiShotActive)
            {
                // Fire three missiles in a spread pattern
                SpawnMissile(missileSpawnPositon.position + new UnityEngine.Vector3(-0.5f, 0, 0));
                SpawnMissile(missileSpawnPositon.position);  // Middle missile
                SpawnMissile(missileSpawnPositon.position + new UnityEngine.Vector3(0.5f, 0, 0));
            }
            else
            {
                // Fire only one missile
                SpawnMissile(missileSpawnPositon.position);
            }
            SpawnMuzzleFlash();
        }
    }

    public void ActivateShield()
    {
        isShieldActive = true;
        shieldSprite.SetActive(true);  // Show the shield sprite
        StartCoroutine(DeactivateShieldAfterTime());
    }

    private IEnumerator DeactivateShieldAfterTime()
    {
        yield return new WaitForSeconds(shieldDuration);
        isShieldActive = false;
        shieldSprite.SetActive(false);  // Hide the shield sprite
    }
    void SpawnMissile(UnityEngine.Vector3 position)
    {
        GameObject gm = Instantiate(missile, position, UnityEngine.Quaternion.identity);
        gm.transform.SetParent(null);
        Destroy(gm, destroyTime);
    }

    public void ActivateMultiShot()
    {
        isMultiShotActive = true;
        StartCoroutine(DeactivateMultiShotAfterTime());
    }

    private IEnumerator DeactivateMultiShotAfterTime()
    {
        yield return new WaitForSeconds(multiShotDuration);
        isMultiShotActive = false;
    }
    void SpawnMuzzleFlash()
    {
            
            GameObject muzzle = Instantiate(GameManager.instance.muzzleFlash, muzzleSpawnPosition);
            muzzle.transform.SetParent(null);
            Destroy(muzzle, destroyTime);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (!isShieldActive)
            {
                // Instantiate explosion effect
                GameObject explosion = Instantiate(GameManager.instance.explosion, transform.position, Quaternion.identity);
                Destroy(explosion, 2f); // Destroy explosion after 2 seconds

                lives--; // Decrease lives
                UpdateLivesUI(); // Update UI when lives decrease

                if (lives <= 0)
                {
                    // Player is out of lives
                    GameObject gm = Instantiate(GameManager.instance.explosion, transform.position, transform.rotation);
                    Destroy(gm, 2f);
                    Destroy(gameObject);  // Destroy the player
                    GameManager.instance.GameOver();  // Trigger Game Over
                }
                else
                {
                    // Only destroy the enemy when the player still has lives
                    Destroy(collision.gameObject);
                }
            }
            else
            {
                // Player is shielded, destroy the enemy but not the player
                Destroy(collision.gameObject);
            }
        }
    }

    void UpdateLivesUI()
    {
        livesText.text = "Lives: " + lives;
    }
}
