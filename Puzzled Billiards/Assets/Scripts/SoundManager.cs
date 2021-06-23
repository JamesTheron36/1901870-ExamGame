using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{

    AudioSource audioSrc;
    [SerializeField] AudioClip starSound;
    [SerializeField] AudioClip collisionSound;
    [SerializeField] AudioClip hitSound;
    [SerializeField] AudioClip winSound;
    [SerializeField] AudioClip pocketSound;
    [SerializeField] AudioClip switchSound;
    [SerializeField] AudioClip stickSound;
    // Start is called before the first frame update
    void Start()
    {
        audioSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayStarSound()
    {
        audioSrc.clip = starSound;
        audioSrc.Play();
    }
    public void PlayColideSound()
    {
        audioSrc.clip = collisionSound;
        audioSrc.Play();
    }
    public void PlayHitSound()
    {
        audioSrc.clip = hitSound;
        audioSrc.Play();
    }
    public void PlayPocketSound()
    {
        audioSrc.clip = pocketSound;
        audioSrc.Play();
    }
    public void PlayWinSound()
    {
        audioSrc.clip = winSound;
        audioSrc.Play();
    }
    public void PlaySwitchSound()
    {
        audioSrc.clip = switchSound;
        audioSrc.Play();
    }
    public void PlayStickSound()
    {
        audioSrc.clip = stickSound;
        audioSrc.Play();
    }
}
