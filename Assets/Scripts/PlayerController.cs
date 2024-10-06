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
     private void OnCollisionEnter2D( Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            GameObject gm = Instantiate(GameManager.instance.explosion, transform.position, transform.rotation);
            Destroy(gm, 2f);
            Destroy(this.gameObject);
            // Game over screen     
        }
    }
}
