using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private GameObject particle;
    public AudioManager audioManager;

    private void Start()
    {
         particle = GameObject.Find("Particle");
         //particle.SetActive(false);
        audioManager = GameObject.FindObjectOfType<AudioManager>();

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        col.transform.parent.GetComponent<RootManager>().GetObstacle();
        Camera.main.GetComponent<CameraShake>().Shake();
        
        audioManager.PlayRockHit();

        //particle
        particle.transform.position = col.transform.position;
        StartCoroutine("ActiveParticle");
       
       
    }

    IEnumerator  ActiveParticle()
    {
        particle.SetActive(true);
        yield return new WaitForSeconds(0.22f);
        particle.SetActive(false);
    }
}
