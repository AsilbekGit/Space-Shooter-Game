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

    

    public int lives = 3;
    public Text livesText;


   AudioManager audioManager;

private void Awake()
{
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
        
        UnityEngine.Vector3 movement = new UnityEngine.Vector3(xpos, ypos,0)* speed * Time.deltaTime;
        transform.Translate(movement);
    }

    void PlayerShoot(){
        if(Input.GetKeyDown(KeyCode.Space))
        {
            audioManager.PlaySFX(audioManager.shoot);
            SpawnMissile();
            SpawnMuzzleFlash();
           
        }

    }
    void SpawnMissile()
    {
            GameObject gm = Instantiate(missile,missileSpawnPositon);
            GameObject muzzle = Instantiate(GameManager.instance.muzzleFlash, muzzleSpawnPosition);
            gm.transform.SetParent(null);
            Destroy(gm, destroyTime);
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
                Destroy(gameObject);
                audioManager.PlayGameOverSound();
                GameManager.instance.GameOver(); // Trigger game over logic
            }
            else
            {
                // Optionally, handle what happens on losing a life
                Debug.Log("Lives left: " + lives);
            }

            // Destroy the enemy
            Destroy(collision.gameObject);
        }
    }

    void UpdateLivesUI()
    {
        livesText.text = "Lives: " + lives;
    }
}
