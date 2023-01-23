using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using WebSocketSharp;
 enum SslProtocolsHack
{
    Tls = 192,
    Tls11 = 768,
    Tls12 = 3072
}
public class Client : MonoBehaviour
{
    DateTime foo = DateTime.Now;
    WebSocket ws;
    void Start()
    {
        ws = new WebSocket("ws://localhost:8000");
      
        ws.OnMessage += (sender, e) =>
        {
            Debug.Log(e.Data);
            WSEvent _event = new WSEvent();
            _event.Type = EventType.Message;
            _event.Name = e.Data.Split("|")[0];
            _event.Data = e.Data.Split("|")[1];
            if(e.Data.Split("|")[1] == "createCar")
            {
                _event.Type = EventType.Open;
                _event.Data = e.Data.Split("|")[2];
            }
           
           
            EventManager.events.Enqueue(_event);
           
        };

        ws.OnOpen += (sender, e) =>{
          
        };
        ws.OnError += (sender, e) => {
            Debug.Log(e.Message);
        };

        ws.OnClose += (sender, e) => {
            Debug.Log(e.Reason);
        };
        ws.Connect();
        
    }

    private void Update()
    {
        if(ws == null) {
            return;
        }
        while (EventManager.events.Count > 0)
        {
            WSEvent _event = EventManager.events.Dequeue();
            switch (_event.Type)
            {
                case EventType.Open:
                    EventManager.PlayerJoin(_event.Name, _event.Data);
                    break;
                case EventType.Message:
                    EventManager.PlayerInput(_event.Data, _event.Name);
                    break;
                case EventType.Error:
                    break;
                case EventType.Close:
                    EventManager.PlayerLeave(_event.Name);
                    break;
                default:
                    break;
            }
        }

    }
    void OnDestory()
    {
        ws.Close();
    }
}
