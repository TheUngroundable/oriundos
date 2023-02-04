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
    }

    public void AddPieceToPlayer()
    {
        foreach(PlayerManager pm in players)
            pm.AddPieceToRoot();
        
        cam.AddMovement();
    }
}
