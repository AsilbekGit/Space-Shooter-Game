using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  // Import SceneManager

public class GameManager : MonoBehaviour
{   
    public static GameManager instance;
    public GameObject EnemyShips;
    public GameObject Enemy;

    public float minValueInstantiate;
    public float maxValueInstantiate;
    public float enemyDestroyTime = 10f;
    public GameObject shieldPowerUpPrefab;
    public GameObject multiShotPowerUpPrefab;
    public float powerUpSpawnInterval = 10f;

    [Header("Particle Effects")]
    public GameObject explosion;
    public GameObject muzzleFlash;

    [Header("Panels")]
    public GameObject startMenu;
    public GameObject pauseMenu;

    public GameObject gameOverPanel;

    private bool spawnEnemyShipsNext = true; // Boolean flag to alternate between Enemy and EnemyShips

    private void Awake()
    {
        instance = this; 
    }

    private void Start()
    {
        startMenu.SetActive(true);
        pauseMenu.SetActive(false);
        gameOverPanel.SetActive(false);
        Time.timeScale = 0f;
        InvokeRepeating("InstantiateEnemy", 1f, 1f); // Spawns enemies every second
        InvokeRepeating("SpawnPowerUps", powerUpSpawnInterval, powerUpSpawnInterval); // Spawns power-ups at interval
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame(true);
        }
    }

    void InstantiateEnemy()
    {
        Vector3 enemyPos = new Vector3(UnityEngine.Random.Range(minValueInstantiate, maxValueInstantiate), 5f);
        GameObject enemyInstance;

        // Alternate between spawning EnemyShips and Enemy
        if (spawnEnemyShipsNext)
        {
            // Spawn EnemyShips
            enemyInstance = Instantiate(EnemyShips, enemyPos, Quaternion.Euler(0f, 0f, 180f));
            Debug.Log("Spawning EnemyShips");
        }
        else
        {
            // Spawn Enemy
            enemyInstance = Instantiate(Enemy, enemyPos, Quaternion.Euler(0f, 0f, 180f));
            Debug.Log("Spawning Enemy");
        }

        // Alternate the flag for the next spawn
        spawnEnemyShipsNext = !spawnEnemyShipsNext;

        // Set speed for EnemyShip
        EnemyShipController enemyController = enemyInstance.GetComponent<EnemyShipController>();
        if (enemyController != null)
        {
            enemyController.shipSpeed = UnityEngine.Random.Range(1f, 2f); // Set a random speed
        }

        // Destroy enemy after a certain time
        Destroy(enemyInstance, enemyDestroyTime);
    }

    void SpawnPowerUps()
    {
        Vector3 powerUpPosition = new Vector3(UnityEngine.Random.Range(minValueInstantiate, maxValueInstantiate), 5f);
        int randomPowerUp = UnityEngine.Random.Range(0, 2);

        if (randomPowerUp == 0)
        {
            Instantiate(shieldPowerUpPrefab, powerUpPosition, Quaternion.Euler(0f, 0f, 180f));
        }
        else
        {
            Instantiate(multiShotPowerUpPrefab, powerUpPosition, Quaternion.Euler(0f, 0f, 180f));
        }
    }

    public void StartButton()
    {
        startMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void PauseGame(bool isPaused)
    {
        if (isPaused)
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void GameOver()
    {
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f;
        Debug.Log("Game Over!");
    }

    // Add RestartGame method
    public void RestartGame()
    {
        // Reload the current active scene to restart the game
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        gameOverPanel.SetActive(false);
    }
}
