using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{

    public AudioManager audioManager;

    private void Start()
    {
        audioManager = GameObject.FindObjectOfType<AudioManager>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        audioManager.PlayPowerUp();
        col.transform.parent.GetComponent<RootManager>().GetPowerUp(col.transform.position);
        Destroy(this.gameObject);
    }
}
