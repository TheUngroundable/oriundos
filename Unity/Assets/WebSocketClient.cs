using UnityEngine;
using WebSocketSharp;
using System.Collections;
using System.Collections.Generic;
[System.Serializable]
class PlayerInfo {

    public float id;
    public string command;
    public float direction;
}

public class WebSocketClient : MonoBehaviour
{
    WebSocket ws;
    public PlayerManager player1;
    public PlayerManager player2;

    private float player1Direction;
    private float player2Direction;

    public List<float> directions = new List<float>();
    private void Start()
    {
        Invoke("Starter",2f);
    }

    void Starter()
    {
        ws = new WebSocket("ws://192.168.1.191:8080");
        ws.ConnectAsync();
        Debug.Log("Connecting to server");
        ws.OnMessage += (sender, e) =>
        {
             PlayerInfo playerInfo = JsonUtility.FromJson<PlayerInfo>(""+e.Data);
             if(playerInfo.command.ToString() == "MOVEMENT"){
                if(playerInfo.id == 1f && player1) {
                    player1Direction = playerInfo.direction;
                } else if(playerInfo.id == 2f){
                    player2Direction = playerInfo.direction;
                }
            }
        };

    }

    private void Update()
    {
        if(ws == null)
        {
            return;
        }
        if(player1.GetComponent<PlayerManager>().direction != player1Direction){
            player1.GetComponent<PlayerManager>().SetDirection(player1Direction);
        }
        if(player1.GetComponent<PlayerManager>().direction != player1Direction){
            player2.GetComponent<PlayerManager>().SetDirection(player2Direction);
        }
    }
}