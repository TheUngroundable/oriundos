using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float speed;
    private bool canScroll;
    
    void Start()
    {
        Invoke("StartMovements",2);
    }

    void StartMovements()
    {
        canScroll = true;
    }


    public void AddMovement()
    {
        transform.position += new Vector3(0,-0.01f,0);
    }
}
