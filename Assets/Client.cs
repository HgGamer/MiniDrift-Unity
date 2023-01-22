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
    WebSocket ws;
    void Start()
    {
        ws = new WebSocket("ws://localhost:8000");
      
        ws.OnMessage += (sender, e) =>
        {
            Debug.Log(e.Data);
        };

        ws.OnOpen += (sender, e) => ws.Send("Hi, there!");
        ws.OnError += (sender, e) => {
            var fmt = "[WebSocket Error] {0}";

            Debug.Log(e.Message);
        };

        ws.OnClose += (sender, e) => {
            var fmt = "[WebSocket Close ({0})] {1}";

            Debug.Log(e.Reason);
        };
        ws.Connect();
        

        
    }

    private void Update()
    {
        if(ws == null) {
            return;
        }

    }
}
