using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource musicSource;
     [SerializeField] AudioSource SFXSource;

     public AudioClip background;
     public AudioClip exposion;
     public AudioClip shoot;
     public AudioClip gameover;
    
     private void Start()
     {
        musicSource.clip = background;
        musicSource.Play();
     }
    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
     public void StopBackgroundMusic()
    {
        musicSource.Stop();
    }

    public void PlayGameOverSound()
    {
        PlaySFX(gameover); // Uses the SFX source to play the game over sound
        StopBackgroundMusic(); // Stops the background music
    }
    

}
