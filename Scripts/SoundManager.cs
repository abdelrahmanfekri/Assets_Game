using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip collectOrb;
    public AudioClip switchForm;
    public AudioClip usePower;
    public AudioClip hitObstacle;
    public AudioClip invalidAction;
    public AudioClip gameOver;
    public AudioClip gameSoundtrack;
    public AudioClip menuSoundtrack;

    AudioSource audioSource;


    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void playCollectOrb()
    {
        audioSource.PlayOneShot(collectOrb);
    }
    public void playSwitchForm()
    {
        audioSource.PlayOneShot(switchForm);
    }
    public void playUsePower()
    {
        audioSource.PlayOneShot(usePower);
    }
    public void playHitObstacle()
    {
        audioSource.PlayOneShot(hitObstacle);
    }
    public void playInvalidAction()
    {
        audioSource.PlayOneShot(invalidAction);
    }

    public void playGameOver()
    {
        audioSource.PlayOneShot(gameOver);
    }
    public void playGameSoundtrack()
    {
        audioSource.PlayOneShot(gameSoundtrack);
    }
    public void playMenuSoundtrack()
    {
        audioSource.clip = menuSoundtrack;
        audioSource.loop = true;
        audioSource.Play();
    }
    public void stopMenuSoundtrack()
    {
        audioSource.Stop();
    }
    public void playBackSoundtrack()
    {
        audioSource.clip = gameSoundtrack;
        audioSource.loop = true;
        audioSource.Play();
    }
    public void stopBackSoundtrack()
    {
        audioSource.Stop();
    }


    public void toggleMute()
    {
        AudioListener.volume = 1 - AudioListener.volume;
    }
}
