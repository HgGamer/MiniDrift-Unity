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
using Fleck;

using System.Security.Cryptography.X509Certificates;

enum SslProtocolsHack
{
    Tls = 192,
    Tls11 = 768,
    Tls12 = 3072
}
public class Client : MonoBehaviour
{
    WebSocketServer server;
    public RawImage image;

    void Start()
    {
        image.texture = generateQR("https://s1.app.catfood.li:8080/");
        server = new WebSocketServer("wss://0.0.0.0:8432");
        server.EnabledSslProtocols = System.Security.Authentication.SslProtocols.Tls12;
        server.Certificate = new X509Certificate2("local.pfx", "123456");

        server.Start(socket =>
        {
            socket.OnOpen = () => Debug.Log("Open!");
            socket.OnClose = () => Debug.Log("Close!");
           
            socket.OnMessage = (message => {

              
                WSEvent _event = new WSEvent();
                _event.Type = EventType.Message;
                
                _event.Name = socket.ConnectionInfo.Id.ToString();
                _event.Data = message.Split("|")[0];
                if (message.Split("|")[0] == "createCar")
                {
                    _event.Type = EventType.Open;
                    _event.Data = message.Split("|")[1];
                  
                }
                if (message.Split("|")[0] == "disconnected")
                {
                    _event.Type = EventType.Close;
                }
                EventManager.events.Enqueue(_event);
            });
        });

        
    }

    private void Update()
    {
        if(server == null) {
            return;
        }
        while (EventManager.events.Count > 0)
        {
   
            WSEvent _event = EventManager.events.Dequeue();
            switch (_event.Type)
            {
                case EventType.Open:
                    //Debug.Log("//Open");
                    EventManager.PlayerJoin(_event.Name, _event.Data);
                    break;
                case EventType.Message:
                    //Debug.Log("//Message");
                    EventManager.PlayerInput(_event.Data, _event.Name);
                    break;
                case EventType.Error:
                    break;
                case EventType.Close:
                   // Debug.Log("//Close");
                    EventManager.PlayerLeave(_event.Name);
                    break;
                default:
                    break;
            }
        }

    }
    void OnDestory()
    {
        server.Dispose();
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
