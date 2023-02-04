using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
         Debug.Log("IAIAOWDAWD");
        col.transform.parent.GetComponent<RootManager>().GetPowerUp();
        Destroy(this.gameObject);
    }
}
