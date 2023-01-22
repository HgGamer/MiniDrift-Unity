
using UnityEngine;
using WebSocketSharp;
using WebSocketSharp.Server;
using System.Security.Cryptography.X509Certificates;


public class Echo : WebSocketBehavior
{

    
    protected override void OnOpen()
    {
        lock (EventManager.events)
        {
            WSEvent _event = new WSEvent();
            _event.Name = Context.QueryString["name"];
            _event.Context = Context;
            _event.Data = null;
            _event.Type = EventType.Open;
            EventManager.events.Enqueue(_event);
        }   
    }

    protected override void OnMessage(MessageEventArgs e)
    {
        WSEvent _event = new WSEvent();
        _event.Name = Context.QueryString["name"];
        _event.Context = Context;
        _event.Data = e.Data;
        _event.Type = EventType.Message;
        EventManager.events.Enqueue(_event);
    }


    protected override void OnClose(CloseEventArgs e)
    {
        WSEvent _event = new WSEvent();
        _event.Name = Context.QueryString["name"];
        _event.Context = Context;
        _event.Data = null;
        _event.Type = EventType.Close;
        EventManager.events.Enqueue(_event);
    }
   
}
public class Server : MonoBehaviour
{
  
    // Start is called before the first frame update
    void Start()
    {
        var wssv = new WebSocketServer(8080, true);
        wssv.AuthenticationSchemes = WebSocketSharp.Net.AuthenticationSchemes.Anonymous;
        wssv.SslConfiguration.ServerCertificate = new X509Certificate2("cert.pfx", "123456");

        wssv.AddWebSocketService<Echo>("/");
        wssv.Start();
        if (wssv.IsListening)
        {
            Debug.Log("Listening on port "+ wssv.Port+", and providing WebSocket services:");

            foreach (var path in wssv.WebSocketServices.Paths)
                Debug.Log( path);
        }

       
    }
  
    private void Update()
    {
        lock (EventManager.events)
        {
            while (EventManager.events.Count > 0)
            {
                WSEvent _event = EventManager.events.Dequeue();
                switch (_event.Type)
                {
                    case EventType.Open:
                        EventManager.PlayerJoin(_event.Name);
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
    }
  

}
