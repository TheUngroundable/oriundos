using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PlayerManager[] players;
    public CameraMovement cam;
    void Start()
    {
        InvokeRepeating("AddPieceToPlayer",2,0.1f);
        InvokeRepeating("SpeedTime",5,5);
    }

    public void AddPieceToPlayer()
    {
        foreach(PlayerManager pm in players)
            pm.AddPieceToRoot();
        
        cam.AddMovement();
    }


    public void SpeedTime()
    {
         Time.timeScale += 0.1f;
    }
}
