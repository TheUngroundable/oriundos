using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    private AudioSource audioSource;

    public AudioClip[] rockAudioClips;

    public AudioClip[] randomGrowAudioClip;
    public AudioClip[] powerUpAudioClips;

    public float volume;

    void Start(){
        audioSource = gameObject.GetComponent<AudioSource>();
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
        audioSource.PlayOneShot(audioClip, volume);
    }
}
