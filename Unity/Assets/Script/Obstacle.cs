using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private GameObject particle;

    private void Start()
    {
         particle = GameObject.Find("Particle");
         particle.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        col.transform.parent.GetComponent<RootManager>().GetObstacle();
        Camera.main.GetComponent<CameraShake>().Shake();
        
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
