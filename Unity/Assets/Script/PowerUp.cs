using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        col.transform.parent.GetComponent<RootManager>().GetPowerUp(col.transform.position);
        Destroy(this.gameObject);
    }
}
