using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileController : MonoBehaviour
{
    public float missileSpeed = 25f; 

    // Update is called once per frame
    private AudioManager audioManager;

    private void Start() {
        // Assuming GameManager has a public static reference to AudioManager instance
        audioManager = FindObjectOfType<AudioManager>();
    }
    void Update()
    {
        transform.Translate(Vector3.up * missileSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter2D( Collision2D collision)
    {
      
        if (collision.gameObject.tag == "Enemy")
        {
            GameObject gm = Instantiate(GameManager.instance.explosion, transform.position, transform.rotation);

            Destroy(gm, 2f);
            Destroy(this.gameObject);
            Destroy(collision.gameObject);
             if (audioManager != null)
            {
                audioManager.PlaySFX(audioManager.exposion);
            }
            else
            {
                Debug.LogWarning("AudioManager not found");
            }
            
        }
    }

}
