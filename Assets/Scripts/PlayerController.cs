using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

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
     private void OnCollisionEnter2D( Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            if (!isShieldActive)
            {
                GameObject gm = Instantiate(GameManager.instance.explosion, transform.position, transform.rotation);
                Destroy(gm, 2f);
                Destroy(this.gameObject);
            }
            else{
                Destroy(collision.gameObject);
            }
            // Game over screen     
        }
    }
}
