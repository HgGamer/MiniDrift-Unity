using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using WebSocketSharp;
using ZXing;
using ZXing.QrCode;
using System.Net;
using System.Net.Sockets;



 enum SslProtocolsHack
{
    Tls = 192,
    Tls11 = 768,
    Tls12 = 3072
}
public class Client : MonoBehaviour
{
    public RawImage image;
    DateTime foo = DateTime.Now;
    WebSocket ws;
    void Start()
    {
        image.texture = generateQR("https://"+GetLocalIPAddress()+":8080");
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
            if(e.Data.Split("|")[1] == "disconnected")
            {
                _event.Type = EventType.Close;
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
                    Debug.Log("//Open");
                    EventManager.PlayerJoin(_event.Name, _event.Data);
                    break;
                case EventType.Message:
                    Debug.Log("//Message");
                    EventManager.PlayerInput(_event.Data, _event.Name);
                    break;
                case EventType.Error:
                    break;
                case EventType.Close:
                    Debug.Log("//Close");
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
    private static Color32[] Encode(string textForEncoding, int width, int height)
    {
        BarcodeWriter writer = new BarcodeWriter
        {
            Format = BarcodeFormat.QR_CODE,
            Options = new QrCodeEncodingOptions
            {
                Height = height,
                Width = width
            }
        };
        return writer.Write(textForEncoding);
    }

    public Texture2D generateQR(string text)
    {
        Debug.Log(text);
        var encoded = new Texture2D(256, 256);
        var color32 = Encode(text, encoded.width, encoded.height);
        encoded.SetPixels32(color32);
        encoded.Apply();
        return encoded;
    }


   
    public static string GetLocalIPAddress()
    {
        var host = Dns.GetHostEntry(Dns.GetHostName());
        foreach (var ip in host.AddressList)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
            {
                return ip.ToString();
            }
        }
        return "";
    }

}
