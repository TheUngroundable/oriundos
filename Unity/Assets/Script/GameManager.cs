using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PlayerManager[] players;
    public CameraMovement cam;
    private WebSocketClient wsc;
    public bool isDebug;
    private int playerAlive;

    void Start()
    {
        wsc = GameObject.FindObjectOfType<WebSocketClient>();
        InvokeRepeating("AddPieceToPlayer",2,0.1f);
        InvokeRepeating("SpeedTime",5,5);
        
    }

    public void AddPieceToPlayer()
    {
        if(wsc.isPlaying&&!isDebug)
        {
            foreach(PlayerManager pm in players)
                pm.AddPieceToRoot();
            
            cam.AddMovement();
        }
        
        
        if(isDebug)
        {
            foreach(PlayerManager pm in players)
                pm.AddPieceToRoot();
            
            cam.AddMovement();
        }
    }

    public void CheckLoose()
    {

        playerAlive = players.Length;
        playerAlive --;
        if(playerAlive==1)
        {
            foreach(PlayerManager pm in players)
            {
                if(pm.isAlive){
                    wsc.EndGame();
                    Time.timeScale = 0;
                    int score = 0;
                    Debug.Log("VINCE SOLO UNOI"+pm.transform.name);
                    foreach(RootManager root in pm.allRoots)
                        score += root.pieces;
                    Debug.Log("Ci sono "+score);
                }
            }
        }
    }

    public void SpeedTime()
    {
         Time.timeScale += 0.1f;
    }
}
