using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpController : MonoBehaviour
{
    public enum PowerUpType { Shield, MultiShot }
    public PowerUpType powerUpType;

    public float speed = 3f;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();

            if (powerUpType == PowerUpType.Shield)
            {
                player.ActivateShield();
            }
            else if (powerUpType == PowerUpType.MultiShot)
            {
                player.ActivateMultiShot();
            }

            // Destroy the power-up after it is collected
            Destroy(gameObject);
        }
    }

    void Update(){
        // Move the power-up downward
        transform.Translate(Vector3.down * speed * Time.deltaTime);

        // Destroy the power-up if it goes off-screen
        if (transform.position.y < -6f)
        {
            Destroy(gameObject);
        }
    }
}
