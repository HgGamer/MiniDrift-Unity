using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Security.Cryptography.X509Certificates;
using System;
using System.Web;

using System.Threading.Tasks;
using Fleck;

public class FleckTest : MonoBehaviour
{

    WebSocketServer server;
    void Start()
    {
        UnitySystemConsoleRedirector.Redirect();
        Console.WriteLine("test");
        FleckLog.Level = LogLevel.Debug;
        server = new WebSocketServer("wss://0.0.0.0:8432");
        server.EnabledSslProtocols = System.Security.Authentication.SslProtocols.Tls12;
        server.Certificate = new X509Certificate2("local.pfx","123456");
        server.Start(socket =>
        {
            socket.OnOpen = () => Debug.Log("Open!");
            socket.OnClose = () => Debug.Log("Close!");
            socket.OnMessage = message => socket.Send("echo:"+message);
        });
        
    }
    private void OnDisable()
    {
        server.Dispose();
    }
}
