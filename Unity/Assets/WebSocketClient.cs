using UnityEngine;
using WebSocketSharp;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
 
[System.Serializable]
class PlayerInfo {
    public int id;
    public string command;
    public float value;
    public string type;
    public int roomNumber;
}

public class WebSocketClient : MonoBehaviour
{
    WebSocket ws;

    public GameManager gameManager;

    public bool isPlaying;
    public float numberOfPlayersInRoom;

    public Text roomNumberText;
    private string roomNumberLabel = "ROOM CODE: ";
    private int roomNumber;

    public string serverIp = "ws://192.168.1.191:8080";

    public List<float> playerDirections = new List<float>();
    private void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
        Invoke("Starter",2f);
    }

    private void SendMessage(string command, float value){
        PlayerInfo message= new PlayerInfo();
        message.id = -1;
        message.command = command;
        message.value = value;
        message.type = "UNITY";
        message.roomNumber = roomNumber;
        ws.Send(JsonUtility.ToJson(message));
    }

    void Starter()
    {
        ws = new WebSocket(serverIp);
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
                playerDirections[playerInfo.id] = playerInfo.value;
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
            if(playerInfo.command.ToString() == "JOINED"){
                Debug.Log("Player Joined");
                playerDirections.Add(0);
                if(playerInfo.id != -1){
                    numberOfPlayersInRoom++;
                }
            }
            
        };

    }

    public void EndGame(){
        SendMessage("FINISHED", 1);
    }

    private void Update()
    {
        if(ws == null || playerDirections.Count < gameManager.players.Length)
        {
            return;
        }

        foreach(PlayerManager player in gameManager.players) {  
            Debug.Log(player.PlayerID);
            float direction = playerDirections[player.PlayerID];
            if(player.direction != direction){
                player.SetDirection(direction);
            }
        }
    }
}