using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        col.transform.parent.GetComponent<RootManager>().GetObstacle();
    }
}
