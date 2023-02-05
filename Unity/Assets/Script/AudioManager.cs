using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public AudioClip intro;

    private AudioSource audioSource;

    public AudioClip[] rockAudioClips;

    public AudioClip[] randomGrowAudioClip;
    public AudioClip[] powerUpAudioClips;



    public float volume = 1;

    void Start(){
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.clip = intro;
        audioSource.Play();
    }

    private static AudioClip GetRandomAudioClip(AudioClip[] audioClips)
    {
        return audioClips[(int)(Random.Range(0f, 10.0f) % audioClips.Length)];
    } 

    public void PlayRockHit(){
        AudioClip randomRockAudioClip = GetRandomAudioClip(rockAudioClips);
        PlaySound(randomRockAudioClip);
    }

    public void PlayPowerUp(){
        AudioClip randomPowerUpClip = GetRandomAudioClip(powerUpAudioClips);
        PlaySound(randomPowerUpClip);
    }

    public void PlayGrow(){
        AudioClip randomGrowAudioClip = GetRandomAudioClip(rockAudioClips);
        PlaySound(randomGrowAudioClip);
    }

    private void PlaySound(AudioClip audioClip){
        Debug.Log("I am playing "+audioClip.name);
        audioSource.PlayOneShot(audioClip, volume);
    }
}
