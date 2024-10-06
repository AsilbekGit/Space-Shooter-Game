using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{   
    public static GameManager instance;
    public GameObject enemyPrefab;
    public float  minValueInstantiate;
    public float  maxValueInstantiate;
    public float  enemyDestroyTime = 10f;
    public GameObject shieldPowerUpPrefab;
    public GameObject multiShotPowerUpPrefab;
    public float powerUpSpawnInterval = 10f;

    [Header("Particle Effects")]
    public GameObject explosion;
    public GameObject muzzleFlash;


    [Header("Panels")]
    public GameObject startMenu;
    public GameObject pauseMenu;


    private void Awake()
    {
        instance = this; 
    }


    private void Start()
    {
        startMenu.SetActive(true);
        pauseMenu.SetActive(false);
        Time.timeScale = 0f;
        InvokeRepeating("InstantiateEnemy", 1f, 1f);
        InvokeRepeating("SpawnPowerUps", powerUpSpawnInterval, powerUpSpawnInterval);
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame(true);
        }
    }

    void InstantiateEnemy()
    {
        Vector3 enemypos = new Vector3(UnityEngine.Random.Range(minValueInstantiate, maxValueInstantiate),5f);
        GameObject enemy =  Instantiate(enemyPrefab,enemypos,Quaternion.Euler(0f,0f,180f));
        Destroy(enemy,enemyDestroyTime);
    }

    void SpawnPowerUps()
    {
        Vector3 powerUpPosition = new Vector3(UnityEngine.Random.Range(-8f, 8f), 5f, 0f);
        int randomPowerUp = UnityEngine.Random.Range(0, 2);

        if (randomPowerUp == 0)
        {
            Instantiate(shieldPowerUpPrefab, powerUpPosition, Quaternion.identity);
        }
        else
        {
            Instantiate(multiShotPowerUpPrefab, powerUpPosition, Quaternion.identity);
        }
    }
    
    public void StartButton()
    {
        startMenu.SetActive(false);
        Time.timeScale = 1f;
    }
    public void PauseGame(bool isPaused)
    {
        if( isPaused == true)
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;
        }else{
            pauseMenu.SetActive(false);
            Time.timeScale = 1f;
        }
        }


    public void QuitGame()
    {
        Application.Quit();
    }
   
}
