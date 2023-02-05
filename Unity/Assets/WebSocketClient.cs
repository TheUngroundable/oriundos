using UnityEngine;
using WebSocketSharp;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
 
[System.Serializable]
class PlayerInfo {
    public float id;
    public string command;
    public float value;
    public string type;
    public int roomNumber;
}

public class WebSocketClient : MonoBehaviour
{
    WebSocket ws;
    public PlayerManager player1;
    public PlayerManager player2;

    private float player1Direction;
    private float player2Direction;

    public bool isPlaying;
    public float numberOfPlayers;

    public Text roomNumberText;
    private string roomNumberLabel = "ROOM CODE: ";
    private int roomNumber;

    public List<float> directions = new List<float>();
    private void Start()
    {
        Invoke("Starter",2f);
    }

    private void SendMessage(string command, float value){
        PlayerInfo message= new PlayerInfo();
        message.id = 0;
        message.command = command;
        message.value = value;
        message.type = "UNITY";
        message.roomNumber = roomNumber;
        ws.Send(JsonUtility.ToJson(message));
    }

    void Starter()
    {
        ws = new WebSocket("ws://192.168.1.191:8080");
        ws.Connect();
        Debug.Log("Connecting to server");

        roomNumber = Random.Range(0, 9999);
        roomNumberText.text = roomNumberLabel+roomNumber.ToString();

        SendMessage("JOINED", 1);
        Debug.Log("I have joined");

        ws.OnMessage += (sender, e) =>
        {
            PlayerInfo playerInfo = JsonUtility.FromJson<PlayerInfo>(""+e.Data);

            if(playerInfo.command.ToString() == "MOVEMENT"){
                if(playerInfo.id == 1f && player1) {
                    player1Direction = playerInfo.value;
                } else if(playerInfo.id == 2f){
                    player2Direction = playerInfo.value;
                }
            }
            if(playerInfo.command.ToString() == "STARTED"){
                if(playerInfo.value == 1f){
                    isPlaying = true;
                    Debug.Log("Game has started");
                } else if(playerInfo.value == 0f){
                    isPlaying = false;
                    Debug.Log("Game has stopped");
                }
            }
        };

    }

    public void EndGame(){
        
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
        if(player2.GetComponent<PlayerManager>().direction != player2Direction){
            player2.GetComponent<PlayerManager>().SetDirection(player2Direction);
        }
    }
}