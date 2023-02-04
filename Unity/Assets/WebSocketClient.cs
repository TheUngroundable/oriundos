using UnityEngine;
using WebSocketSharp;
public class WebSocketClient : MonoBehaviour
{
    WebSocket ws;
    private void Start()
    {
        Invoke("Starter",2f);
    }

    void Starter()
    {
       ws = new WebSocket("ws://192.168.1.59:8080");
        ws.ConnectAsync();
        Debug.Log("ciao");
       ws.OnMessage += (sender, e) =>
       {
          Debug.Log("Message Received from "+((WebSocket)sender).Url+", Data : "+e.Data);
       };

    }
    
    private void Update()
    {
        if(ws == null)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.Space))
                {
                    Debug.Log("Sent");
                    ws.Send("Hello");
                }  
     
        
    } 
    
}