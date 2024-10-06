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

    [Header("Particle Effects")]
    public GameObject explosion;
    public GameObject muzzleFlash;

    public GameObject gameOverPanel;


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

    public void GameOver()
    {
        // Implement game over logic here
        gameOverPanel.SetActive(true); // Show game over panel
        Time.timeScale = 0f; // Stop the game
        Debug.Log("Game Over!");
    }
   
}
